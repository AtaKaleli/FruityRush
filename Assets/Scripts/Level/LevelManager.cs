using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPref;
    [SerializeField] private Transform buttonParent;

    [SerializeField] private bool[] levelOpen;

    private void Start()
    {
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);

        AssignLevelBooleans();

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) //for each level we have, create a button in the main menu
        {


            if (!levelOpen[i])
                return;

            string sceneName = "Level " + i; //create name of the scene

            GameObject newLevelButton = Instantiate(buttonPref, buttonParent); //instantiate new button
            newLevelButton.GetComponent<Button>().onClick.AddListener(() => LevelLoader(sceneName)); // then add onclick->listener to it, and pass func.
            newLevelButton.GetComponent<LevelButton>().DisplayLevelInfo(i);

        }
    }

    private void AssignLevelBooleans()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (unlocked)
            {
                levelOpen[i] = true;
            }
            else
                return;
        }
    }

    public void LevelLoader(string sceneName)
    {
        GameManager.instance.SaveGameDiffuculty();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNewGame()
    {
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;
            if (unlocked)
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            else
            {
                SceneManager.LoadScene("Level 1");
                return;

            }

            

        }
    }

    public void LoadContinueGame()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (!unlocked)
            {
                SceneManager.LoadScene("Level " + (i - 1));
                return;
            }
        }
    }

}
