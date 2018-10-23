using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SelectManager : MonoBehaviour
{
    enum MODE
    {
        Select,
        StageInfo,
    }

    MODE eSceneMode;

    [SerializeField]
    private TextMeshProUGUI InfoText;
    [SerializeField]
    private TextMeshProUGUI LemonText;//レモンの数

    void Awake()
    {
        Init();
    }


    void Init()
    {
        int lemonValue = PlayerPrefs.GetInt("Lemon");
        LemonText.text = "× " + lemonValue.ToString();
    }

    void StageInfo(int StageNum, int CostAP)
    {
        eSceneMode = MODE.StageInfo;
        InfoText.text = "消費APは" + CostAP.ToString();
    }
}
