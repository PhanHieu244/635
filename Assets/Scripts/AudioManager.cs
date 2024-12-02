/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件由会员免费分享，如果商用，请务必联系原著购买授权！

daily assets update for try.

U should buy a license from author if u use it in your project!
*/

using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public AudioSource characterSound, mainMusic;

    public AudioClip btnSfx, coinSfx,boomSfx,ballonSfx,waterDropSfx,clearSfx;

	

	public static AudioManager Instance { get { return instance; } }

	private static AudioManager instance;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		} else
			DestroyImmediate (this.gameObject);
        
	}

	public void PlayBtn()
	{
		characterSound.PlayOneShot (btnSfx, 1.0f);
	}

    public void PlayCoin()
    {
        characterSound.PlayOneShot(coinSfx, 1.0f);
    }

    public void PlayBoom()
    {
        characterSound.PlayOneShot(boomSfx, 1.0f);
    }

    public void PlayBallon()
    {
        characterSound.PlayOneShot(ballonSfx, 1.0f);
    }

    public void PlayWaterDrop()
    {
        characterSound.PlayOneShot(waterDropSfx, 1.0f);
    }

    public void PlayClear()
    {
        characterSound.PlayOneShot(clearSfx, 1.0f);
    }


    public void OffSound()
	{
		characterSound.mute = true;
        PlayerPrefs.SetInt("Sound", 1);

    }

	public void OnSound()
	{
		characterSound.mute = false;
        PlayerPrefs.SetInt("Sound", 0);

    }

    public void OffMusic()
    {

        mainMusic.mute = true;
        PlayerPrefs.SetInt("Music", 1);

    }

    public void OnMusic()
    {
       
        mainMusic.mute = false;
        PlayerPrefs.SetInt("Music", 0);

    }


}
