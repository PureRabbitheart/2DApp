using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Japanese;
    [SerializeField]
    private GameObject English;


    void Awake()
    {
        LanguageChange();//言語の切り替え
    }

    public void LanguageChange()//言語の切り替え
    {
        string language = PlayerPrefs.GetString("Language");
        switch(language)//どの言語か識別する
        {
            case "Japanese"://日本語を選んでいるなら
                Japanese.SetActive(true);
                English.SetActive(false);
                break;

            case "English"://英語を選んでいるなら
                Japanese.SetActive(false);
                English.SetActive(true);
                break;
        }
    }
}
