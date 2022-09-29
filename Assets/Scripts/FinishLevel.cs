using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public bool needAKey;
    public KeyBehaivor[] keys;

    private void Start()
    {
        keys = FindObjectsOfType(typeof(KeyBehaivor)) as KeyBehaivor[];
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
        if (needAKey)
        {
            bool allCollectables = true;
            for (int i = 0; i < keys.Length; i++)
            {
                if (!keys[i].isTaken) allCollectables = false;
            }
            if (allCollectables) LevelManager.sharedInstance.FinishLevel();
        }
        else
        {
            LevelManager.sharedInstance.FinishLevel();
        }
    }

    public void ResetKeys()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].ResetMe();
        }
    }
}
