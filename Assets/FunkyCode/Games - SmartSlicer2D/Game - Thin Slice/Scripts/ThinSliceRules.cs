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

public class ThinSliceRules : MonoBehaviour {
	static int cutObjects = 0;

	void Start () {
		Slicer2D slicer = GetComponent<Slicer2D>();
		slicer.AddEvent(OnSlice);
		slicer.AddResultEvent(AfterSlice);
	}
	
	// Triggered Before Every Slice // Should we perform slice? Is it succesful according our rules?
	bool OnSlice(Slice2D sliceResult) {
		Polygon2D CutObject = GetCutPolygon(sliceResult);

		// Forbidden double slices // Shouldn't be possible with linear slicer
		if (sliceResult.polygons.Count > 2) {
			return(false);
		}

		// Add Particles if slice is succesful
		if (CutObject == null) {
			ThinSliceGameManager.CreateParticles();
			Slicer2DController.ClearPoints();
			return(false);
		}

		return(true);	
	}

	// Triggered On Every Successful Slice // Manage Game Objects
	void AfterSlice(Slice2D sliceResult) {
		GameObject cutObject = GetCutGameObject(sliceResult);
		
		if (cutObject != null) {
			RemoveGameObject(cutObject);

			foreach(GameObject g in sliceResult.gameObjects) {
				if (g != cutObject) {
					g.name = name;
				}
			}
		}

		ThinSliceGameManager.UpdateLevelBar();
	}

	// After Slice - Get smallest polygon which does not have balls in it
	GameObject GetCutGameObject(Slice2D sliceResult) {
		double area = 1e+10f;
		GameObject CutObject = null;
		foreach(GameObject resultObject in sliceResult.gameObjects) {
			Polygon2D poly = Polygon2DList.CreateFromGameObject(resultObject)[0];
			if (poly.GetArea() < area && ThinSliceBall.PolygonHasBallsInside(poly.ToWorldSpace(resultObject.transform)) == false) {
				CutObject = resultObject;
				area = poly.GetArea();
			}
		}
		return(CutObject);
	}

	// Before Slice - Get smallest polygon which does not have balls in it
	Polygon2D GetCutPolygon(Slice2D sliceResult) {
		double area = 1e+10f;
		Polygon2D CutObject = null;
		foreach(Polygon2D poly in sliceResult.polygons) {
			if (poly.GetArea() < area && ThinSliceBall.PolygonHasBallsInside(poly) == false) {
				CutObject = poly;
				area = poly.GetArea();
			}
		}
		return(CutObject);
	}

	// Polygon Removal
	void RemoveGameObject(GameObject CutObject) {
		Slicer2D.explosionPieces = 5;

		Rigidbody2D rigidBody2D = CutObject.AddComponent<Rigidbody2D>();
		rigidBody2D.AddForce(new Vector2(0, 200));
		rigidBody2D.AddTorque(Random.Range(-15, 15));

		cutObjects ++;
		CutObject.transform.Translate(0, 0, 100 - cutObjects + CutObject.transform.position.z);

		CutObject.AddComponent<Mesh2D>().material = CutObject.GetComponent<Slicer2D>().material;
	

		CutObject.AddComponent<DestroyTimer>();

		Destroy(CutObject.GetComponent<Slicer2D>());
		Destroy(CutObject.GetComponent<ThinSliceRules>());
	}
}
