using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected Transform thisTransform;//Transform del enemigo
    protected Vector3 startPosition;//Posicion de inicio del enemigo
    public bool resetMe;//Variable que indica si el enemigo debe reiniciarse

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        startPosition = thisTransform.position;
    }

    //Funcion que se redefine segun el tipo de enemigo
    protected virtual void ResetEnemy()
    {
        Debug.Log("Reset!");
    }
}
