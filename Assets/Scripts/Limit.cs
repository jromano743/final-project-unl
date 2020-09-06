using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    public PlayerController player; //Referencia del jugador

    /*Si el jugador choca con los limites el juego se reinicia, si lo hace otro objeto este se desactiva*/
    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
       {
            LevelManager.ResetLevel();
            player.ResetPosition();
       }
       else
       {
            other.gameObject.SetActive(false);
       }
    }
}
