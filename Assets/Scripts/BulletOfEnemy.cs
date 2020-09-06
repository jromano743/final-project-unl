using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOfEnemy : MonoBehaviour
{
    public float speed;
    public Vector3 direction;

    private void Start()
    {
        Destroy(gameObject, 0.5f);
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
