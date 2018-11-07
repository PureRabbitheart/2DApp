using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GachaEmission : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI debug;
    [SerializeField]
    private GachaData GachaDataFile;

    private int GachaCount;
    private int MaxCount = 100;

    private List<int>tmp;


    //展開する
    void LoadFile()
    {



    }


    public void Gacha(int value)//ボタンが押されたらの判定
    {
        GachaCount += value;
        debug.text = "";
        string []tmp = new string[value];
        for (int i = 0; i < value; i++)
        {
            tmp[i] = Gacha(Random.Range(0.000f, 1.0f));
          
        }
        for (int i = 0; i < value; i++)
        {
            debug.text += tmp[i] + "\n";
        }
    }

    string Gacha(float probability)//何が出るかの排出
    {
        if (probability < 0.01f || GachaCount > MaxCount - 1)//0.01
        {
            GachaCount = 0;
            return "大当たり_星5";
        }
        if (probability < 0.11f)//0.1
        {
            return "あたり_星4";
        }
        return "はずれ_星3";

    }
}
