using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : BaseEnemy
{
    [Header("Variables del Enemigo")]
    public Rigidbody rb;
    public bool awake = true;
    public bool shoot = false;
    public bool lookRight;
    public float timeSleep;
    public float timeAwake;
    public float actualTime;
    public float distance;

    [Header("Variables del referencia")]
    public Transform target;
    public GameObject bullet;
    public float bulletSpeed;
    public Animator anim;
    public SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        FlipDirection(lookRight);
        PaintColorObject();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStuned)
        {
            currentStunTime -= Time.deltaTime;

            if(currentStunTime < 0)
            {
                isStuned = false;
                anim.SetBool("Stunned", false);
            }
        }
        else
        {
            actualTime -= Time.deltaTime;
            if(actualTime < 0.0f)
            {
                ChangeStatus();
            }
        }
    }

    public void ChangeStatus()
    {
        if (awake)
        {
            actualTime = timeSleep;
            awake = false;
            anim.SetBool("awake", awake);
        }
        else
        {
            actualTime = timeAwake;
            awake = true;
            anim.SetBool("awake", awake);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && awake && !isStuned)
        {
            target = other.transform;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject aux = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);

        Vector3 direction = target.position - aux.transform.position;
        if (lookRight)
        {
            direction = direction.normalized;
        }
        else
        {
            direction = direction.normalized * -1;
            direction.y = direction.y * -1;
        }
        aux.GetComponent<BulletOfEnemy>().direction = direction.normalized;
        aux.GetComponent<BulletOfEnemy>().speed = bulletSpeed;
        aux.SetActive(true);
    }

    void FlipDirection(bool right)
    {
        if (right)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
            //sphereCollider.center = sphereCollider.center;
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
            //sphereCollider.center = sphereCollider.center * -1;
        }
    }
    protected override void ResetEnemy()
    {
        resetMe = false;
        transform.position = startPosition;
        awake = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.right * distance);
        Gizmos.DrawRay(transform.position, transform.right * -distance);
    }

    public void HitInHead()
    {
        if(isStuned) return;

        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Transform targetPosition = target.transform;
        GunColor color = target.GetComponentInParent<GrapplingGun>().GetColor();

        bool isCorrectColor = GrappeMe(color, targetPosition);

        if(isCorrectColor)
        {
            target.gameObject.GetComponent<PlayerController>().holdEnemy = true;
            isStuned = true;
            anim.SetBool("Stunned", true);
        }
    }
}
