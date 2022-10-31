using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;
    public GrapplingGun playerGun;

    //Al detectar un objeto aderente indica al hook que se este se adiere y fija el objeto aderente
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "HookableRed":
                if(playerGun.activeColor == GunColor.Red)
                {
                    playerGun.hooked = true;
                    playerGun.hookedObj = other.gameObject;
                }
                break;
            case "HookableBlue":
                if (playerGun.activeColor == GunColor.Blue)
                {
                    playerGun.hooked = true;
                    playerGun.hookedObj = other.gameObject;
                }
                break;
            case "HookableYellow":
                if (playerGun.activeColor == GunColor.Yellow)
                {
                    playerGun.hooked = true;
                    playerGun.hookedObj = other.gameObject;
                }
                break;
            case "ground":
                Debug.Log("Pared");
                player.GetComponent<GrapplingGun>().WallCollision();
                break;
            default:
                /*  TODO: Al chocar con un objeto cualquiera retroceder */
                break;
        }
    }
}
