using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDialog : MonoBehaviour
{
    public string nameSpeaker;
    public bool isReactivatable;
    [TextArea(3,30)]
    public string[] SinitialDialog;
    [TextArea(3,30)]
    public string[] SfightlDialog;
    [TextArea(3,30)]
    public string[] SfinalDialog;

    bool hasActivate;
    TextShow boxDialog;
    // Start is called before the first frame update
    void Start()
    {
        hasActivate = false;
        GameObject GO = GameObject.FindGameObjectWithTag("BoxDialog");
        if(GO != null) boxDialog = GO.GetComponent<TextShow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasActivate)
        {
            boxDialog.setNameSpeaker(nameSpeaker);
            boxDialog.SinitialDialog = SinitialDialog;
            boxDialog.OpenDialogBox(0);
            hasActivate = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && hasActivate && Input.GetButton("Jump") && isReactivatable)
        {
            boxDialog.setNameSpeaker(nameSpeaker);
            boxDialog.SinitialDialog = SinitialDialog;
            boxDialog.OpenDialogBox(0);
        }
    }
}
