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
public class Shop : MonoBehaviour
{
    public Text coinTxt;

    // Start is called before the first frame update
    void Start()
    {
        ShowCoinValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void ShowCoinValue()
    {
        int _coin = PlayerPrefs.GetInt("Coin");
        coinTxt.text = _coin.ToString();
    }

    public void BuyCoin(int _pack)
    {
        AudioManager.Instance.PlayBtn();
        switch (_pack)
        {
            case 1:
                MoreCoin(900);
                break;
            case 2:
                MoreCoin(2000);
                break;
            case 3:
                MoreCoin(4000);
                break;
            case 4:
                MoreCoin(8000);
                break;
            default:
                break;
        }
    }

    public void MoreCoin(int _value)
    {
        int _coin = PlayerPrefs.GetInt("Coin");
        _coin += _value;
        coinTxt.text = _coin.ToString();
        PlayerPrefs.SetInt("Coin",_coin);
    }
}
