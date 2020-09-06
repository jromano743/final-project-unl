using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public PlayerController player;
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float offsetX;
    public float offsetUp;

    private void Start()
    {
        
    }
    //Sigue al "target"
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        CheckDirection();
        CheckLookDown();
    }

    //Cambia el offset izquierdo o derecho segun hacia donde mire el personaje
    void CheckDirection()
    {
        float offsetXAux = 0.0f;
        if (player.look) offsetXAux = offsetX;
        if (player.lookRight)
        {
            offset.x = offsetXAux;
        }
        else
        {
            offset.x = -offsetXAux;
        }
    }

    void CheckLookDown()
    {
        if (player.lookDown)
        {
            offset.y = 0;
        }
        else
        {
            offset.y = offsetUp;
        }
    }
}
