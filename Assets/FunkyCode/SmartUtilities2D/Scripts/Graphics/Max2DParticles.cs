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

public class Max2DParticles : MonoBehaviour {
	public List<LineParticle> lines = new List<LineParticle>();
	public bool stripped = true;

	public static Max2DParticles CreateSliceParticles (List<Vector2D> slice) {
		GameObject newGameObject = new GameObject();
		newGameObject.name = "Particles";
		Max2DParticles particles = newGameObject.AddComponent<Max2DParticles>();

		foreach(Pair2D line in Pair2D.GetList(slice, false)) {
			particles.lines.Add(new LineParticle(line));
		}

		newGameObject.AddComponent<DestroyTimer>();

		return(particles);
	}
	
	public void OnRenderObject() {
		Max2D.lineMaterial = Max2D.lineLegacyMaterial;
		Max2D.lineMaterial.SetColor ("_Emission", Color.white);
		if (stripped) {
			foreach(LineParticle line in lines) {
				line.Update();
				List<Vector2D> list = new List<Vector2D>();
				list.Add(line.GetPair().A);
				list.Add(line.GetPair().B);
				Max2DLegacy.DrawStrippedLine (list, 1, -2);
			}
		} else {
			foreach(LineParticle line in lines) {
				line.Update();
				List<Vector2D> list = new List<Vector2D>();
				list.Add(line.GetPair().A);
				list.Add(line.GetPair().B);
				Max2DLegacy.DrawLine(line.GetPair().A, line.GetPair().B, -2);
			}
		}
		Max2D.lineMaterial = Max2D.lineNewMaterial;
		Max2D.lineMaterial.SetColor ("_Emission", Color.black);
	}
}

public class LineParticle {
	float direction;
	float length;
	Vector2 position;

	Vector2 velocity;
	float angularVelocity = 0;

	public LineParticle(Pair2D pair) {
		length = (float)Vector2D.Distance(pair.A, pair.B) / 2;
		direction = (float)Vector2D.Atan2(pair.A, pair.B);
		position = (pair.A + pair.B).ToVector2() / 2f;

		velocity = new Vector2(Random.Range(-0.1f, 0.1f), 0.1f);
		angularVelocity = Random.Range(-1, 1);
	}

	public Pair2D GetPair() {
		Vector2D A = new Vector2D(position.x + Mathf.Cos(direction) * length, position.y + Mathf.Sin(direction) * length);
		Vector2D B = new Vector2D(position.x + Mathf.Cos(direction) * -length, position.y + Mathf.Sin(direction) * -length);
		return(new Pair2D(A, B));
	}

	public void Update() {
		position += velocity * 0.2f;
		velocity.y += -0.02f;
		direction += angularVelocity / (180.0f / Mathf.PI);
	}
}
