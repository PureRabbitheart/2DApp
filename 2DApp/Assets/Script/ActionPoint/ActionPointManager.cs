using UnityEngine;
using System;
using System.Collections;

public class ActionPointManager : MonoBehaviour
{

    
    const int recoveryUnitSeconds = 10;// 1ActionPoint回復をするのに必要な時間

    public int nowPoint { get; private set; }//現在のAP数
    public int maxPoint { get; private set; }
    private double restRecoveryTime;
    private DateTime lastRcoveryTime;


    void Update()
    {
        UpdateActionPointStatus();// 毎フレームAPの更新を行う
    }


    public void Init(int point, int maxpoint, DateTime lastRecoveryTime)  /// 初期化処理 …現在のポイント,最大ポイント,最後に回復した時間
    {
        nowPoint = point;
        maxPoint = maxpoint;
        lastRcoveryTime = lastRecoveryTime;
    }

    public bool UseActionPoint(int usePoint)// APを使用する
    {

        if (nowPoint < usePoint)
        {
            return false;
        }

        if (nowPoint >= maxPoint)
        {
            lastRcoveryTime = DateTime.Now;
        }

        nowPoint -= usePoint;

        return true;
    }


    public void RecoveryAllPoint()// APを全回復する
    {
        nowPoint += maxPoint;
    }

    public string GetRestRecoveryTimeLabel()// 次に回復する時間までのカウントダウン用ラベルを返す
    {
        if (nowPoint >= maxPoint)
        {
            return "00:00";
        }
        var span = TimeSpan.FromSeconds(restRecoveryTime);
        return string.Format("{0:00}:{1:00}", span.Minutes, span.Seconds);
    }

    
    public float ActionPointRatio()// 全体のポイントに対しての現在のポイントの割合を返す
    {
        if (nowPoint >= maxPoint)
        {
            return 1f;
        }

        if (nowPoint == 0)
        {
            return 0f;
        }

        return (float)nowPoint / maxPoint;
    }
   
    void UpdateActionPointStatus()// APの更新処理を行う
    {

        if (nowPoint >= maxPoint)//MAX値超えていたら
        {
            return;
        }

        var time = DateTime.Now;
        var diff = time - lastRcoveryTime;

        var totalSeconds = diff.TotalSeconds;

        while (totalSeconds > recoveryUnitSeconds)
        {
            if (nowPoint >= maxPoint)
            {
                break;
            }

            totalSeconds -= recoveryUnitSeconds;
            lastRcoveryTime = lastRcoveryTime.Add(TimeSpan.FromSeconds(recoveryUnitSeconds));
            nowPoint++;
        }

        restRecoveryTime = recoveryUnitSeconds - totalSeconds;
    }
}