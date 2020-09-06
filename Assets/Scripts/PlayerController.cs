using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Propiedades del jugador
    public Animator anim;
    Rigidbody rb;
    bool canJump;
    public bool lookRight;
    public bool lookDown;
    public bool look;
    public float playerSpeed = 10.0f;
    public float jumpForce = 10.0f;
    Vector3 initialPosition;
    Vector3 resetZ;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        lookDown = false;
        look = false;
        initialPosition = transform.position;
        canJump = true;
        rb = GetComponent<Rigidbody>();
        lookRight = true;
        resetZ = new Vector3(1, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reset") && !gameOver) LevelManager.sharedInstance.ResetAllLevel();
        if (!gameOver) CheckMove();
    }

    //Mover al personaje en el eje horizontal
    void CheckMove()
    {
        //Tomamos el eje ejes
        float movX = Input.GetAxis("Horizontal");
        if(movX != 0) CheckDirection(movX);
        //Se aplica la fuerza en el eje X
        rb.velocity = new Vector3(movX * playerSpeed, rb.velocity.y, 0);
        if (Input.GetButtonDown("Jump") && canJump) Jump();
        lookDown = Input.GetButton("LookDown");
        look = Input.GetButton("Look");

        //Animator
        anim.SetFloat("VelX", movX);
    }

    //Funcion para saltar
    void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            canJump = false;
            anim.SetFloat("VelY", 1.0f);
        }
    }

    void CheckDirection(float dir)
    {
        if(dir > 0)
        {
            lookRight = true;
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            lookRight = false;
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    //Indicar que el personaje puede volver a saltar
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ground" || collision.transform.tag == "HookableRed" || collision.transform.tag == "HookableBlue" || collision.transform.tag == "HookableYellow")
        {
            anim.SetFloat("VelY", 0f);
            canJump = true;
        }
        if(collision.transform.tag == "Enemy" && !gameOver)
        {
            ResetPosition();
            LevelManager.ResetLevel();
        }
        if(collision.transform.tag == "Finish")
        {
            LevelManager.sharedInstance.FinishLevel();
        }
    }

    //Detiene en seco la velocidad del personaje
    public void StopForces()
    {
        rb.velocity = Vector3.zero;
    }

    public void Finish()
    {
        gameOver = true;
        rb.velocity = Vector3.zero;
        anim.SetFloat("VelX", 0f);
        anim.SetFloat("VelY", 0f);
    }

    //Reinicia la posicion del personaje
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }

    public void ResetZPosition()
    {
        resetZ.x = transform.position.x;
        resetZ.y = transform.position.y;
        resetZ.z = 0;
        transform.position = resetZ;
    }
}
