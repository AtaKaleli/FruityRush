using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ingame_UI : MonoBehaviour
{

    

    [Header("Ingame Info")]
    [SerializeField] private TextMeshProUGUI gameTimer;
    [SerializeField] private TextMeshProUGUI collectedFruits;

    [Header("Endgame Info")]
    [SerializeField] private TextMeshProUGUI endGameBestTime;
    [SerializeField] private TextMeshProUGUI endGameYourTime;
    [SerializeField] private TextMeshProUGUI endGameFruits;

    [Header("MenuGameObjects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseGameUI;
    [SerializeField] private GameObject endGameUI;

    private bool isGamePaused;

    private void Start()
    {
        
        GameManager.instance.levelNumber = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
        SwitchUI(inGameUI);
        PlayerManager.instance.ingame_UI = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInGameInfo();


        if (Input.GetKeyDown(KeyCode.Escape))
            checkIfNotPaused();


    }

    private void UpdateInGameInfo()
    {
        gameTimer.text = "Timer: " + GameManager.instance.timer.ToString("#0.00");
        collectedFruits.text = PlayerManager.instance.collectedFruits.ToString();
    }

    public void EndGameScoreInfo()
    {
        
        
        

        endGameBestTime.text = "Best time: " + PlayerPrefs.GetFloat("Level " + GameManager.instance.levelNumber + "BestTime",999).ToString("#0.00") + " sec";
        endGameYourTime.text ="Your time: " + GameManager.instance.timer.ToString("#0.00") + " sec";
        endGameFruits.text ="Fruits: " +  PlayerManager.instance.collectedFruits.ToString();
    }


    public void checkIfNotPaused()
    {
        if (!isGamePaused)
        {
            isGamePaused = true;
            Time.timeScale = 0;
            SwitchUI(pauseGameUI);
            
        }
        else
        {
            isGamePaused = false;
            Time.timeScale = 1;
            SwitchUI(inGameUI);
           
        }
    }


    public void OnLevelFinished()
    {
        SwitchUI(endGameUI);
    }

    public void SwitchUI(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false); // switch off everything
        }

        uiMenu.SetActive(true); // switch on what we need
    }

    public void ReturnMainMenu() => SceneManager.LoadScene("Main Menu");
    public void RestartCurrentLevel() { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.instance.timer = 0;
        PlayerManager.instance.collectedFruits = 0;

    } 
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       
    }

}
