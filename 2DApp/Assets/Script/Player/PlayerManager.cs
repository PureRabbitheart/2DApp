using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private Text CountText;

    private int ItemCount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Item")
        {
            ItemCount++;
            Destroy(other.gameObject);

            CountText.text = ItemCount.ToString();
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
