using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pointsOne;
    [SerializeField] private GameObject[] pointsTwo;
    [SerializeField] private GameObject runnerOne, runnerTwo;
    
    Vector3[] massVctPointOne = new Vector3[13];
    Vector3[] massVctPointTwo = new Vector3[13];
    private Vector3 targetVctOne = new Vector3();
    private Vector3 targetVctTwo = new Vector3();

    private int pointN1 = 0, pointN2 = 0, 
                speed1, speed2;

    Random rand = new Random();


    void Start()
    {
        CreatePointVector(massVctPointOne, pointsOne);
        CreatePointVector(massVctPointTwo, pointsTwo);

        targetVctOne = massVctPointOne[pointN1];
        targetVctTwo = massVctPointTwo[pointN2];
        speed1 = rand.Next(1, 4);
        speed2 = rand.Next(1, 6);
    }

    void Update()
    {
        Movement(runnerOne, massVctPointOne, targetVctOne, speed1, true);
        Movement(runnerTwo, massVctPointTwo, targetVctTwo, speed2, false);

    }

    private void CreatePointVector(Vector3[] inputvector, GameObject[] inputobject)
    {

        for (int i = 0; i < inputobject.Length; i++)
        {
            inputvector[i] = inputobject[i].transform.position;
        }

    }    

    private void Movement(GameObject runner, Vector3[] massVct, Vector3 target, int speed, bool check)
    {
        if (runner.transform.position == target)
        {
            if (check)
            {
                pointN1 += 1;
                if (pointN1 < massVct.Length)
                {
                    targetVctOne = massVct[pointN1];
                }
                else if (pointN1 == massVct.Length)
                    pointN1 = 0;

                target = targetVctOne;
            }
            else
            {
                pointN2 += 1;
                if (pointN2 < massVct.Length)
                {
                    targetVctTwo = massVct[pointN2];
                }
                else if (pointN2 == massVct.Length)
                    pointN2 = 0;

                target = targetVctTwo;
            }

            //current++;
            //if (current < massVct.Length)
            //{
            //    target = massVct[current];
            //}
            //else if (current == massVct.Length)
            //    current = 0;
        }

        runner.transform.LookAt(target);
        runner.transform.position = Vector3.MoveTowards(runner.transform.position, target, Time.deltaTime * speed);
        
    }
}