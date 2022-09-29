using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    public GameObject panel;

    public string[] SinitialDialog
    {get; set;}
    public string[] SfightlDialog
    {get; set;}
    public string[] SfinalDialog
    {get; set;}

    public Text nameSpeaker;
    public string getNameSpeaker() { return nameSpeaker.text; }
    public void setNameSpeaker(string name) { nameSpeaker.text = name; }
    public Text txtDialog;
    public bool isDialogActive;
    public PlayerController playerController;

    Coroutine auxCorutine;

    void Start(){
        panel.SetActive(false);
    }
    public void OpenDialogBox(int sDialog){
        if (isDialogActive)
        {
            CloseDialog();
            StartCoroutine(WaitOverlapSDialog(sDialog));
        }
        else
        {
            playerController.canWalk = false;
            panel.SetActive(true);
            isDialogActive = false;
            auxCorutine = StartCoroutine(ShowDialog(sDialog));
        }
    }
    IEnumerator ShowDialog(int sDialog, float timePerLetter = 0.01f, float timePerDialog = 2.4f){
        panel.SetActive(true);
        string[] dialog;
        if(sDialog == 0) dialog = SinitialDialog;
        else if (sDialog == 1) dialog = SfightlDialog;
        else dialog = SfinalDialog;

        int nDialogs = dialog.Length;
        string res = "";
        isDialogActive = true;
        yield return null;
        for(int i=0; i<nDialogs;i++)
        {
            res = "";
            if(isDialogActive)
            {
                for(int letter = 0; letter < dialog[i].Length; letter++)
                {
                    if(isDialogActive)
                    {
                        res = string.Concat(res, dialog[i][letter]);
                        txtDialog.text = res;
                        yield return new WaitForSeconds(timePerLetter);
                    }
                    else yield break;
                }
                yield return new WaitForSeconds(timePerDialog);
            }
            else yield break;
        }
        yield return new WaitForSeconds(timePerDialog);
        CloseDialog();
    }

    IEnumerator WaitOverlapSDialog(int sDialog)
    {
        yield return new WaitForEndOfFrame();
        auxCorutine = StartCoroutine(ShowDialog(sDialog));
    }

    public void CloseDialog()
    {
        isDialogActive = false;
        if (auxCorutine != null)
        {
            StopCoroutine(auxCorutine);
            auxCorutine = null;
        }

        txtDialog.text = "";
        panel.SetActive(false);
        playerController.canWalk = true;
    }
}
