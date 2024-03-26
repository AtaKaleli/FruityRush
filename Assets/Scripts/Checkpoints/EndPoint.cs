using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{

    private Ingame_UI inGame_UI;
    private void Start()
    {
        inGame_UI = GameObject.Find("Canvas").GetComponent<Ingame_UI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GetComponent<Animator>().SetTrigger("active");
            AudioManager.instance.PlaySFX(6);
            inGame_UI.OnLevelFinished();
            inGame_UI.EndGameScoreInfo();
            GameManager.instance.SaveBestTime();
            GameManager.instance.SaveCollectedFruits();
            GameManager.instance.SaveLevelInfo();
            PlayerManager.instance.KillPlayer();
        }
    }
}
