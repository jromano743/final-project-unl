using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    AlarmPanel MainPanel;
    [SerializeField] Material ActiveMaterial, DeactiveMaterial;
    [SerializeField] Color ActiveColor, DeactiveColor;
    [SerializeField] MeshRenderer ButtonAlarm;
    [SerializeField] Light SLight;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel = Object.FindObjectOfType<AlarmPanel>();
    }

    public void ActiveAlarm()
    {
        ButtonAlarm.material = ActiveMaterial;
        SLight.color = ActiveColor;
    }
    public void DeactiveAlarm()
    {
        ButtonAlarm.material = DeactiveMaterial;
        SLight.color = DeactiveColor;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("PatrolHead"))
        {
            EnemyReporter enemy = other.gameObject.GetComponentInParent<EnemyReporter>();
            if(enemy)
            {
                if(enemy.IsReporting())
                {
                    MainPanel.ActiveAlarm();
                    enemy.DontFollow();
                }
            }
        }
    }

}
