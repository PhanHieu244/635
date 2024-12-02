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

public class UIFade : MonoBehaviour {
	UICanvasScale scaler;
	public bool state = true;

	float startingY; 

	static public UIFade instance;

	public GameObject[] menuObjects;
	

	void Start () {
		scaler = GetComponent<UICanvasScale>();

		startingY = scaler.rect.y;

		instance = this;
	}
	
	void Update () {
		if (state) {
			scaler.rect.y = scaler.rect.y * 0.95f + startingY * 0.05f;
		} else {
			scaler.rect.y = scaler.rect.y * 0.95f + 200 * 0.05f;
		}
	}
}
