using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerLevel : MonoBehaviour
{
    SceneLoader sceneLoader;
    public int unlockLevels;
    static public int currentLevel;
    public Button[] menuButtons;

    private void Awake()
    {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
