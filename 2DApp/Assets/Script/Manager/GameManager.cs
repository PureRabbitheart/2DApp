using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public enum GAMEMODE
    {
        Game,//ゲーム中
        Pause,//ポーズ中
        Over,//ゲームオーバー
        Clear,//ゲームクリア
    }

    public GAMEMODE eGameMode;//ゲームモードの管理

    [SerializeField]
    private GameObject gPauseUI;
    [SerializeField]
    private GameObject gClearUI;
    [SerializeField]
    private GameObject gOverUI;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseUIActive();
        ClearUIActive();
        OverUIActive();
    }

    void ClearUIActive()//ゲームクリアUIの管理
    {
        if (eGameMode == GAMEMODE.Clear)
        {
            gClearUI.SetActive(true);
        }
        else
        {
            gClearUI.SetActive(false);
        }
    }

    void OverUIActive()//ゲームオーバーUIの管理
    {
        if (eGameMode == GAMEMODE.Over)
        {
            gOverUI.SetActive(true);
        }
        else
        {
            gOverUI.SetActive(false);
        }
    }

    void PauseUIActive()//ポーズUIの管理
    {
        if (eGameMode == GAMEMODE.Pause)
        {
            gPauseUI.SetActive(true);
        }
        else
        {
            gPauseUI.SetActive(false);
        }
    }

    public void PauseStart()//ポーズをする
    {
        eGameMode = GAMEMODE.Pause;
    }

    public void PauseEnd()//ポーズをやめる
    {
        Invoke("GameMode", 0.1f);
    }

    void GameMode()//ゲームモードに遷移
    {
        eGameMode = GAMEMODE.Game;
    }
}
