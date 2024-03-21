using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [SerializeField] private GameObject playerPref;
    public Transform respawnPoint;
    public GameObject currentPlayer;


    private void Awake()
    {
        instance = this;
        PlayerRespawn();

    }

    public void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
    }
}
