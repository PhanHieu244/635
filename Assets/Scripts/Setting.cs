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

public class Setting : MonoBehaviour
{
    public Image soundImg, musicImg;

    public Sprite soundOn, soundOff, musicOn, musicOff;
    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSound()
    {
        if(PlayerPrefs.GetInt("Sound") == 0)
        {
            //off sound
            soundImg.sprite = soundOff;
            AudioManager.Instance.OffSound();
        }
        else
        {
            //on sound
            soundImg.sprite = soundOn;
            AudioManager.Instance.OnSound();
        }
    }


    public void ToggleMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            //off music
            musicImg.sprite = musicOff;
            AudioManager.Instance.OffMusic();
        }
        else
        {
            //on music
            musicImg.sprite = musicOn;
            AudioManager.Instance.OnMusic();
        }
    }

    void LoadInfo()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            //on sound
            soundImg.sprite = soundOn;
            AudioManager.Instance.OnSound();
        }
        else
        {
            //off sound
            soundImg.sprite = soundOff;
            AudioManager.Instance.OffSound();
        }


        if (PlayerPrefs.GetInt("Music") == 0)
        {
            //on music
            musicImg.sprite = musicOn;
            AudioManager.Instance.OnMusic();
        }
        else
        {
            //off music
            musicImg.sprite = musicOff;
            AudioManager.Instance.OffMusic();
        }
    }

}
