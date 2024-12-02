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

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    public Text timerText,lvText1,lvText2;

    public GameObject pausePanel, pauseAnimation,gameOverPanel,gameClearPanel,levelSelectorPanel;

    public Image timerPr;

    public GameObject star1, star2, star3,starResult1, starResult2, starResult3;

    public double startingArea = 0;
    public double currentArea = 0;

    public Text percentText;

    public int sliceCount;

    int percent;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
       

        UpdateCurrentArea();

        startingArea = currentArea;
       
    }

    // Update is called once per frame
    void Update()
    {
      
        UpdateStar();

        Polygon2D cameraPolygon = Polygon2D.CreateFromCamera(Camera.main);
        cameraPolygon = cameraPolygon.ToRotation(Camera.main.transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
        cameraPolygon = cameraPolygon.ToOffset(new Vector2D(Camera.main.transform.position));

        foreach (Slicer2D slicer in Slicer2D.GetList())
        {
            if (Math2D.PolyCollidePoly(slicer.GetPolygon().ToWorldSpace(slicer.transform), cameraPolygon) == false)
            {
                Destroy(slicer.gameObject);
            }
        }

        UpdateCurrentArea();

        percent = (int)((currentArea / startingArea) * 100);
        percentText.text = percent + "%";
        timerPr.fillAmount = (float)((currentArea / startingArea));
    }

    public void Pause()
    {
        AudioManager.Instance.PlayBtn();
        pausePanel.SetActive(true);
        pauseAnimation.GetComponent<Animator>().SetBool("Open",true);
    }

    public void Resume()
    {
        AudioManager.Instance.PlayBtn();
        Time.timeScale = 1.0f;
        pauseAnimation.GetComponent<Animator>().SetBool("Open", false);
        pausePanel.SetActive(false);
    }

    public void ShowGameOver()
    {
        StartCoroutine(IEGameOver());
    }

    IEnumerator IEGameOver()
    {
        if(AudioManager.Instance != null)
        AudioManager.Instance.PlayBoom();
        yield return new WaitForSeconds(1.0f);
        gameOverPanel.SetActive(true);
        lvText1.text = "LEVEL " + (GameManager.instance.levelIndex + 1).ToString();
    }

    public void ShowGameClear()
    {
        StartCoroutine(IEGameClear());
    }

    IEnumerator IEGameClear()
    {
        yield return new WaitForSeconds(1.0f);
        if (AudioManager.Instance != null)
        AudioManager.Instance.PlayClear();
        gameClearPanel.SetActive(true);
        if (percent < 20)
        {
            PlayerPrefs.SetInt("Star" + (GameManager.instance.levelIndex + 1),3);
            starResult1.SetActive(true);
            starResult2.SetActive(true);
            starResult3.SetActive(true);
        }
        else if (percent >= 20 && percent < 40)
        {
            PlayerPrefs.SetInt("Star" + (GameManager.instance.levelIndex + 1), 2);
            starResult1.SetActive(true);
            starResult2.SetActive(true);
            starResult3.SetActive(false);
        }
        else if (percent >= 40 && percent < 50)
        {
            PlayerPrefs.SetInt("Star" + (GameManager.instance.levelIndex + 1), 1);
            starResult1.SetActive(true);
            starResult2.SetActive(false);
            starResult3.SetActive(false);
        }
        lvText2.text = "LEVEL " + (GameManager.instance.levelIndex + 1).ToString();
        PlayerPrefs.SetInt("Level"+ (GameManager.instance.levelIndex + 2).ToString(),1);
    }

    void UpdateStar()
    {
        if (sliceCount == 3)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (sliceCount == 2)
        {

            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (sliceCount == 1)
        {

            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else if (sliceCount == 0)
        {
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
    }
    public void RePlay()
    {

        Resume();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        //AudioManager.Instance.PlayBtnClick();
    }

    public void BackHome()
    {
        Resume();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        //AudioManager.Instance.PlayBtnClick();
    }

    public void NextLevel()
    {
        Resume();
        PlayerPrefs.SetInt("CurrentLevel", (GameManager.instance.levelIndex + 2));
        PlayerPrefs.SetInt("Level" + (GameManager.instance.levelIndex + 2),1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        //AudioManager.Instance.PlayBtnClick();
    }

    public void NextGameRewardVideo()
    {
        Resume();
        int _coin = PlayerPrefs.GetInt("Coin");
        if(_coin >= 100)
        {
            _coin -= 100;
            PlayerPrefs.SetInt("Coin", _coin);
            NextLevel();
        }

    }

    public void ShowLevel()
    {
        Resume();
        levelSelectorPanel.SetActive(true);
    }

    public void UpdateCurrentArea()
    {
        currentArea = 0f;
        foreach (Slicer2D slicer in Slicer2D.GetList())
        {
            currentArea += slicer.GetPolygon().GetArea();
        }
    }

    public void CheckLevel()
    {

        StartCoroutine(CheckLevelIE());
    }

    IEnumerator CheckLevelIE()
    {
        yield return new WaitForSeconds(5.0f);

        if (percent < 50)
            ShowGameClear();
        else
            ShowGameOver();

    }
}
