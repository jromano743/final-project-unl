using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static int unlockedLevels;
    static int numberOfLevels;
    public int currentLevel;
    public Button[] menuButtons;

    public void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            numberOfLevels = menuButtons.Length;
            UpdateButtons();
        }
    }

    //Actualiza los botones que se muestran en el menu de selecciond de nivel
    public void UpdateButtons()
    {
        for (int i = 0; i < unlockedLevels+1; i++)
        {
            menuButtons[i].interactable = true;
        }
    }

    //Coloca al nivel en los niveles desbloqueados. Coloca el nuevo nivel actual
    public void UnlockLevel()
    {
        if(unlockedLevels < currentLevel)
        {
            unlockedLevels = currentLevel;
            currentLevel++;
        }
    }

    //Abre el siguiente nivel disponible
    public void NextLevel()
    {
        if (currentLevel <= numberOfLevels)
        {
            OpenLevel(currentLevel);
        }
        else
        {
            OpenLevel(0);
        }
    }
    //0 is the menu. Any other is a level
    public void OpenLevel(int level)
    {
        if(level == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("Level" + level);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
