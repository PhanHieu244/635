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

public class ShadowScript : MonoBehaviour {
	public Material material;
	private Mesh mesh;

	void Start () {
		Polygon2D polygon = Polygon2DList.CreateFromGameObject(gameObject)[0];
		polygon = polygon.ToOffset(new Vector2D(0.125f, -0.125f));
		mesh = polygon.CreateMesh(Vector2.zero, Vector2.zero);
		Update();
	}

	void Update() {
		Vector3 position = transform.position + new Vector3(0, 0, 1);
		Quaternion rotation = transform.rotation;
		Vector3 scale = transform.lossyScale;
		Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);

		Graphics.DrawMesh(mesh, matrix, material, 0);
	}
}
