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
using UnityEngine.UI;

[ExecuteInEditMode]
public class UITextScale : MonoBehaviour {
	public float ratio = 10f;
	public Rect rect = new Rect(0, 0, 100, 100);

	private Text text;
	private RectTransform rectTransform;

	void Start () {
		text = GetComponent<Text>();
		rectTransform = GetComponent<RectTransform>();
		Update ();
	}
	
	void Update () {
		text.fontSize = (int)(Screen.height * (ratio / 100f));
		rectTransform.anchorMin = rect.min / 100;
		rectTransform.anchorMax = rect.max / 100;
	}
}
