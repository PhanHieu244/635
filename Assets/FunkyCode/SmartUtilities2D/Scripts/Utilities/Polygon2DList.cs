/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon2DList : Polygon2D {

	// Get List Of Polygons from Collider (Usually Used Before Creating Slicer2D Object)
	static public List<Polygon2D> CreateFromPolygonColliderToWorldSpace(PolygonCollider2D collider) {
		List<Polygon2D> result = new List<Polygon2D> ();

		if (collider != null && collider.pathCount > 0) {
			Polygon2D newPolygon = new Polygon2D ();

			foreach (Vector2 p in collider.GetPath (0)) {
				newPolygon.AddPoint (p + collider.offset);
			}
			
			newPolygon = newPolygon.ToWorldSpace(collider.transform);

			result.Add (newPolygon);

			for (int i = 1; i < collider.pathCount; i++) {
				Polygon2D hole = new Polygon2D ();
				foreach (Vector2 p in collider.GetPath (i)) {
					hole.AddPoint (p + collider.offset);
				}

				hole = hole.ToWorldSpace(collider.transform);

				if (newPolygon.PolyInPoly (hole) == true) {
					newPolygon.AddHole(hole);
				} else {
					result.Add(hole);
				}
			}
		}
		return(result);
	}

	static public List<Polygon2D> CreateFromPolygonColliderToLocalSpace(PolygonCollider2D collider) {
		List<Polygon2D> result = new List<Polygon2D>();

		if (collider != null && collider.pathCount > 0) {
			Polygon2D newPolygon = new Polygon2D ();

			foreach (Vector2 p in collider.GetPath (0)) {
				newPolygon.AddPoint (p + collider.offset);
			}

			result.Add(newPolygon);

			for (int i = 1; i < collider.pathCount; i++) {
				Polygon2D hole = new Polygon2D ();
				foreach (Vector2 p in collider.GetPath (i)) {
					hole.AddPoint (p + collider.offset);
				}

				if (newPolygon.PolyInPoly (hole) == true) {
					newPolygon.AddHole (hole);
				} else {
					result.Add(hole);
				}
			}
		}
		return(result);
	}

	// Slower CreateFromCollider
	public static List<Polygon2D> CreateFromGameObject(GameObject gameObject) {
		ColliderType colliderType = GetColliderType(gameObject);

		return(CreateFromGameObject(gameObject, colliderType));
	}

	// Faster CreateFromCollider
	public static List<Polygon2D> CreateFromGameObject(GameObject gameObject, ColliderType colliderType) {
		List<Polygon2D> result = new List<Polygon2D>();
		switch (colliderType) {
			case ColliderType.Edge:
				result.Add(CreateFromEdgeCollider (gameObject.GetComponent<EdgeCollider2D> ()));
				break;
			case ColliderType.Polygon:
				result = CreateFromPolygonColliderToLocalSpace(gameObject.GetComponent<PolygonCollider2D> ());
				break;
			case ColliderType.Box:
				result.Add(CreateFromBoxCollider (gameObject.GetComponent<BoxCollider2D> ()));
				break;
			case ColliderType.Circle:
				result.Add(CreateFromCircleCollider (gameObject.GetComponent<CircleCollider2D> ()));
				break;
			case ColliderType.Capsule:
				result.Add(CreateFromCapsuleCollider (gameObject.GetComponent<CapsuleCollider2D> ()));
				break;
			default:
				break;
		}
		return(result);
	}
}
