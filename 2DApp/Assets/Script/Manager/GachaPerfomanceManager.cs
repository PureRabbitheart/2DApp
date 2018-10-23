using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaPerfomanceManager : MonoBehaviour
{

    enum MODE
    {
        Recovery,//人回収演出
        Details,//中身詳細
    }

    private MODE eSceneMode;


    [SerializeField]
    private GameObject Fade;//フェードオブジェクト




    [SerializeField]
    private GameObject Light;//ライトのオブジェクト
    [SerializeField]
    private GameObject UFO;//UFOのオブジェクト
    [SerializeField]
    private Vector3 vEndPosition;//止まる地点のポジション
    [SerializeField]
    private GameObject[] Human = new GameObject[20];//人オブジェクト

    List<int> Number = new List<int>();//移動される人の配列番号管理
    private float fUFOTime;//UFOの移動時間管理
    private float fHumanTime;//人の移動時間管理
    private Vector3[] vHumanStartPosition = new Vector3[20];//人の立っている位置
    private Vector3 vUFOStartPosition;//UFOの開始地点
    private bool isStart;//演出開始フラグ
    private bool isRecovery;//回収フラグ

    public int GachaNum = 10;//何回回すか


    void Start()
    {
        Light.SetActive(false);
        HumanInit();
        StartCoroutine(RandamValue(GachaNum));//ランダムでガチャの回す分数字を選ぶ
    }

    void Update()
    {
        switch(eSceneMode)
        {
            case MODE.Details:
                break;
            case MODE.Recovery:
                RecoveryUpdate();
                break;
        }
    }

    void RecoveryUpdate()
    {
        if (isStart == true && isRecovery == false)//タップされてUFO移動までの処理
        {
            UFOMove();
        }
        if (isRecovery == true)//人の回収
        {
            for (int i = 0; i < GachaNum; i++)
            {
                HumanMove(Number[i]);
            }
        }
    }

    void UFOInit()
    {
        vUFOStartPosition = UFO.transform.position;
    }

    void UFOMove()//UFOの移動
    {
        fUFOTime += Time.deltaTime;
        if (fUFOTime > 1.0f)
        {
            UFO.transform.position = vEndPosition;
            Invoke("LightON", 0.5f);

        }
        var rate = fUFOTime / 1.0f;
        UFO.transform.position = Vector3.Lerp(vUFOStartPosition, vEndPosition, rate);
    }

    void LightON()//ライトの点灯
    {
        Light.SetActive(true);
        isRecovery = true;
        HumanSOS();
        Fade.GetComponent<Animator>().SetBool("Fade", true);

    }

    void HumanInit()//人のスタートポジション
    {
        for (int i = 0; i < Human.Length; i++)
        {
            vHumanStartPosition[i] = Human[i].transform.position;
        }
    }

    void HumanMove(int num)//引数に応じて人を回収する
    {
        fHumanTime += Time.deltaTime;
        if (fHumanTime > 6.0f)
        {
            Human[num].transform.position = vEndPosition;
            Destroy(Human[num]);
            Human[num] = new GameObject();
            Light.SetActive(false);
            if (UFO.transform.position != vUFOStartPosition)
            {
                UFO.transform.position += new Vector3(0, 0.05f, 0);
            }
        }
        var rate = fHumanTime / 6.0f;
        Human[num].transform.position = Vector3.Lerp(vHumanStartPosition[num], vEndPosition, rate);
    }

    IEnumerator RandamValue(int num)//引数の数分どいつを回収するか決める
    {
        Number.Add(Random.Range(0, Human.Length));//１つ目の固定を決める
        while (true)//数分回す
        {
            int quizNum = Random.Range(0, Human.Length);
            bool isReset = false;

            for (int i = 0; i < Number.Count; i++)//記録している分回す
            {
                if (Number[i] == quizNum)//同じ数字があったら
                {
                    isReset = true;
                    break;//保存しない
                }
            }
            if (isReset == false)
            {
                Number.Add(quizNum);//保存する
            }

            if (Number.Count == num)
            {
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void HumanSOS()//人の移動処理
    {
        List<int> HumanSOSNum = new List<int>();//移動されない人の配列番号管理


        for (int i = 0; i < Human.Length; i++)
        {
            for (int j = 0; j < Number.Count; j++)
            {
                if (i == Number[j])
                {
                    break;
                }
            }
            HumanSOSNum.Add(i);
        }

        for (int m = 0; m < HumanSOSNum.Count; m++)
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                Human[HumanSOSNum[m]].GetComponent<Animator>().SetTrigger("Left");

            }
            else
            {
                Human[HumanSOSNum[m]].GetComponent<Animator>().SetTrigger("Right");

            }
        }
    }

    public void TapGacha()//タップしたとき
    {
        UFOInit();
        isStart = true;
    }
}
