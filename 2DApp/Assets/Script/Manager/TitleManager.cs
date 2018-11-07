using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{


    public enum SCENEMODE
    {
        Title,//タイトル
        Credit,//クレジット
        Language,//言語設定
    }
    public SCENEMODE eSceneMode;

    [SerializeField]
    private AudioMixer AudioMixer;//AudioMixerの保管
    [SerializeField]
    private GameObject gCreditUI;//クレジットのUIを管理
    [SerializeField]
    private GameObject gLanguageUI;//言語設定のUIを管理
    [SerializeField]
    private GameObject gNextSceneButton;


    void Awake()
    {
        Init();//初期化
    }


    void Init()//初期化
    {
        bool isInit = PlayerPrefsX.GetBool("InitActive");
        if (isInit == false)//初期起動なら
        {
            InitSetting();//初期起動設定を実行する
        }
    }

    void Update()
    {
        CreditUIActive();
        LanguageUIActive();
    }

    void CreditUIActive()//CreditUIの管理処理
    {
        if (eSceneMode == SCENEMODE.Credit)
        {
            gCreditUI.SetActive(true);
            gNextSceneButton.SetActive(false);
        }
        else
        {
            gCreditUI.SetActive(false);
            gNextSceneButton.SetActive(true);
        }
    }

    void LanguageUIActive()//言語設定UIの管理処理
    {
        if (eSceneMode == SCENEMODE.Language)
        {
            gLanguageUI.SetActive(true);
            gNextSceneButton.SetActive(false);
        }
        else
        {
            gLanguageUI.SetActive(false);
            gNextSceneButton.SetActive(true);
        }
    }

    void InitSetting()//ゲームの初期起動設定をする
    {
        PlayerPrefsX.SetBool("InitActive", true);//初期起動のフラグ

        PlayerPrefs.SetFloat("BGMVolume", 5f);//BGMの音量のデータ作成
        PlayerPrefs.SetFloat("SEVolume", 5f);//SEの音量のデータ作成
        PlayerPrefs.SetInt("Lemon", 0);//レモンの数
        PlayerPrefs.SetString("Language","Japanese");//言語設定

        AudioMixer.SetFloat("SEVol", 5f);
        AudioMixer.SetFloat("BGMVol", 5f);
    }

    public void CreditStart()//クレジットモードへ移行
    {
        eSceneMode = SCENEMODE.Credit;
    }

    public void TitleStart()//タイトルモードへ移行
    {
        eSceneMode = SCENEMODE.Title;
    }

    public void LanguageStart()//言語設定モードへ移行
    {
        eSceneMode = SCENEMODE.Language;
    }

    public void JapaneseSet()//日本語設定
    {
        PlayerPrefs.SetString("Language", "Japanese");//言語設定
        LanguageChange();
    }

    public void EnglishSet()//英語設定
    {
        PlayerPrefs.SetString("Language", "English");//言語設定
        LanguageChange();
    }

    void LanguageChange()
    {
        foreach (GameObject obj in UnityEngine.Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            var tmp =obj.GetComponent<LanguageManager>();
            if (tmp != null)
            {
                tmp.LanguageChange();
            }
        }
    }

    public void SettingReset()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefsX.SetBool("InitActive", false);//初期起動のフラグ
        PlayerPrefs.SetFloat("BGMVolume", 5f);//BGMの音量のデータ作成
        PlayerPrefs.SetFloat("SEVolume", 5f);//SEの音量のデータ作成
        PlayerPrefs.SetInt("Lemon", 0);//レモンの数
        PlayerPrefs.SetInt("AP", 20);//AP
        PlayerPrefs.SetString("Language", "Japanese");//言語設定
        PlayerPrefs.SetString("RcoveryTime", System.DateTime.Now.ToString());
    }
}
