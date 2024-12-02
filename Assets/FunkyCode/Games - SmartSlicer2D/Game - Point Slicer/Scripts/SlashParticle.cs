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

public class SlashParticle : MonoBehaviour {
	public Vector2D moveTo;
	public float speed = 1f;

	static public List<SlashParticle> GetList() {
		List<SlashParticle> result = new List<SlashParticle>();
		foreach (SlashParticle buffer in Object.FindObjectsOfType(typeof(SlashParticle))) {
			result.Add(buffer);
		}
		return(result);
	}

	void Update () {
		Vector3 pos = transform.position;

		if (Vector2.Distance(moveTo.ToVector2(), pos) > speed * 1.5f) {
			pos += Vector2D.RotToVec(Vector2D.Atan2(moveTo, new Vector2D(pos))).ToVector3(0) * speed;
			pos.z = -3f;
			transform.position = pos;
		} else {
			Destroy(gameObject);
		}
	}
}
