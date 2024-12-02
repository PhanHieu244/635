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

public class SlicePointGameManager : MonoBehaviour {

	public double startingArea = 0;
	public double currentArea = 0;

	public Text percentText;
	
	void Start () {
		
		UpdateCurrentArea();

		startingArea = currentArea;
	}
	
	void Update () {
		Polygon2D cameraPolygon = Polygon2D.CreateFromCamera(Camera.main);
		cameraPolygon = cameraPolygon.ToRotation(Camera.main.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
		cameraPolygon = cameraPolygon.ToOffset(new Vector2D(Camera.main.transform.position));
	
		foreach(Slicer2D slicer in Slicer2D.GetList()) {
			if (Math2D.PolyCollidePoly(slicer.GetPolygon().ToWorldSpace(slicer.transform), cameraPolygon) == false) {
				Destroy(slicer.gameObject);
			}
		}

		UpdateCurrentArea();
 
		int percent = (int)((currentArea / startingArea) * 100);
		percentText.text = "Left: " + percent + "%";
	}

	public void UpdateCurrentArea() {
		currentArea = 0f;
		foreach(Slicer2D slicer in Slicer2D.GetList()) {
			currentArea += slicer.GetPolygon().GetArea();
		}
	}
}
