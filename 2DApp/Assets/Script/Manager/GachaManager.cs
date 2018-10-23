using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GachaManager : MonoBehaviour
{

    public enum SCENEMODE
    {
        Menu,//ガチャメニュー
        Pickup,//ピックアップ詳細
        Confirmation,//確認画面
        Gacha,//ガチャ
        GachaNG,//ガチャ引けない時
    }

    public SCENEMODE eSceneMode;

    [SerializeField]
    private GameObject gGachaConfirmation;//ガチャの確認画面
    [SerializeField]
    private GameObject gGachaNG;//レモンが足りない時
    [SerializeField]
    private TextMeshProUGUI[] LemonValue = new TextMeshProUGUI[2];//現在のレモンの数を出すText
    [SerializeField]
    private TextMeshProUGUI NextLemonValue;//ガチャ引いた後にレモンの数を出すText
    [SerializeField]
    private int oneGachaLemon;//一回引くのに使うレモンの数
    [SerializeField]
    private NextScene pNextScene;

    private int nextLemonValue;//ガチャ引いた後に残るレモンの数
    private bool isGachaPull;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("Lemon", 5);
        LemonValueUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        ConfirmationUIActive();
        GahaNGUIActive();
    }

    void ConfirmationUIActive()//言語設定UIの管理処理
    {
        if (eSceneMode == SCENEMODE.Confirmation)
        {
            gGachaConfirmation.SetActive(true);
        }

    }
    void GahaNGUIActive()//レモンが足りない時の確認画面
    {
        if (eSceneMode == SCENEMODE.GachaNG)
        {
            gGachaNG.SetActive(true);
        }
    }
    public void GachaCheck(int value)//ガチャボタンを押したら
    {
        eSceneMode = SCENEMODE.Confirmation;

        NextLemonValueUpdate(value);
    }

    public void Cancel()//キャンセルボタンを押したら
    {
        eSceneMode = SCENEMODE.Menu;
        gGachaConfirmation.SetActive(false);

    }

    void LemonValueUpdate()//今持っているレモンの数を表示する
    {
        int lemonValue = PlayerPrefs.GetInt("Lemon");
        for (int i = 0; i < LemonValue.Length; i++)
        {
            LemonValue[i].text = "× " + lemonValue.ToString();
        }
    }

    void NextLemonValueUpdate(int value)//ガチャ引いた後に残るレモンの数
    {
        nextLemonValue = PlayerPrefs.GetInt("Lemon");
        nextLemonValue -= value * oneGachaLemon;
        if (nextLemonValue < 0)//ガチャ引こうとしてレモンが0以下になった時
        {
            nextLemonValue = 0;
            isGachaPull = false;
        }
        else
        {
            isGachaPull = true;
        }
        NextLemonValue.text = "× " + nextLemonValue.ToString();
    }

    public void GachaPull()//ガチャを引くボタンを押したらの処理
    {
        if (isGachaPull == true)//ガチャ引けるとき
        {
            int lemonValue = PlayerPrefs.GetInt("Lemon");
            int GachaValue = (lemonValue - nextLemonValue) / oneGachaLemon;//何回引くか計算で算出
            PlayerPrefs.SetInt("Lemon", nextLemonValue);//持っているレモンの数を減らす   

            pNextScene.PerfomanceScene();
        }
        else//ガチャ引けない時
        {
            eSceneMode = SCENEMODE.GachaNG;
        }

    }

    public void GachaNGClose()//レモンが足りない時の確認画面を閉じるボタンを押したときの処理
    {
        eSceneMode = SCENEMODE.Gacha;
        gGachaNG.SetActive(false);

    }
}
