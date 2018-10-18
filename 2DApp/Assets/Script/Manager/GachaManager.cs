using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GachaManager : MonoBehaviour
{

    public enum SCENEMODE
    {
        Menu,//ガチャメニュー
        Pickup,//ピックアップ詳細
        Confirmation,//確認画面
        Gacha,//ガチャ
    }

    public SCENEMODE eSceneMode;

    [SerializeField]
    private GameObject gGachaConfirmation;//ガチャの確認画面
    [SerializeField]
    private TextMeshProUGUI[] LemonValue = new TextMeshProUGUI[2];
    [SerializeField]
    private TextMeshProUGUI NextLemonValue;
    [SerializeField]
    private int OneGachaLemon;
    // Use this for initialization
    void Start()
    {
        LemonValueUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        ConfirmationUIActive();
    }

    void ConfirmationUIActive()//言語設定UIの管理処理
    {
        if (eSceneMode == SCENEMODE.Confirmation)
        {
            gGachaConfirmation.SetActive(true);
        }
        else
        {
            gGachaConfirmation.SetActive(false);
        }
    }
    public void GachaCheck(int value)//ガチャボタンを押したら
    {
        eSceneMode = SCENEMODE.Confirmation;

        NextLemonValueUpdate(value);
    }

    public void Cancel()//キャンセルボタンを押したら
    {
        eSceneMode = SCENEMODE.Menu;
    }

    void LemonValueUpdate()
    {
        int lemonValue = PlayerPrefs.GetInt("Lemons");
        for (int i = 0; i < LemonValue.Length; i++)
        {
            LemonValue[i].text = "× " + lemonValue.ToString();
        }
    }

    void NextLemonValueUpdate(int value)//ガチャ引いた後に残るレモンの数
    {
        int lemonValue = PlayerPrefs.GetInt("Lemons");
        lemonValue -= value * OneGachaLemon;
        if (lemonValue < 0)
        {
            lemonValue = 0;
        }
        NextLemonValue.text = "× " + lemonValue.ToString();
    }

}
