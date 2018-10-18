using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{

    [SerializeField]
    private AudioMixer AudioMixer;//AudioMixerの保管
    [SerializeField]
    private Slider BGMSlider;//BGMのSlider
    [SerializeField]
    private Slider SESlider;//SEのSlider


    void Awake()
    {
        Init();//初期化
    }


    void Init()
    {
        VolumeLoad();//音量の読み込み
        SliderSet();//音量に合わせてボタンの位置を変える
    }

    void VolumeLoad()//ボリュームをロードする
    {
        AudioMixer.SetFloat("BGMVol", PlayerPrefs.GetFloat("BGMVolume"));
        AudioMixer.SetFloat("SEVol", PlayerPrefs.GetFloat("SEVolume"));
    }

    void SliderSet()//Sliderの初期設定
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SESlider.value = PlayerPrefs.GetFloat("SEVolume");
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
            var tmp = obj.GetComponent<LanguageManager>();
            if (tmp != null)
            {
                tmp.LanguageChange();
            }
        }
    }
}
