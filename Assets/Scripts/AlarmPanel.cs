using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmPanel : MonoBehaviour
{

    FinishLevel FinishDoor;
    bool PlayerIsInAlarm;
    bool AlarmActive = false;
    EnemyReporter[] Patrols;
    Alarm[] Alarms;

    [Header("Light Objects")]
    [SerializeField] Material ActiveMaterial;
    [SerializeField] Material DeactiveMaterial;
    [SerializeField] Color ActiveColor;
    [SerializeField] Color DeactiveColor;

    [Header("Light Config")]
    [SerializeField] float MaxIntensity = 3f;
    [SerializeField] float TunOnRate = 1;
    [SerializeField] MeshRenderer[] LightsObjects;
    [SerializeField] Light[] SpothLights;

    [Header("Sounds")]
    [SerializeField] AudioClip AlarmSound;
    [SerializeField] AudioClip AccesGrantedSound;
    float CurrentIntensity;
    bool TurnOnLight = false;
    
    private void Start() 
    {
        PlayerIsInAlarm = false;
        Patrols = FindObjectsOfType<EnemyReporter>();
        Alarms = FindObjectsOfType<Alarm>();

        CurrentIntensity = MaxIntensity;

        foreach (MeshRenderer light in LightsObjects)
        {
            light.material = DeactiveMaterial;
        }

        foreach (Light SLight in SpothLights)
        {
            SLight.color = DeactiveColor;
            SLight.intensity = CurrentIntensity;
        }
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerIsInAlarm) DeactiveAlarm();  

        if(AlarmActive)
        {
            if(TurnOnLight)
            {
                float DeltaTime = Time.deltaTime;
                SpothLights[0].intensity += DeltaTime * TunOnRate;
                SpothLights[1].intensity += DeltaTime * TunOnRate;
                SpothLights[2].intensity += DeltaTime * TunOnRate;
                CurrentIntensity += DeltaTime * TunOnRate;
                if(CurrentIntensity >= MaxIntensity) 
                {
                    AudioManager.sharedInstance.PlaySound(AlarmSound);
                    TurnOnLight = false;
                }
            }
            else
            {
                float DeltaTime = Time.deltaTime;
                SpothLights[0].intensity -= DeltaTime * TunOnRate;
                SpothLights[1].intensity -= DeltaTime * TunOnRate;
                SpothLights[2].intensity -= DeltaTime * TunOnRate;
                CurrentIntensity -= DeltaTime * TunOnRate;
                if(CurrentIntensity <= 0) TurnOnLight = true;
            }
        }
    }

    public void ActiveAlarm()
    {
        AlarmActive = true;

        foreach (Alarm alarm in Alarms)
        {
            alarm.ActiveAlarm();
        }

        foreach (MeshRenderer lightObject in LightsObjects)
        {
            lightObject.material = ActiveMaterial;
        }

        foreach (Light SLight in SpothLights)
        {
            SLight.color = ActiveColor;
        }
    }

    public void DeactiveAlarm()
    {
        AlarmActive = false;

        foreach (Alarm alarm in Alarms)
        {
            alarm.DeactiveAlarm();
        }

        foreach (MeshRenderer lightObject in LightsObjects)
        {
            lightObject.material = DeactiveMaterial;
        }

        foreach (Light SLight in SpothLights)
        {
            SLight.color = DeactiveColor;
            SLight.intensity = 1;
        }

        CurrentIntensity = 1;
        TurnOnLight = false;

        AudioManager.sharedInstance.PlaySound(AccesGrantedSound);

        CancelReports();
    }

    void CancelReports()
    {
        foreach (EnemyReporter enemy in Patrols)
        {
            enemy.RestoreReported();
        }
    }

    public bool AlarmsOff()
    {
        return AlarmActive;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerIsInAlarm = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            PlayerIsInAlarm = false;
        }
    }
}
