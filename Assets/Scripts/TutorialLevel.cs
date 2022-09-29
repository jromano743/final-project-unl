using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLevel : MonoBehaviour
{
    SceneLoader sceneLoader;//Cargador de escenas
    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButton("Jump"))
        {
            sceneLoader.OpenLevel(-1);
        }
    }
}
