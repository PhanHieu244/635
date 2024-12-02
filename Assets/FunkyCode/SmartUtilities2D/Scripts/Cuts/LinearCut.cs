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

public class LinearCut {
	public Pair2D pairCut;
	float size = 1f;

	static public LinearCut Create(Pair2D pair, float size)
	{
		LinearCut cut = new LinearCut();
		cut.size = size;
		cut.pairCut = pair;
		return(cut);
	}

	public List<Vector2D> GetPointsList(float multiplier = 1f){
		if (pairCut == null) {
			Debug.LogWarning("SmartUtilities2D: Linear Cut generation issue");
			return(new List<Vector2D>());
		}

		double rot = Vector2D.Atan2(pairCut.A, pairCut.B);

		Vector2D a = pairCut.A.Copy();
		Vector2D b = pairCut.A.Copy();
		Vector2D c = pairCut.B.Copy();
		Vector2D d = pairCut.B.Copy();

		a.Push(rot + Mathf.PI / 4, size * multiplier);
		b.Push(rot - Mathf.PI / 4, size * multiplier);
		c.Push(rot + Mathf.PI / 4 + Mathf.PI, size * multiplier);
		d.Push(rot - Mathf.PI / 4 + Mathf.PI, size * multiplier);

		List<Vector2D> result = new List<Vector2D>();
		result.Add(a);
		result.Add(b);
		result.Add(c);
		result.Add(d);

		return(result);
	}
}
