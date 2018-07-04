using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GameManager.Instance.StartGame(GameManager.Instance.CurrentLevel + 1);
        }
    }
}
