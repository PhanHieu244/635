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

[ExecuteInEditMode]
public class JointLineRenderer2D : MonoBehaviour {
	public bool customColor = false;
	public Color color = Color.white;
	public float lineWidth = 1;

	private List<Joint2D> joints = new List<Joint2D>();
	private Material material;

	const float lineOffset = -0.001f;

	public void Start() {
		joints = Joint2D.GetJoints(gameObject);

		Max2D.Check();
		material = new Material(Max2D.lineMaterial);
	}

	public void Update() {
		foreach(Joint2D joint in joints) {
			if (joint.gameObject == null) {
				continue;
			}
			if (joint.anchoredJoint2D == null) {
				continue;
			}
			if (joint.anchoredJoint2D.isActiveAndEnabled == false) {
				continue;
			}
			if (joint.anchoredJoint2D.connectedBody == null) {
				continue;
			}

			switch (joint.jointType) {
				case Joint2D.Type.HingeJoint2D:
					Pair2D pairA = new Pair2D(new Vector2D (transform.TransformPoint (joint.anchoredJoint2D.anchor)), new Vector2D (joint.anchoredJoint2D.connectedBody.transform.TransformPoint (Vector2.zero)));
					Draw(pairA);
					break;

				default:
					Pair2D pairB = new Pair2D(new Vector2D (transform.TransformPoint (joint.anchoredJoint2D.anchor)), new Vector2D (joint.anchoredJoint2D.connectedBody.transform.TransformPoint (joint.anchoredJoint2D.connectedAnchor)));
					Draw(pairB);
					break;
			}
		}
	}

	public void Draw(Pair2D pair) {
		List<Mesh2DTriangle> trianglesList = new List<Mesh2DTriangle>();
		trianglesList.Add(Max2DMesh.CreateLineNew(pair, lineWidth, transform.position.z + lineOffset));
		Mesh mesh = Max2DMesh.ExportMesh(trianglesList);

		if (customColor) {
			material.SetColor ("_Emission", color);
		
			Max2DMesh.Draw(mesh, material);
		} else {
			Max2DMesh.Draw(mesh, Max2D.lineMaterial);
		}
	}
}
