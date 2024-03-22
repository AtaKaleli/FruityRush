using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private Transform buttonParent;

    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) //for each level we have, create a button in the main menu
        {
            string sceneName = "Level " + i; //create name of the scene
            
            GameObject newLevelButton = Instantiate(buttonPref, buttonParent); //instantiate new button
            newLevelButton.AddComponent<Button>().onClick.AddListener(() => LevelLoader(sceneName)); // then add onclick->listener to it, and pass func.
            newLevelButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;

        }
    }

    public void LevelLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
