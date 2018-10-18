using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{



    [SerializeField]
    private TextMeshProUGUI CountText;
    [SerializeField]
    private GameManager pGameManager;

    private int ItemCount;


    [SerializeField]
    private float fHP;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item")
        {
            ItemCount++;
            Destroy(other.gameObject);

            CountText.text = "× " + ItemCount.ToString();
        }
        else if (other.tag == "Clear")
        {
            pGameManager.eGameMode = GameManager.GAMEMODE.Clear;
            LemonAddingUp();

        }
        else if (other.tag == "Gimmick")
        {
            other.transform.GetComponent<Animator>().SetTrigger("Play");
            fHP -= 1;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerActive();
    }

    void PlayerActive()//プレイヤーのHPの管理
    {
        if (fHP <= 0)
        {
            pGameManager.eGameMode = GameManager.GAMEMODE.Over;
        }
    }

    void LemonAddingUp()
    {
        int lemonValue = PlayerPrefs.GetInt("Lemon");
        lemonValue += ItemCount;
        PlayerPrefs.SetInt("Lemon", lemonValue);
    }
}
