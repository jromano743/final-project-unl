using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager sharedInstance;

    [Header("Coins")]
    GameObject[] collectable;

    [Header("Enemies")]
    GameObject[] enemies;

    [Header("Datos del juego")]
    public PlayerController player;
    public int tryes = 0;
    public float bestTime;
    public float currentTime;
    public bool[] takenOjbects; /*AQUI*/

    SceneLoader sceneLoader;//Cargador de escenas
    public GameObject finishMenu;//Contiene el panel del menu final
    public GUIManager guiGame;
    public FinishLevel endPoint;

    [Header("GUI")]
    public float time = 0.0f;
    public bool gameover = false;

    // Start is called before the first frame update
    private void Awake()
    {
        //Patron singleton
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        
    }

    public void Start()
    {
        endPoint = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevel>();
        collectable = GameObject.FindGameObjectsWithTag("Coin");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        guiGame = GameObject.FindGameObjectWithTag("GUI").GetComponent<GUIManager>();
    }

    //Realiza la configuraicon necesaria para reiniciar el nivel, se utiliza cuando el jugador muere
    public static void ResetLevel()
    {
        for (int i = 0; i < sharedInstance.enemies.Length; i++)
        {
            sharedInstance.enemies[i].GetComponent<BaseEnemy>().resetMe = true;
        }
        for (int i = 0; i < sharedInstance.collectable.Length; i++)
        {
            sharedInstance.collectable[i].GetComponent<CoinBehaivor>().ResetCoin();
        }
        sharedInstance.endPoint.ResetKeys();
    }

    //Reinicia el nivel por completo. Se utiliza al apretar el boton R o entrar al nivel por primera vez
    public void ResetAllLevel()
    {
        //Reinicio de un enemigo
        for (int i = 0; i < sharedInstance.enemies.Length; i++)
        {
            sharedInstance.enemies[i].GetComponent<BaseEnemy>().resetMe = true;
        }

        //Reinicio de los objetos colleccionables del nivel. Las monedas se deben volver a tomar
        for (int i = 0; i < sharedInstance.collectable.Length; i++)
        {
            sharedInstance.collectable[i].GetComponent<CoinBehaivor>().FullResetCoin();
        }

        //Reinicio de los objetos y variables del nivel
        player.ResetPosition();
        sharedInstance.endPoint.ResetKeys();
        player.gameOver = false;
        guiGame.time = 0.0f;
        guiGame.gameOver = false;
        currentTime = 0;
        tryes = 0;
        guiGame.time = 0.0f;
    }

    //TODO: FUNCION PARA MOSTRAR EL PANEL FINAL
    public void CountCoins()
    {
        takenOjbects = new bool[3];
        int j = 0;

        for (int i = 0; i < collectable.Length; i++)
        {
            try
            {
                takenOjbects[j] = collectable[i].GetComponent<CoinBehaivor>().isTaken;
                j++;
            }
            catch
            {
                Debug.Log("Esto no es una moneda");
            }
        }
    }

    //Configura los resultados y el comportamiento del juego al finalizar el nivel
    public void FinishLevel()
    {
        player.Finish();
        guiGame.gameOver = true;
        currentTime = guiGame.time;
        CountCoins();
        //sceneLoader.UnlockLevel();//TODO: Desbloquear SOLO el nivel siguiente al nivel actual
        finishMenu.SetActive(true);
        guiGame.ShowCollectableItems(takenOjbects[0], takenOjbects[1], takenOjbects[2]);
    }
}
