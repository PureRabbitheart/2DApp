using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FlickTest : MonoBehaviour
{
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;

    [SerializeField]
    private LayerMask BackGround;
    [SerializeField, Range(0, 1)]
    float time = 0.5f;
    [SerializeField]
    private GameObject player;

    void Start()
    {
        startPosition = player.transform.position;
        endPosition = player.transform.position;

    }

    void Update()
    {
        Flick();
        Move();
    }

    void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
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


    void RayHit(Vector2 vPos)
    {

        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, vPos, 100, BackGround);
        Debug.DrawRay(player.transform.position, new Vector3(0, 0, 100), Color.blue, 1);// 可視化

        //なにかと衝突した時だけそのオブジェクトの名前をログに出す
        if (hit.collider)
        {
            endPosition = new Vector3(hit.point.x - vPos.x, hit.point.y - vPos.y, 0.0f);
        }

    }


    void MoveInit()
    {
        if (time <= 0)
        {
            transform.position = endPosition;
        }

        startTime = Time.timeSinceLevelLoad;
        startPosition = player.transform.position;
    }

    void Move()
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        if (diff > time)
        {
            player.transform.position = endPosition;
        }

        var rate = diff / time;
        player.transform.position = Vector3.Lerp(startPosition, endPosition, rate);
    }

    void OnDrawGizmosSelected()
    {
#if UNITY_EDITOR

        if (!UnityEditor.EditorApplication.isPlaying || enabled == false)
        {
            startPosition = player.transform.position;
        }

        UnityEditor.Handles.Label(endPosition, endPosition.ToString());
        UnityEditor.Handles.Label(startPosition, startPosition.ToString());
#endif
        Gizmos.DrawSphere(endPosition, 0.1f);
        Gizmos.DrawSphere(startPosition, 0.1f);

        Gizmos.DrawLine(startPosition, endPosition);
    }
}
