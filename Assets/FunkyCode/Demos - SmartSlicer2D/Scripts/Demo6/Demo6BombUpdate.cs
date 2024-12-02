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

public class Demo6BombUpdate : MonoBehaviour {
	private float timer = 0;

	void Update() {
		timer += Time.deltaTime;

		if (timer > 5.0) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.name.Contains ("Terrain")) {
			Vector2D pos = new Vector2D (transform.position);

			Polygon2D.defaultCircleVerticesCount = 15;

			Polygon2D slicePolygon = Polygon2D.Create (Polygon2D.PolygonType.Circle, 2f);
			Polygon2D slicePolygonDestroy = Polygon2D.Create (Polygon2D.PolygonType.Circle, 2.5f);

			slicePolygon = slicePolygon.ToOffset(pos);
			slicePolygonDestroy = slicePolygonDestroy.ToOffset(pos);

			foreach (Slicer2D id in Slicer2D.GetList()) {
				Slice2D result = Slicer2D.API.PolygonSlice (id.GetPolygon().ToWorldSpace (id.transform), slicePolygon);
				if (result.polygons.Count > 0) {
					foreach (Polygon2D p in new List<Polygon2D>(result.polygons)) {
						if (slicePolygonDestroy.PolyInPoly (p) == true) {
							result.polygons.Remove (p);
						}
					}

					if (result.polygons.Count > 0) {
						id.PerformResult (result.polygons, new Slice2D());
					} else {
						// Polygon is Destroyed!!!
						Destroy (id.gameObject);
					}
				}
			}

			Destroy (gameObject);

			Polygon2D.defaultCircleVerticesCount = 25;
		}
	}
}
