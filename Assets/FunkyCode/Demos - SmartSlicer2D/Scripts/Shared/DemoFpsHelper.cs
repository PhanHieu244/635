/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DemoFpsHelper : MonoBehaviour {
	int fps = 0;
	float timer = 0;
	int fpsResult = 0;
	Text text;

	void Start() {
		text = GetComponent<Text> ();
	}

	void OnRenderObject() {
		fps += 1;

		if (Time.realtimeSinceStartup > timer + 1){
			timer = Time.realtimeSinceStartup;
			fpsResult = fps;
			fps = 0;
		}

		if (Application.targetFrameRate > 0) {
			text.text = "fps " + fpsResult + "/" + Application.targetFrameRate;
		} else {
			text.text = "fps " + fpsResult;
		}
	}
}
