using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer AudioMixer;//AudioMixerの保管

    void Update()
    {
        Debug.Log("SE"+ PlayerPrefs.GetFloat("SEVolume"));
        Debug.Log("BGM" + PlayerPrefs.GetFloat("BGMVolume"));
    }

    public void SetBGM(float volume)//BGMのSliderを動かしたときに呼び出す　引数…ボリューム
    {
        AudioMixer.SetFloat("BGMVol", volume);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSE(float volume)//SEのSliderを動かしたときに呼び出す　引数…ボリューム
    {
        AudioMixer.SetFloat("SEVol", volume);
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

 
}
