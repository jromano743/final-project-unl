using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public bool needAKey;
    public KeyBehaivor[] keys;

    AlarmPanel MainPanel;

    private void Start()
    {
        keys = FindObjectsOfType(typeof(KeyBehaivor)) as KeyBehaivor[];
        MainPanel = Object.FindObjectOfType<AlarmPanel>();
        if(keys.Length != 0)
        {
            needAKey = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButton("Jump"))
        {
            CheckWin();
        }
    }

    private void CheckWin()
    {
        if(MainPanel.AlarmsOff())
        {
            return;
        }

        Debug.Log("Fin del nivel");
        /*
        if (needAKey)
        {
            bool allKeysIsCollected = true;
            for (int i = 0; i < keys.Length; i++)
            {
                if (!keys[i].isTaken) allKeysIsCollected = false;
            }
            if (allKeysIsCollected) LevelManager.sharedInstance.FinishLevel();
        }
        else
        {
            LevelManager.sharedInstance.FinishLevel();
        }*/
    }

    public void ResetKeys()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].ResetMe();
        }
    }

}
