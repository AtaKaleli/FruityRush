using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelDifficulty;

    [Header("Timer Info")]
    public float timer;
    public bool startTime;

    [Header("Level Info")]
    public int levelNumber;

  

    private void Awake()
    {
        
        
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            
            instance = this;

        }
        else
        {
            
 
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        int gameDifficulty = PlayerPrefs.GetInt("GameDifficulty");
        if (levelDifficulty == 0)
            levelDifficulty = gameDifficulty;

      

    }
    private void Update()
    {
        if (startTime)
        {
            timer += Time.deltaTime;
        }
           
    }

    public void SaveGameDiffuculty()
    {
        PlayerPrefs.SetInt("GameDifficulty", levelDifficulty);
    }

    public void SaveBestTime()
    {
        float previousRecord = PlayerPrefs.GetFloat("Level " + levelNumber + "BestTime",999);
        startTime = false;
        if(timer < previousRecord)
            PlayerPrefs.SetFloat("Level " + levelNumber + "BestTime", timer);
        timer = 0;
    }

    public void SaveCollectedFruits()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");
        int newFruits = totalFruits + PlayerManager.instance.collectedFruits;
        PlayerPrefs.SetInt("TotalFruitsCollected", newFruits);
        PlayerPrefs.SetInt("Level" + levelNumber + "FruitsCollected", PlayerManager.instance.collectedFruits);
        PlayerManager.instance.collectedFruits = 0;
    }

    public void SaveLevelInfo()
    {
        int nextLevelNumber = levelNumber + 1;
        PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked", 1);
    }


}
