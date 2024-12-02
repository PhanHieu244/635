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
using System.Linq;
using UnityEngine;

public class ThinSliceBall : MonoBehaviour {
	private Vector2 direction;
	public float speed = 0.1f;

	static private List<ThinSliceBall> ballList = new List<ThinSliceBall>();

	static public List<ThinSliceBall> GetList() {
		return(new List<ThinSliceBall>(ballList));
	}

	void OnEnable() {
		ballList.Add (this);
	}

	void OnDisable() {
		ballList.Remove (this);
	}

	void Start () {
		SetDirection(Random.insideUnitCircle);
	}
	
	void Update () {
		UpdateMovement();

		UpdateSlicerCollision();
	}

	void SetDirection(Vector3 newDirection) {
		direction = newDirection;
		direction.Normalize();
	}

	// This manages ball movement and collisions with level walls
	void UpdateMovement() {
		transform.Translate(direction * speed);

		float ballSize = 1;

		// Balls vs Map Collisions
		foreach(Slicer2D slicer in Slicer2D.GetList()) {
			foreach (Pair2D id in Pair2D.GetList(slicer.GetPolygon().ToWorldSpace(slicer.transform).pointsList)) {
				if (Math2D.LineIntersectCircle(id, new Vector2D(transform.position), ballSize) == true) {
					transform.Translate(direction * -speed);
					SetDirection(Math2D.ReflectAngle(direction, (float)Vector2D.Atan2(id.A, id.B)));
					transform.Translate(direction * speed);
				}
			}
		}

		// Balls vs Balls Collision
		foreach(ThinSliceBall ball in ThinSliceBall.GetList()) {
			if (ball == this) {
				continue;
			}

			if (Vector2.Distance(transform.position, ball.transform.position) < ballSize + ballSize) {
				ball.direction = Vector2D.RotToVec(Vector2D.Atan2(transform.position, ball.transform.position)- Mathf.PI).ToVector2();
				direction = Vector2D.RotToVec(Vector2D.Atan2(transform.position, ball.transform.position)).ToVector2();
				
				ball.transform.Translate(ball.direction * ball.speed);
				transform.Translate(direction * speed);
			}
		}
	}

	// Ball vs Slice Collision
	public void UpdateSlicerCollision() {
		float ballSize = 1;

		if (Math2D.LineIntersectCircle(Slicer2DController.GetPair(), new Vector2D(transform.position), ballSize)) {
			ThinSliceGameManager.CreateParticles();

			// Remove Current Slicing Process
			Slicer2DController.ClearPoints();
		}
	}

	// Check if polygon has ball objects inside
	public static bool PolygonHasBallsInside(Polygon2D poly) {
		foreach(ThinSliceBall ball in ThinSliceBall.GetList()) {
			if (poly.PointInPoly(new Vector2D(ball.transform.position)) == true) {
				return(true);
			}
		}
		return(false);
	}
}
