using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMeEnemyShooter : MonoBehaviour
{
    public EnemyShooter shooterReference;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Gun")
        {
            shooterReference.HitInHead();
        }
    }
}
