using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public RectTransform credits;
    void Update()
    {
        if (Input.GetButtonDown("Reset") || credits.anchoredPosition.y > 2020)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
