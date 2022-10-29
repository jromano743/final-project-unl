using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolHead : MonoBehaviour
{
    [SerializeField] BaseEnemy Owner;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Gun"))
        {
            Owner.StunEnemy(other);
        }
    }
}
