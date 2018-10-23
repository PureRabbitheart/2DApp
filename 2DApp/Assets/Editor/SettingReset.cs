#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
using System.Text;
using System;
using System.Reflection;

public class GameSetting
{

#if UNITY_EDITOR
    [MenuItem("Tools/Reset/SettingReset")]
#endif

    static void SettingReset()
    {
        PlayerPrefsX.SetBool("InitActive", false);//初期起動のフラグ
        PlayerPrefs.SetFloat("BGMVolume", 5f);//BGMの音量のデータ作成
        PlayerPrefs.SetFloat("SEVolume", 5f);//SEの音量のデータ作成
        PlayerPrefs.SetInt("Lemon", 0);//レモンの数
        PlayerPrefs.SetInt("AP", 20);//AP
        PlayerPrefs.SetString("Language", "Japanese");//言語設定
        PlayerPrefs.SetString("RcoveryTime", DateTime.Now.ToString());
        Debug.Log("ゲームの設定を初期化しました");
    }


#if UNITY_EDITOR
    [MenuItem("Tools/Reset/ALLDataDelete")]
#endif
    static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("すべてのデータを消しました");
    }

}