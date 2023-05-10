using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsScripts : MonoBehaviourUI
{
    
    public void ShowPopup()
    {
        this.gameObject.SetActive(true);
        
    }

    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }

    
}
