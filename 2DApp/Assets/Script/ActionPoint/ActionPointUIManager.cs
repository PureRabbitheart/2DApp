using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System;


public class ActionPointUIManager : MonoBehaviour
{

    [SerializeField]
    private ActionPointManager pAPManager;
    [SerializeField]
    private TextMeshProUGUI recoveryTimeLabel;
    [SerializeField]
    private TextMeshProUGUI pStatusLabel;
    [SerializeField]
    private Slider APBar;

    void Start()
    {
        DateTime time = DateTime.Parse(PlayerPrefs.GetString("RcoveryTime"));
        int nowAP = PlayerPrefs.GetInt("AP");
        pAPManager.Init(nowAP, 20, time);
    }

    void Update()
    {
        recoveryTimeLabel.text = pAPManager.GetRestRecoveryTimeLabel();
        pStatusLabel.text = string.Format("{0:00}/{1:00}", pAPManager.nowPoint, pAPManager.maxPoint);
        APBar.value = pAPManager.ActionPointRatio();
    }

}