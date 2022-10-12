using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum GunColor
{
    Red,
    Yellow,
    Blue
}
public class ListaCircular
{
    class Nodo
    {
        public GunColor color;
        public Nodo ant, sig;
    }

    private Nodo raiz;

    public ListaCircular()
    {
        raiz = null;
    }

    public void InsertarPrimero(GunColor x)
    {
        Nodo nuevo = new Nodo();
        nuevo.color = x;
        if (raiz == null)
        {
            nuevo.sig = nuevo;
            nuevo.ant = nuevo;
            raiz = nuevo;
        }
        else
        {
            Nodo ultimo = raiz.ant;
            nuevo.sig = raiz;
            nuevo.ant = ultimo;
            raiz.ant = nuevo;
            ultimo.sig = nuevo;
            raiz = nuevo;
        }
    }

    public void InsertarUltimo(GunColor x)
    {
        Nodo nuevo = new Nodo();
        nuevo.color = x;
        if (raiz == null)
        {
            nuevo.sig = nuevo;
            nuevo.ant = nuevo;
            raiz = nuevo;
        }
        else
        {
            Nodo ultimo = raiz.ant;
            nuevo.sig = raiz;
            nuevo.ant = ultimo;
            raiz.ant = nuevo;
            ultimo.sig = nuevo;
        }
    }

    public bool Vacia()
    {
        if (raiz == null)
            return true;
        else
            return false;
    }

    public int Cantidad()
    {
        int cant = 0;
        if (!Vacia())
        {
            Nodo reco = raiz;
            do
            {
                cant++;
                reco = reco.sig;
            } while (reco != raiz);
        }
        return cant;
    }

    public void DebugLogs()
    {
        
    }

    public void Borrar(int pos)
    {
        if (pos <= Cantidad())
        {
            if (pos == 1)
            {
                if (Cantidad() == 1)
                {
                    raiz = null;
                }
                else
                {
                    Nodo ultimo = raiz.ant;
                    raiz = raiz.sig;
                    ultimo.sig = raiz;
                    raiz.ant = ultimo;
                }
            }
            else
            {
                Nodo reco = raiz;
                for (int f = 1; f <= pos - 1; f++)
                    reco = reco.sig;
                Nodo anterior = reco.ant;
                reco = reco.sig;
                anterior.sig = reco;
                reco.ant = anterior;
            }
        }
    }

    public void RotateToRigth()
    {
        Nodo aux = raiz;

        raiz = raiz.ant;
        raiz.sig = aux;
        aux.ant = raiz;
        aux.sig.ant = aux;
    }

    public void RotateToLeft()
    {
        Nodo aux = raiz;

        raiz = raiz.sig;
        raiz.ant = aux;
        aux.sig = raiz;
        aux.ant.sig = aux;
    }

    //Retorna la raiz de la lista
    public GunColor ReturnActiveColor()
    {
        return raiz.color;
    }

    public GunColor ReturnNextColor()
    {
        return raiz.sig.color;
    }

    public GunColor ReturnPreviosColor()
    {
        return raiz.ant.color;
    }
}

public class GrapplingGun : MonoBehaviour
{
    [Header("Propiedades del hook")]
    public GunColor activeColor;
    public GameObject hook;//El hook que se dispara
    public GameObject hookHolder;//El contenedor del hook (La pistola)

    public float hookTravelSpeed;//Velocidad en la que se mueve el hook
    public float playerTravelSpeed;//Velocidad en la que se mueve el jugador con el hook conectado

    public static bool fired;//Si se disparó el hook
    public bool hooked;//Cuando se enganchó a un objeto
    public GameObject hookedObj;//El objeto al que se engancho
    public LineRenderer rope;//La linea que dibujara desde el hook hasta el hok holder

    public float maxDistance;//Distancia maxima que puede recorrer el hook
    private float currentDistance;//Distancia actual del hook y el hookholder
    public bool grounded;//

    Vector3 hookScale;//La escala actual del hook (para evitar la deformacion al impactar)

    public CapsuleCollider playerCollider;//El collider del jugador

    public PlayerController player;

    [Header("Colores disponibles")]
    public Material colorRed;
    public Material colorYellow;
    public Material colorBlue;
    public Material colorTest;
    //Propiedades de la lista circular
    ListaCircular list;

    //Se prepara el rope y la escala del hook
    private void Awake()
    {
        list = new ListaCircular();
        list.InsertarPrimero(GunColor.Blue);
        list.InsertarPrimero(GunColor.Yellow);
        list.InsertarPrimero(GunColor.Red);
    }
    private void Start()
    {

        activeColor = list.ReturnActiveColor();

        rope.positionCount = 0;
        hookScale = hook.transform.localScale;

        ChangeWeaponColor();
    }

    private void Update()
    {
        CheckInputs();
        //Si se esta disparando
        if (fired)
        {
            rope.positionCount = 2;//Se actualiza los puntos del rope
            rope.SetPosition(0, hookHolder.transform.position);//El primer punto se lo coloca en el hookholder
            rope.SetPosition(1, hook.transform.position);//El segundo punto se lo coloca en el hook
            Vector3 mousePos = Input.mousePosition;//Se guarda la posicion del mouse en
            mousePos.z = 0f;//Se le modifca el eje Z

            //Se toma la posicion del holder en el mundo y se obtiene la distancia entre el mouse y este
            Vector3 objectPos = Camera.main.WorldToScreenPoint(hookHolder.transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            //Se calcula el angulo en donde esta el mouse con respecto al holder
            //Se hace que este siga al mouse
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            hookHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        //Si se dispara y aun no choco con un objeto aderente sigue su camino
        //Si excede la distancia maxima regresa
        if (fired && !hooked)
        {
            hook.transform.Translate(Vector3.up * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
            if (currentDistance >= maxDistance) ReturnHook();
        }

        //Si esta en disparo y se adiere a un objeto aderente
        if (fired && hooked)
        {
            player.StopForces();
            playerCollider.isTrigger = true; //Se coloca al player con trigeer
            hook.transform.parent = hookedObj.transform; //Se vuelve al objeto aderente padre del hook

            //El jugador se mueve hacia la posicion del hook
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);//Se calcula la distancia hacia el hook

            GetComponent<Rigidbody>().useGravity = false;
            if (distanceToHook < 2)
            {
                if (!grounded)//Si la distancia es minima y no esta en tierra de le da un empujon hacia arriba
                {
                    transform.Translate(Vector3.up * Time.deltaTime * 90f);
                }
                StartCoroutine("Climb");
            }
        }
        else
        {
            //Si no se adiere. Hookholder se vuelve padre del hook
            hook.transform.parent = hookHolder.transform;
            GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void CheckInputs()
    {
        //Si se presiona el BTI y no esta disparando, se dispara y se reproduce un sonido
        if (Input.GetMouseButtonDown(0) && !fired)
        {
            AudioManager.sharedInstance.PlaySound(AudioManager.sharedInstance.grraplinGun);
            fired = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            list.RotateToRigth(); 
            ChangeWeaponColor();
        }
    }

    void ChangeWeaponColor()
    {
        switch (list.ReturnActiveColor())
        {
            case GunColor.Yellow:
                rope.material = colorYellow;
                break;
            case GunColor.Red:
                rope.material = colorRed;
                break;
            case GunColor.Blue:
                rope.material = colorBlue;
                break;
            default:
                rope.material = colorTest;
                break;
        }
        activeColor = list.ReturnActiveColor();
    }
    IEnumerator Climb()
    {
        yield return new WaitForSeconds(0.1f);
        ReturnHook();
    }

    void ReturnHook()
    {
        //Se reinician todas las propiedades del hook y del rope
        player.ResetZPosition();
        playerCollider.isTrigger = false;
        hook.transform.rotation = hookHolder.transform.rotation = Quaternion.LookRotation(Vector3.zero);
        hook.transform.position = hookHolder.transform.position;
        hook.transform.localScale = hookScale;
        fired = false;
        hooked = false;
        rope.positionCount = 0;
    }

    public GunColor GetColor(int next=0)
    {
        if (next == 1)
        {
            return list.ReturnNextColor();
        }
        else if(next == 2)
        {
            return list.ReturnPreviosColor();
        }
        else
        {
            return list.ReturnActiveColor();
        }
    }

    //Dibuja el gizmo de hasta donde puede disparar el hook
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(hookHolder.transform.position, hookHolder.transform.up * maxDistance);
    }
}
