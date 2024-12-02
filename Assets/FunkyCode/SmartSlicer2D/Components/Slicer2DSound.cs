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

public class Slicer2DSound : MonoBehaviour {
	public AudioClip clip;

	static public TimerHelper timer = null;

	void Start () {
		Slicer2D slicer = GetComponent<Slicer2D>();
		slicer.AddResultEvent(SlicerEvent);

		if (timer == null) {
			timer = TimerHelper.Create();
		}
	}

	void SlicerEvent (Slice2D slice) {
		if (timer.GetMillisecs() < 15) {
			return;
		}

		if (clip == null) {
			return;
		}

		timer = TimerHelper.Create();

		GameObject sound = new GameObject();
		sound.name = "Slicer2D Sound";

		AudioSource audio = sound.AddComponent<AudioSource>();
		audio.clip = clip;
		audio.enabled = false;
		audio.enabled = true;

		sound.AddComponent<DestroyTimer>();
	}
}
