using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaivor : MonoBehaviour
{
    public bool isTaken;
    public float speedRotation=10.0f;
    public GameObject particleEfecct;
    Vector3 rotation;
    void Start()
    {
        isTaken = false;
        rotation = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * speedRotation, Space.Self);//Rota el objeto
    }

    public void ResetCoin()
    {
        //Si la moneda ya fue tomada, la muestra un poco transparente
        if (isTaken)
        {
            Material newMat = Resources.Load("Materials/CoinTaken", typeof(Material)) as Material;
            GetComponentInChildren<Renderer>().material = newMat;
        }
        gameObject.SetActive(true);
    }

    public void FullResetCoin()
    {
        isTaken = false;
        ResetCoin();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            AudioManager.sharedInstance.PlaySound(AudioManager.sharedInstance.pickUpCoin);
            GameObject aux = Instantiate(particleEfecct, transform.position, transform.rotation);
            isTaken = true;
            Destroy(aux, 1.0f);
            gameObject.SetActive(false);
        }
    }
}
