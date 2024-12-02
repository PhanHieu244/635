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

public class VirtualSpriteRenderer {
	public Sprite sprite;

	public int sortingLayerID = 0;
	public string sortingLayerName = "";
	public int sortingOrder = 0;

	public Color color;
	public Material material;

	public bool flipX = false;
	public bool flipY = false;

	public VirtualSpriteRenderer(SpriteRenderer spriteRenderer) {
		sprite = spriteRenderer.sprite;

		sortingLayerID = spriteRenderer.sortingLayerID;
		sortingLayerName = spriteRenderer.sortingLayerName;
		sortingOrder = spriteRenderer.sortingOrder;

		flipX = spriteRenderer.flipX;
		flipY = spriteRenderer.flipY;

		material = new Material(spriteRenderer.sharedMaterial);

		color = spriteRenderer.color;
	}
}


		

