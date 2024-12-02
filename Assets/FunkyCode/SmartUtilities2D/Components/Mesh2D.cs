/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;

[ExecuteInEditMode]
public class Mesh2D : MonoBehaviour {
	public PolygonTriangulator2D.Triangulation triangulation = PolygonTriangulator2D.Triangulation.Advanced;

	// Optionable material
	public Material material;
	public Vector2 materialScale = new Vector2(1, 1);
	public Vector2 materialOffset = Vector2.zero;

	public string sortingLayerName; 
	public int sortingLayerID;
	public int sortingOrder;

	void Start () {
		if (GetComponents<Mesh2D>().Length > 1) {
			//Debug.LogError("Multiple 'Mesh2D' components cannot be attached to the same game object");
			return;
		}

		// Generate Mesh from collider
		Polygon2D polygon = Polygon2DList.CreateFromGameObject (gameObject)[0];
		if (polygon != null) {
			polygon.CreateMesh(gameObject, materialScale, materialOffset, triangulation);

			// Setting Mesh material
			if (material != null) {
				MeshRenderer meshRenderer = GetComponent<MeshRenderer> ();
				meshRenderer.material = material;
			
				meshRenderer.sortingLayerName = sortingLayerName;
				meshRenderer.sortingLayerID = sortingLayerID;
				meshRenderer.sortingOrder = sortingOrder;
			}
		}
	}
}
