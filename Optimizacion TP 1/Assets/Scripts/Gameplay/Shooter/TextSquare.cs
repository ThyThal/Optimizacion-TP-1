using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSquare : MonoBehaviourUI
{
    [SerializeField] Text textWaypoints;
    [SerializeField] private SquareMovement squareMovement;


    public override void ManagedUpdate()
    {
        textWaypoints.text = ("NEXT POINT " + squareMovement.TextChange());

    }
}
