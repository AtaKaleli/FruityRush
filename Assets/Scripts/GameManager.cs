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
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        int gameDifficulty = PlayerPrefs.GetInt("GameDifficulty");
        if (levelDifficulty == 0)
            levelDifficulty = gameDifficulty;

        print(PlayerPrefs.GetFloat("Level " + levelNumber + "BestTime"));
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
        float previousRecord = PlayerPrefs.GetFloat("Level " + levelNumber + "BestTime");
        startTime = false;
        if(timer < previousRecord)
            PlayerPrefs.SetFloat("Level " + levelNumber + "BestTime", timer);
        timer = 0;
    }


}
