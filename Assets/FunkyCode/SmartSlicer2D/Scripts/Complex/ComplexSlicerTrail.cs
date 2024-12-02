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
using System.Linq;

public class ComplexSlicerTrail {
	public List<SlicerTrailObject> trailList = new List<SlicerTrailObject>();

	public List<Slice2D> Update(Vector2D position, float timer) {
		List<Slice2D> result = new List<Slice2D>();
		foreach(Slicer2D slicer in Slicer2D.GetList()) {
			SlicerTrailObject trail = GetSlicerTrail(slicer);
			if (trail == null) {
				trail = new SlicerTrailObject();
				trail.slicer = slicer;
				trailList.Add(trail);
			}

			if (trail.lastPosition != null) {
				if (Vector2D.Distance(trail.lastPosition, position) > 0.05f) {
					trail.pointsList.Add(new TrailPoint(position, timer));
				}
			} else {
				trail.pointsList.Add(new TrailPoint(position, timer));
			}

			if (trail.pointsList.Count > 1) {
				foreach(TrailPoint trailPoint in new List<TrailPoint>(trail.pointsList)) {
					if (trailPoint.Update() == false) {
						trail.pointsList.Remove(trailPoint);
					}
				}

				List<Vector2D> points = new List<Vector2D>();
				foreach(TrailPoint trailPoint in trail.pointsList) {
					points.Add(trailPoint.position);
				}

				Slicer2D.complexSliceType = Slicer2D.SliceType.Regular;
				Slice2D slice = slicer.ComplexSlice(points);
				if (slice.gameObjects.Count > 0) {
					trailList.Remove(trail);

					result.Add(slice);
				};
			}

			trail.lastPosition = position;
		}

		return(result);
	}

	public SlicerTrailObject GetSlicerTrail(Slicer2D slicer) {
		foreach(SlicerTrailObject trail in trailList) {
			if (trail.slicer == slicer) {
				return(trail);
			}
		}
		return(null);
	}
}
