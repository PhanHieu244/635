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
public class DemoTabs : MonoBehaviour {
	public GameObject[] tabObjects;
	public GameObject[] buttons;
	int tabid = 0;

	public void SetTab(int id) {
		tabid = id;
		for(int i = 0; i < tabObjects.Length; i++) {
			if (i == id) {
				tabObjects[i].SetActive(true);
			} else {
				tabObjects[i].SetActive(false);
			}
		}
		ColorUpdate ();
	}

	void Start() {
		 ColorUpdate ();
	}
	
	void ColorUpdate () {
		for(int i = 0; i < tabObjects.Length; i++) {
			Button button = buttons[i].GetComponent<Button>();
			ColorBlock colors = button.colors;
			if (i == tabid) {
				colors.normalColor = new Color(1, 142f / 255, 0, 1);
				colors.highlightedColor = new Color(1, 142f / 255, 0, 1);
				
			} else {
				colors.normalColor = new Color(47f / 255, 47f / 255, 47f / 255, 76f / 255);
				colors.highlightedColor = new Color(255f / 255, 144f / 255, 47f / 255, 206f / 255);
			}
			
			button.colors = colors;
		}
	}
}
