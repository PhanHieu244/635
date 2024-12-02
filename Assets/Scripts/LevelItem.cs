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

public class LevelItem : MonoBehaviour
{
    public Text levelText;

    public GameObject star1, star2, star3;

    public Image lockImage;

    public Sprite lockIcon, completeIcon;

    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelText(int _index)
    {
        levelText.text = _index.ToString();
    }

    public void SetStar(int _value)
    {
        if(_value == 0)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if(_value == 1)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if (_value == 2)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (_value == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
    }

    public void SetLock(int _lock)
    {
        if (_lock == 0)
            lockImage.sprite = lockIcon;
        else
            lockImage.sprite = completeIcon;
    }

    public void LoadLevel()
    {
        if(PlayerPrefs.GetInt("Level" + levelIndex.ToString()) == 1)
        {
            PlayerPrefs.SetInt("CurrentLevel", levelIndex);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }

    }
}
