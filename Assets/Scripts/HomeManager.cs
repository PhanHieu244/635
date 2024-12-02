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

public class HomeManager : MonoBehaviour
{
    public GameObject homePanel, levelSelectorPanel, settingPanel,shopPanel;

    public Text coinTxt;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        ShowCoinValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLevelSelector()
    {
        homePanel.SetActive(false);
        levelSelectorPanel.SetActive(true);
        settingPanel.SetActive(false);
        shopPanel.SetActive(false);
        AudioManager.Instance.PlayBtn();
    }
    public void ShowSetting()
    {
        homePanel.SetActive(false);
        levelSelectorPanel.SetActive(false);
        settingPanel.SetActive(true);
        shopPanel.SetActive(false);
        AudioManager.Instance.PlayBtn();
    }
    public void ShowShop()
    {
        homePanel.SetActive(false);
        levelSelectorPanel.SetActive(false);
        settingPanel.SetActive(false);
        shopPanel.SetActive(true);
        AudioManager.Instance.PlayBtn();
    }
    public void PlayGame()
    {
        AudioManager.Instance.PlayBtn();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        AudioManager.Instance.PlayBtn();
    }

    public void BackToHome()
    {
        homePanel.SetActive(true);
        levelSelectorPanel.SetActive(false);
        settingPanel.SetActive(false);
        shopPanel.SetActive(false);
        AudioManager.Instance.PlayBtn();
    }

    void ShowCoinValue()
    {
        int _coin = PlayerPrefs.GetInt("Coin");
        coinTxt.text = _coin.ToString();
    }
}
