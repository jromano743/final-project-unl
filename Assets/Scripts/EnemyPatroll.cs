using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatroll : BaseEnemy
{
    [Header("Variables de Patrullero")]
    public Animator anim;
    public float maxRight;
    public float maxLeft;
    public float speed;
    public float chaseSpeed;
    public Rigidbody rb;
    bool lookRigth = true;
    bool chaseEnemy = false;
    [Header("Variables del jugador")]
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetBool("Move", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (resetMe) ResetEnemy();
        if (!chaseEnemy)
        {
            if (lookRigth)
            {
                rb.velocity = new Vector3(speed, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(-speed, 0, 0);
            }
            CheckDirectionOnPatroll();
        }
        else
        {
            SetPlayerDirection();
            FlipDirection(lookRigth);
            if (lookRigth)
            {
                rb.velocity = new Vector3(chaseSpeed, 0, 0);
            }
            else
            {
                rb.velocity = new Vector3(-chaseSpeed, 0, 0);
            }
        }

    }

    //Verifica hacia donde mira el personaje mientras esta patrullando
    void CheckDirectionOnPatroll()
    {
        if(transform.position.x > startPosition.x + maxRight)
        {
            lookRigth = false;
        }
        if (transform.position.x < startPosition.x - maxLeft)
        {
            lookRigth = true;
        }
        FlipDirection(lookRigth);
    }

    //Observa en donde esta el jugador para mirarlo
    void SetPlayerDirection()
    {
        if (player.position.x > transform.position.x)
        {
            lookRigth = true;
        }
        else
        {
            lookRigth = false;
        }
        FlipDirection(lookRigth);
    }

    //Da vuelta el personaje
    void FlipDirection(bool right)
    {
        if (right)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    //Intenta capturar al jugador
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            chaseEnemy = true;
            anim.SetBool("Chase", chaseEnemy);
        }
    }

    //Ignora al jugador
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            chaseEnemy = false;
            anim.SetBool("Chase", chaseEnemy);
        }
    }

    //Dibuja los gizmos de la distancia entre la cual patrullara
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * maxRight);
        Gizmos.DrawRay(transform.position, transform.right * maxLeft * -1);
    }

    //Reinicia los valores del personaje a sus valores iniciales
    protected override void ResetEnemy()
    {
        resetMe = false;
        transform.position = startPosition;
        lookRigth = true;
        chaseEnemy = false;
    }
}
