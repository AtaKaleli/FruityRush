using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Sequences;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{



    public static PlayerManager instance;
    public Ingame_UI ingame_UI;


    [SerializeField] private GameObject playerPref;
    [SerializeField] private GameObject deathFXPref;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    public int chosenSkinID;

    public int collectedFruits;


    [SerializeField] private CinemachineImpulseSource impulse;

    public void ScreenShake(int facingDir)
    {
        impulse.m_DefaultVelocity = new Vector3(1 * facingDir, 1);
        impulse.GenerateImpulse();
    }

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

    public void KillPlayer()
    {
        GameObject newDeathFX = Instantiate(deathFXPref, currentPlayer.transform.position, Quaternion.identity);
        Destroy(newDeathFX,.4f);

        Destroy(currentPlayer);
    }

    private bool HaveEnoughFruits()
    {
        if(collectedFruits > 0)
        {
            collectedFruits--;
            if (collectedFruits < 0)
                collectedFruits = 0;
            return true;
        }
        return false;
    }

    public void OnTakingDamage()
    {

        if (HaveEnoughFruits())
        {
            
            if (GameManager.instance.levelDifficulty == 2)
            {
                print("fruit dropped");
            }
            
        }
        else
        {
            ingame_UI.checkIfNotPaused();
        }

         
    }

    public void OnFalling()
    {
        KillPlayer();

        if(GameManager.instance.levelDifficulty == 1)
        {
            Invoke("PlayerRespawn", 1f);
        }
        else if (GameManager.instance.levelDifficulty == 2)
        {
            if (HaveEnoughFruits())
                Invoke("PlayerRespawn", 1f);
            else
                ingame_UI.checkIfNotPaused();
        }
        else
        {
            ingame_UI.checkIfNotPaused();
        }

        
    }

}
