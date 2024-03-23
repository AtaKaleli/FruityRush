using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelDifficulty;
    

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
    }
    private void Update()
    {
        
    }

    public void SaveGameDiffuculty()
    {
        PlayerPrefs.SetInt("GameDifficulty", levelDifficulty);
    }




}
