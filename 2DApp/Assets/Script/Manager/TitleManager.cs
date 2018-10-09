using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer AudioMixer;//AudioMixerの保管


    void Awake()
    {
        Init();//初期化
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()//初期化
    {
        bool isInit = PlayerPrefsX.GetBool("InitActive");
        if (isInit == false)//初期起動なら
        {
            InitSetting();//初期起動設定を実行する
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
}
