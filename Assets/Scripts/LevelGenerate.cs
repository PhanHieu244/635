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

public class LevelGenerate : MonoBehaviour
{
    public RectTransform levelRoot;

    public GameObject levelItem;

    public int row;

    // Start is called before the first frame update
    void Start()
    {
        GenLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenLevel()
    {
        for(int i = 0; i < row * 2;i++)

        {
            GameObject _level =(GameObject) Instantiate(levelItem, Vector3.zero, Quaternion.Euler(0, 0, 0));
            _level.GetComponent<RectTransform>().parent = levelRoot;
            if(i%2 ==0)
            {
                _level.GetComponent<RectTransform>().localPosition = new Vector3(-48.0f ,-108.0f * (i + 1),0);
            }
            else
            {
                _level.GetComponent<RectTransform>().localPosition = new Vector3(48.0f, -108.0f * (i + 1), 0);
            }
            _level.GetComponent<RectTransform>().localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
    }
}
