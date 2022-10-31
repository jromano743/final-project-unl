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
        PaintColorObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetMe) ResetEnemy();

        //No esta agarrado ni stuneado
        if(!isGrappred && !isStuned)
        {
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
        

        //ESTA AGARRADO
        if(isGrappred)
        {
            float step = (speed * 5 ) *Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, grappPosition.position, step);

            isGrappred = false;
            isStuned = true;

            stunPosition = transform;
        }

        //Esta stuneado
        if(isStuned)
        {
            transform.position = stunPosition.position;
            rb.velocity = Vector3.zero;
            currentStunTime -= Time.deltaTime;
            if(currentStunTime < 0)
            {
                isStuned = false;
                anim.SetBool("Stunned", false);
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
        FlipDirection(lookRigth);//TODO::Comprobar si esto afecta o no el rendimiento
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
            if(isStuned) return;

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


    public override void StunEnemy(Collider other)
    {
        if(isStuned) return;

            
        GunColor color = other.GetComponentInParent<GrapplingGun>().GetColor();
        bool isCorrectColor = GrappeMe(color, player);
        if(isCorrectColor)
        {
            chaseEnemy = false;

            player.gameObject.GetComponent<PlayerController>().holdEnemy = true;

            AudioManager.sharedInstance.PlaySound(PunchSound);
            anim.SetBool("Stunned", true);
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
        isGrappred = false;
        isStuned = false;
        transform.position = startPosition;
        lookRigth = true;
        chaseEnemy = false;
        anim.SetBool("Chase", false);
        anim.SetBool("Move", false);
        anim.SetBool("Stunned", false);
    }
}
