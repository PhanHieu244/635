/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PolygonTriangulator2D : MonoBehaviour {
	public enum Triangulation {Advanced, Legacy};
	static float precision = 0.001f;

	static bool garbageCollector = true;
	
	public static Mesh Triangulate3D(Polygon2D polygon, float z, Vector2 UVScale, Vector2 UVOffset, Triangulation triangulation) {
		Mesh result = null;
		switch (triangulation) {
			case Triangulation.Advanced:
				Polygon2D newPolygon = new Polygon2D(PreparePolygon(polygon));
				foreach (Polygon2D hole in polygon.holesList) {
					newPolygon.AddHole(new Polygon2D(PreparePolygon(hole)));
				}

				if (newPolygon.pointsList.Count < 3) {
					if ((int)polygon.GetArea() == 0) {
						List<Vector2> l = new List<Vector2>();
						foreach(Vector2D p in polygon.pointsList) {
							l.Add(p.ToVector2());
						}

						result = Triangulator.Create(l.ToArray());;
						
						return(result);
					}
				}

				List<Vector3> sideVertices = new List<Vector3>();
				List<int> sideTriangles = new List<int>();
				int vCount = 0;
				foreach(Pair2D pair in Pair2D.GetList(polygon.pointsList)) {
					Vector3 pointA = new Vector3((float)pair.A.x, (float)pair.A.y, 0);
					Vector3 pointB = new Vector3((float)pair.B.x, (float)pair.B.y, 0);
					Vector3 pointC = new Vector3((float)pair.B.x, (float)pair.B.y, 1);
					Vector3 pointD = new Vector3((float)pair.A.x, (float)pair.A.y, 1);

					sideVertices.Add(pointA);
					sideVertices.Add(pointB);
					sideVertices.Add(pointC);
					sideVertices.Add(pointD);
					
					sideTriangles.Add(vCount + 2);
					sideTriangles.Add(vCount + 1);
					sideTriangles.Add(vCount + 0);
					
					sideTriangles.Add(vCount + 0);
					sideTriangles.Add(vCount + 3);
					sideTriangles.Add(vCount + 2);

					vCount += 4;
				}

				Mesh meshA = TriangulateAdvanced(newPolygon, UVScale, UVOffset);

				Mesh meshB = new Mesh();
				List<Vector3> verticesB = new List<Vector3>();
				foreach(Vector3 v in meshA.vertices) {
					verticesB.Add(new Vector3(v.x, v.y, v.z + z));
				}
				meshB.vertices = verticesB.ToArray();
				meshB.triangles = meshA.triangles.Reverse().ToArray();
		

				Mesh mesh = new Mesh();
				mesh.vertices = sideVertices.ToArray();
				mesh.triangles= sideTriangles.ToArray();
			
				List<Vector3> vertices = new List<Vector3>();
				foreach(Vector3 v in meshA.vertices) {
					vertices.Add(v);
				}
				foreach(Vector3 v in meshB.vertices) {
					vertices.Add(v);
				}
				foreach(Vector3 v in sideVertices) {
					vertices.Add(v);
				}
				mesh.vertices = vertices.ToArray();

				List<int> triangles = new List<int>();
				foreach(int p in meshA.triangles) {
					triangles.Add(p);
				}
				int count = meshA.vertices.Count();
				foreach(int p in meshB.triangles) {
					triangles.Add(p + count);
				}
				count = meshA.vertices.Count() + meshB.vertices.Count();
				foreach(int p in sideTriangles) {
					triangles.Add(p + count);
				}
				mesh.triangles = triangles.ToArray();

				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
			
				result = mesh;

			break;
		}

		return(result);
	}

	public static Mesh Triangulate(Polygon2D polygon, Vector2 UVScale, Vector2 UVOffset, Triangulation triangulation) {
		Mesh result = null;
		switch (triangulation) {
			case Triangulation.Advanced:
				if (garbageCollector == true & polygon.GetArea() < 0.005f) {
					Debug.LogWarning("SmartUtilities2D: Garbage Collector Removed Object Because it was too small");
					
					return(null);
				}

				Polygon2D newPolygon = new Polygon2D(PreparePolygon(polygon));

				if (newPolygon.pointsList.Count < 3) {
					Debug.LogWarning("SmartUtilities2D: Mesh is too small for advanced triangulation, using simplified triangulations instead (size: " + polygon.GetArea() +")");
					
					result = TriangulateAdvanced(polygon, UVScale, UVOffset);

					return(result);
				}

				foreach (Polygon2D hole in polygon.holesList) {
					newPolygon.AddHole(new Polygon2D(PreparePolygon(hole, -1)));
				}

				result = TriangulateAdvanced(newPolygon, UVScale, UVOffset);

			break;

			case Triangulation.Legacy:

				List<Vector2> list = new List<Vector2>();
				foreach(Vector2D p in polygon.pointsList) {
					list.Add(p.ToVector2());
				}
				result = Triangulator.Create(list.ToArray());
				return(result);
		}

		return(result);
	}

	// Not finished - still has some artifacts
	static public List<Vector2D> PreparePolygon(Polygon2D polygon, float multiplier = 1f)
	{
		Polygon2D newPolygon = new Polygon2D();

		polygon.Normalize();

		DoublePair2D pair;
		foreach (Vector2D pB in polygon.pointsList) {
				int indexB = polygon.pointsList.IndexOf (pB);

				int indexA = (indexB - 1);
				if (indexA < 0) {
					indexA += polygon.pointsList.Count;
				}

				int indexC = (indexB + 1);
				if (indexC >= polygon.pointsList.Count) {
					indexC -= polygon.pointsList.Count;
				}

				pair = new DoublePair2D (polygon.pointsList[indexA], pB, polygon.pointsList[indexC]);

				double rotA = Vector2D.Atan2(pair.B, pair.A);
				double rotC = Vector2D.Atan2(pair.B, pair.C);

				Vector2D pairA = new Vector2D(pair.A);
				pairA.Push(rotA - Mathf.PI / 2, precision * multiplier);

				Vector2D pairC = new Vector2D(pair.C);
				pairC.Push(rotC + Mathf.PI / 2, precision * multiplier);
				
				Vector2D vecA = new Vector2D(pair.B);
				vecA.Push(rotA - Mathf.PI / 2, precision * multiplier);
				vecA.Push(rotA, 100f);

				Vector2D vecC = new Vector2D(pair.B);
				vecC.Push(rotC + Mathf.PI / 2, precision * multiplier);
				vecC.Push(rotC, 100f);

				Vector2D result = Math2D.GetPointLineIntersectLine(new Pair2D(pairA, vecA), new Pair2D(pairC, vecC));

				if (result != null) {
					newPolygon.AddPoint(result);
				}
			}

		return(newPolygon.pointsList);
	} 

	public static Mesh TriangulateAdvanced(Polygon2D polygon, Vector2 UVScale, Vector2 UVOffset)
	{
		foreach(Pair2D p in Pair2D.GetList(new List<Vector2D>(polygon.pointsList))) {
			if (polygon.pointsList.Count < 4) {
				break;
			}
			if (Vector2D.Distance(p.A, p.B) < 0.005f) {
				Debug.LogWarning("SmartUtilities2D: Polygon points are too close");
				polygon.pointsList.Remove(p.A);
			}
		}

		TriangulationWrapper.Polygon poly = new TriangulationWrapper.Polygon();

		List<Vector2> pointsList = null;
		List<Vector2> UVpointsList = null;

		Vector3 v = Vector3.zero;

		foreach (Vector2D p in polygon.pointsList) {
			v = p.ToVector2();
			poly.outside.Add (v);
			poly.outsideUVs.Add (new Vector2(v.x / UVScale.x + .5f + UVOffset.x, v.y / UVScale.y + .5f + UVOffset.y));
		}

		foreach (Polygon2D hole in polygon.holesList) {
			pointsList = new List<Vector2> ();
			UVpointsList = new List<Vector2> ();
			
			foreach (Vector2D p in hole.pointsList) {
				v = p.ToVector2();
				pointsList.Add (v);
				UVpointsList.Add (new Vector2(v.x / UVScale.x + .5f, v.y / UVScale.y + .5f));
			}

			poly.holes.Add (pointsList);
			poly.holesUVs.Add (UVpointsList);
		}

		return(TriangulationWrapper.CreateMesh (poly));
	}
}
