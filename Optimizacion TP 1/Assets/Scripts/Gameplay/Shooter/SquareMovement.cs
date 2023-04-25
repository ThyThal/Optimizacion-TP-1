using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SquareMovement : MonoBehaviourGameplay
{
    private int nextPoint;
    private int distance = 1;

    [SerializeField] private float speed;
    [SerializeField] private GameObject[] wayPoints;

    public UnityEvent OnTextChange = new UnityEvent();

    public override void ManagedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextPoint].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, wayPoints[nextPoint].transform.position) < distance)
        {
            nextPoint++;
            TextChange();

            if (nextPoint >= wayPoints.Length)
            {
                nextPoint = 0;
                TextChange();
            }
        }
        base.ManagedUpdate();
    }

    public int TextChange()
    {
        return nextPoint;
    }
}
