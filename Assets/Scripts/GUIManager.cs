using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public Text timer;
    public GrapplingGun grapGun;
    GunColor activeColorGun;
    public float time = 0.0f;

    public Image lastColor, activeColor, nextColor;
    public GameObject item1, item2, item3;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "Tiempo: " + time.ToString("f2");
        activeColorGun = grapGun.activeColor;
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            time += Time.deltaTime;
            timer.text = "Tiempo " + time.ToString("f2");
        }
        if(activeColorGun != grapGun.activeColor)
        {
            ChangeColor();
            activeColorGun = grapGun.activeColor;
        }
    }

    void ChangeColor()
    {
        lastColor.color = AssignColor(grapGun.GetColor(2));
        activeColor.color = AssignColor(grapGun.GetColor(0));
        nextColor.color = AssignColor(grapGun.GetColor(1));
    }

    Color AssignColor(GunColor color)
    {
        Color aux;
        switch (color)
        {
            case GunColor.Blue:
                aux = Color.blue;
                break;
            case GunColor.Red:
                aux = Color.red;
                break;
            case GunColor.Yellow:
                aux = Color.yellow;
                break;
            default:
                aux = Color.gray;
                break;
        }
        return aux;
    }

    public void ShowCollectableItems(bool show1, bool show2, bool show3)
    {
        item1.SetActive(show1);
        item2.SetActive(show2);
        item3.SetActive(show3);
    }

    public void ResetGame()
    {
        LevelManager.sharedInstance.ResetAllLevel();
    }
}
