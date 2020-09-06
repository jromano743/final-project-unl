using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    public bool actived = false;
    public float speed = 10.0f;
    public GameObject myObject;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (actived)
        {
            rb.isKinematic = false;
            rb.velocity = new Vector3(0,-speed,0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground" && collision.gameObject != myObject.gameObject)
        {
            actived = false;
            rb.isKinematic = true;
        }
    }
}
