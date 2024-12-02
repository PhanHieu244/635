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

public class PopulateGrid : MonoBehaviour
{
    public GameObject prefab; // This is our prefab object that will be exposed in the inspector

    public int numberToCreate; // number of objects to create. Exposed in inspector

    void Start()
    {
        //unlock level 1
        PlayerPrefs.SetInt("Level1",1);
        Populate();
    }

    void Update()
    {

    }

    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < numberToCreate; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(prefab, transform);

            newObj.name = "Level" + (i+1).ToString();
            newObj.GetComponent<LevelItem>().SetLevelText((i + 1));
            int _star = PlayerPrefs.GetInt("Star" + (i+1).ToString());
            int _lock = PlayerPrefs.GetInt("Level" + (i + 1).ToString());
            newObj.GetComponent<LevelItem>().SetStar(_star);
            newObj.GetComponent<LevelItem>().SetLock(_lock);
            newObj.GetComponent<LevelItem>().levelIndex = i + 1;
        }

    }
}
