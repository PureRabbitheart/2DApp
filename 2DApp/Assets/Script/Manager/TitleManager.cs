using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer AudioMixer;//AudioMixerの保管

    public enum TITLEMODE
    {
        Title,
        Credit,
    }
    public TITLEMODE eTitleMode;

    [SerializeField]
    private GameObject gCreditUI;//CreditのUIを管理

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
    }

    void CreditUIActive()//CreditUIの管理処理
    {
        if (eTitleMode == TITLEMODE.Credit)
        {
            gCreditUI.SetActive(true);
        }
        else
        {
            gCreditUI.SetActive(false);
        }
    }

    void InitSetting()//ゲームの初期起動設定をする
    {
        PlayerPrefsX.SetBool("InitActive", true);//初期起動のフラグ

        PlayerPrefs.SetFloat("BGMVolume", 5f);//BGMの音量のデータ作成
        PlayerPrefs.SetFloat("SEVolume", 5f);//SEの音量のデータ作成
        AudioMixer.SetFloat("SEVol", 5f);
        AudioMixer.SetFloat("BGMVol", 5f);
    }

    public void CreditStart()//Creditモードへ移行
    {
        eTitleMode = TITLEMODE.Credit;
    }

    public void CreditEnd()//Creditモード終了
    {
        eTitleMode = TITLEMODE.Title;
    }
}
