using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    public static CoinBehaivor[][] collectables;

    private void Awake()
    {
        MakeSingleton();
    }

    public void Start()
    {
        Screen.SetResolution(1280, 720, true, 60);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetCollectablesOfLevel(int level, CoinBehaivor[] coins)
    {
        for (int i = 0; i < 3; i++)
        {
            collectables[level][i].isTaken = coins[i].isTaken;
        }
    }

    private void MakeSingleton()
    {
        //Patron singleton
        if (sharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
