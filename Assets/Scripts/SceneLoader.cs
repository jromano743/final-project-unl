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
        //Debug.Log("Nivel actual START:" + currentLevel);
        //Debug.Log("Niveles actuales: START" + numberOfLevels);
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
        }
    }

    //Abre el siguiente nivel disponible
    public void NextLevel()
    {
        if (currentLevel > 0) UnlockLevel();
        if (currentLevel <= numberOfLevels)
        {
            OpenLevel(currentLevel+1);
        }
        else
        {
            OpenLevel(0);
        }
    }
    //0 is the menu. Any other is a level
    public void OpenLevel(int level)
    {
        switch(level){
            case -2:
                SceneManager.LoadScene("Menu");
            break;
            case -1:
                SceneManager.LoadScene("Tutorial");
            break;
            case 0:
                SceneManager.LoadScene("Base");
            break;
            default:
                SceneManager.LoadScene("Level" + level);
            break;
        }
        //Debug.Log("Niveles actuales:" + numberOfLevels);
        //Debug.Log("Nivel actual:" + currentLevel);
    }

    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
