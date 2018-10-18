using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SelectManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI LemonText;

    void Awake()
    {
        Init();
    }


    void Init()
    {
        int lemonValue = PlayerPrefs.GetInt("Lemon");
        LemonText.text = "× " + lemonValue.ToString();
    }
}
