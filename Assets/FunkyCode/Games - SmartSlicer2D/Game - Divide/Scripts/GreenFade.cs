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

public class GreenFade : MonoBehaviour {
	public enum FadeType {Green, Red};
	public FadeType fadeTyp = FadeType.Green;
	MeshRenderer meshRenderer;
	float fade = 0;


	void Start () {
		meshRenderer = GetComponent<MeshRenderer>();
	}
	
	void Update () {
		switch(fadeTyp) {
			case FadeType.Green:
				meshRenderer.material.SetColor ("_TintColor", new Color(0.5f, 0.5f + fade, 0.5f, 0.5f));

			break;

			case FadeType.Red:
				meshRenderer.material.SetColor ("_TintColor", new Color(0.5f + fade, 0.5f - fade * 0.25f, 0.5f - fade * 0.25f, 0.5f));

			break;
		}

		if (fade < 0.35f) {
			fade += Time.deltaTime * 2;
		}
	}
}
