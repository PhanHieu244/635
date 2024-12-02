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

public class Slicer2DParticlesManager : MonoBehaviour {
	public int particlesCount = 0;
	static public Slicer2DParticlesManager instance;

	static public List<Particle2D> particlesList = new List<Particle2D>();

	static public void Instantiate() {
		if (instance != null) {
			return;
		}

		GameObject manager = new GameObject();
		manager.name = "Slicer2D Particles";

		instance = manager.AddComponent<Slicer2DParticlesManager>();
	}

	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			if (Slicer2D.Debug.enabled) {
				Debug.LogWarning("Slicer2D: Multiple Particle Managers Detected!");
			}
			Destroy(this);
		}
	}

	void Update() {
		foreach(Particle2D particle in new List<Particle2D>(particlesList)) {
			if (particle.Update() == true) {
				particle.Draw();
			} else {
				particlesList.Remove(particle);
			}
		}

		particlesCount = particlesList.Count;
	}
}
