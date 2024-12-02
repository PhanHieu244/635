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

[ExecuteInEditMode]
public class ColliderLineRenderer2D : MonoBehaviour {
	public bool customColor = false;
	public Color color = Color.white;
	public float lineWidth = 1;
	public int orderInLayer = 0;

	private bool edgeCollider = false; // For Edge Collider

	private Polygon2D polygon = null;
	private Mesh mesh = null;
	private float lineWidthSet = 1;
	private Material material;

	const float lineOffset = -0.01f;

	void Start () {
		if (GetComponent<EdgeCollider2D>() != null) {
			edgeCollider = true;
		}
		
		Max2D.Check();
		material = new Material(Max2D.lineMaterial);

		GenerateMesh();
		Draw();
	}
	
	public void Update() {
		if (lineWidth != lineWidthSet) {
			if (lineWidth < 0.01f) {
				lineWidth = 0.01f;
			}
			GenerateMesh();
		}

		Draw();
	}

	public Polygon2D GetPolygon() {
		if (polygon == null) {
			polygon = Polygon2DList.CreateFromGameObject (gameObject)[0];
		}
		return(polygon);
	}

	public void GenerateMesh() {
		lineWidthSet = lineWidth;

		mesh = Max2DMesh.GeneratePolygon2DMeshNew(transform, GetPolygon(), lineOffset, lineWidth, edgeCollider == false);
	}

	public void Draw() {
		if (customColor) {
			material.SetColor ("_Emission", color);
			Max2DMesh.Draw(mesh, transform, material, orderInLayer);
		} else {
			Max2D.Check();
			Max2D.lineMaterial.SetColor ("_Emission", Color.black);
			Max2DMesh.Draw(mesh, transform, Max2D.lineMaterial, orderInLayer);
		}
	}
}
