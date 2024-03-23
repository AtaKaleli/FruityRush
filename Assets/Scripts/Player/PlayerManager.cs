using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    public static PlayerManager instance;

    [SerializeField] private GameObject playerPref;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public int chosenSkinID;

    public int collectedFruits;
    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    public void PlayerRespawn()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
            return;


        if (currentPlayer == null)
            currentPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
    }
}
