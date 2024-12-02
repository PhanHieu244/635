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

public class SplitSlicePush : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Slicer2D slicer = GetComponent<Slicer2D>();
		slicer.AddResultEvent(SliceEvent);
	}
	
	void SliceEvent(Slice2D slice) {
		Vector2D midPoint = SliceMidPoint(slice.slices[0]);

		foreach(Slicer2D g in Slicer2D.GetList()) {
			Vector2D center = new Vector2D(Polygon2DList.CreateFromGameObject(g.gameObject)[0].ToWorldSpace(g.transform).GetBounds().center);
			Vector2D position = new Vector2D(g.transform.position);
			position.Push(Vector2D.Atan2(center, midPoint), 0.2f); // + Mathf.PI / 2
			g.transform.position = position.ToVector2();
		}
	}

	Vector2D SliceMidPoint(List<Vector2D> points) {
		if (points.Count != 2) {
			return(new Vector2D(0, 0));
		}
		return(new Vector2D((points[0].ToVector2() + points[1].ToVector2()) / 2));
	}
}
