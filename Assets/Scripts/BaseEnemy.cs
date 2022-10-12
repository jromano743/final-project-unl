using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    protected Transform thisTransform;//Transform del enemigo
    protected Vector3 startPosition;//Posicion de inicio del enemigo

    [Header("Enemy Base")]
    public GunColor EnemyColor;
    public MeshRenderer HatRenderer;
    
    public float stunTime = 5f;
    [SerializeField] protected float currentStunTime;
    public bool resetMe;//Variable que indica si el enemigo debe reiniciarse
    public Material RedMaterial;
    public Material YellowMaterial;
    public Material BlueMaterial;
    protected Transform grappPosition;
    protected bool isGrappred;
    [SerializeField] protected bool isStuned;
    protected Transform stunPosition;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        startPosition = thisTransform.position;
        isGrappred = false;
        isStuned = false;
    }

    //Funcion que se redefine segun el tipo de enemigo
    protected virtual void ResetEnemy()
    {
        
    }

    protected bool GrappeMe(GunColor gunColor, Transform _grappPosition)
    {
        if(gunColor == EnemyColor)
        {
            grappPosition = _grappPosition;
            isGrappred = true;
            currentStunTime = stunTime;
            return true;
        }

        return false;
    }

    public bool IsStuned()
    {
        return isStuned;
    }

    protected void PaintColorObject()
    {
        switch (EnemyColor)
        {
            case GunColor.Red:
                HatRenderer.material = RedMaterial;
            break;

            case GunColor.Yellow:
                HatRenderer.material = YellowMaterial;
            break;

            case GunColor.Blue:
                HatRenderer.material = BlueMaterial;
            break;

            default:
                HatRenderer.material = RedMaterial;
                EnemyColor = GunColor.Red;
            break;
        }
    }
}
