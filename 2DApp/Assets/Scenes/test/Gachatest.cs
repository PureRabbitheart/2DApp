using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Gachatest : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI debug;

    private int GachaCount;
    private int MaxCount = 110;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Gacha(int value)
    {
        for (int i = 0; i < value; i++)
        {
            debug.text = Gacha(Random.Range(0.000f, 1.0f));
            Debug.Log(debug.text);
            GachaCount++;
        }

    }



    string Gacha(float probability)
    {
        if (probability < 0.01f || GachaCount > MaxCount)//0.01
        {
            GachaCount = 0;
            return "大当たり_★5";
        }
        if (probability < 0.11f)//0.1
        {
            return "あたり_★4";
        }
        return "はずれ_★3";

    }
}
