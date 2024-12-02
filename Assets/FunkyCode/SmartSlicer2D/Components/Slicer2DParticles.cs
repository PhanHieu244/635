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

public class Slicer2DParticles : MonoBehaviour {

	void Start () {
		Slicer2D slicer = GetComponent<Slicer2D>();
		if (slicer != null) {
			slicer.AddResultEvent(SliceEvent);
		}
	}
	
	void SliceEvent(Slice2D slice) {
		Slicer2DParticlesManager.Instantiate();
		
		float posZ = transform.position.z - 0.1f;
			
		foreach(List<Vector2D> pointList in slice.slices) {
			foreach(Pair2D p in Pair2D.GetList(pointList)) {
				Particle2D firstParticle = Particle2D.Create(Random.Range(0, 360), new Vector3((float)p.A.x, (float)p.A.y, posZ));
				Slicer2DParticlesManager.particlesList.Add(firstParticle);

				Particle2D lastParticle = Particle2D.Create(Random.Range(0, 360), new Vector3((float)p.B.x, (float)p.B.y, posZ));
				Slicer2DParticlesManager.particlesList.Add(lastParticle);

				Vector2 pos = p.A.ToVector2();
				while (Vector2.Distance(pos, p.B.ToVector2()) > 0.5f) {
					pos = Vector2.MoveTowards(pos, p.B.ToVector2(), 0.35f);
					Particle2D particle = Particle2D.Create(Random.Range(0, 360), new Vector3(pos.x, pos.y, posZ));
					Slicer2DParticlesManager.particlesList.Add(particle);
				}
			}
		} 
	}
}
