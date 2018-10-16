using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{

    [SerializeField]
    private GameManager pGameManager;
    [SerializeField]
    private GameObject gPauseUI;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PauseUIActive();
    }

    void PauseUIActive()//ポーズUIの管理
    {
        if (pGameManager.eGameMode == GameManager.GAMEMODE.Pause)
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
        pGameManager.eGameMode = GameManager.GAMEMODE.Pause;
    }

    public void PauseEnd()//ポーズをやめる
    {
        pGameManager.eGameMode = GameManager.GAMEMODE.Game;
    }
}
