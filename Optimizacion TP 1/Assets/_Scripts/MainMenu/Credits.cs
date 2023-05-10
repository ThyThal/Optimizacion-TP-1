using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Credits : MonoBehaviourUI
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
