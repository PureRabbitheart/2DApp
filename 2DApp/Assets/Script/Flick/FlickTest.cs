using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlickTest : MonoBehaviour
{
    private Vector3 vTouchStartPos;//タップした位置
    private Vector3 vTouchEndPos;//画面を離した位置
    private Vector3 vStartPos;//移動時の最初の位置
    private Vector3 vEndPos;//移動目標
    private float fNowTime;//移動時間

    [SerializeField]
    private GameManager pGameManager;//GameManagerのポインタ
    [SerializeField]
    private LayerMask BackGround;//BackGroundのレイヤー管理
    [SerializeField, Range(0, 1)]
    float fMoveTime = 0.5f;//移動時間
    [SerializeField]
    private GameObject gPlayer;//プレイヤーのオブジェクト

    void Start()
    {
        vStartPos = gPlayer.transform.position;
        vEndPos = gPlayer.transform.position;

    }

    void Update()
    {
        if (pGameManager.eGameMode == GameManager.GAMEMODE.Game)
        {

            Flick();//フリック処理
            Move();//移動処理
        }
        else
        {
            vTouchStartPos = new Vector3(0, 0, 0);
            vTouchEndPos = new Vector3(0, 0, 0);
        }

    }

    void Flick()//フリック処理
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            vTouchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            vTouchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
    }

    void GetDirection()
    {
        float directionX = vTouchEndPos.x - vTouchStartPos.x;
        float directionY = vTouchEndPos.y - vTouchStartPos.y;
        string Direction = null;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
        {
            if (30 < directionY)
            {
                //上向きにフリック
                Direction = "up";
            }
            else if (-30 > directionY)
            {
                //下向きのフリック
                Direction = "down";
            }
        }
        else
        {
            //タッチを検出
            Direction = "touch";
        }

        switch (Direction)
        {
            case "up":
                //上フリックされた時の処理
                RayHit(new Vector2(0, 0.25f));
                MoveInit();
                break;

            case "down":
                //下フリックされた時の処理
                RayHit(new Vector2(0, -0.25f));
                MoveInit();
                break;

            case "right":
                //右フリックされた時の処理
                RayHit(new Vector2(0.25f, 0.0f));
                MoveInit();
                break;

            case "left":
                //左フリックされた時の処理
                RayHit(new Vector2(-0.25f, 0.0f));
                MoveInit();
                break;

            case "touch":
                //タッチされた時の処理
                break;

        }
    }


    void RayHit(Vector2 vPos)//フリックした時にRayを出して次行く位置を決める
    {

        RaycastHit2D hit = Physics2D.Raycast(gPlayer.transform.position, vPos, 100, BackGround);
        Debug.DrawRay(gPlayer.transform.position, new Vector3(0, 0, 100), Color.blue, 1);// 可視化

        if (hit.collider)
        {
            vEndPos = new Vector3(hit.point.x - vPos.x, hit.point.y - vPos.y, 0.0f);//目標地点を決定
        }

    }


    void MoveInit()//移動時の初期化
    {
        if (fMoveTime <= 0)
        {
            transform.position = vEndPos;
        }

        fNowTime = 0.0f;
        vStartPos = gPlayer.transform.position;
    }

    void Move()//移動処理
    {
        fNowTime += Time.deltaTime;

        if (fNowTime > fMoveTime)
        {
            gPlayer.transform.position = vEndPos;
        }

        float rate = fNowTime / fMoveTime;
        gPlayer.transform.position = Vector3.Lerp(vStartPos, vEndPos, rate);
    }
}
