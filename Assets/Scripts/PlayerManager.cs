using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform respawnPoint;
    public GameObject currentPlayer;


    private void Awake()
    {
        instance = this;
        PlayerRespawn();

    }

    private void PlayerRespawn()
    {
        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
    }
}
