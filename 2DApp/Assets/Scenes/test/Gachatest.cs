using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Gachatest : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI debug;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Gacha()
    {
        debug.text = Gacha(Random.Range(0.000f, 1.0f));
        Debug.Log(debug.text);
    }

    string Gacha(float probability)
    {
        if (probability < 0.01f)//0.01
        {
            return "大当たり_SSR";
        }
        if (probability < 0.11f)//0.1
        {
            return "中あたり_SR";
        }
        if (probability < 0.41f)//0.3
        {
            return "あたり_R";
        }
        return "はずれ";

    }
}
