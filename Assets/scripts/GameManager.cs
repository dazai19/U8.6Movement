using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pointsOne;
    [SerializeField] private GameObject[] pointsTwo;
    [SerializeField] private GameObject runnerOne, runnerTwo, runnerThree, runnerFour;
    
    Vector3[] massVctPointOne = new Vector3[13];
    Vector3[] massVctPointTwo = new Vector3[13];
    private Vector3 targetVctOne = new Vector3();
    private Vector3 targetVctTwo = new Vector3();

    private GameObject activRunnerOne, activeRunnerTwo, passiveRunnerOne, passiveRunnerTwo, swapObject;

    private int pointN1 = 0, pointN2 = 0, 
                speed1, speed2;

    private float distOneThree, distTwoFour;
    private bool check1 = true, check2 = true;

    Random rand = new Random();


    void Start()
    {
        activRunnerOne = runnerOne;
        activeRunnerTwo = runnerTwo;
        passiveRunnerOne = runnerThree;
        passiveRunnerTwo = runnerFour;
        CreatePointVector(massVctPointOne, pointsOne);
        CreatePointVector(massVctPointTwo, pointsTwo);

        targetVctOne = massVctPointOne[pointN1];
        targetVctTwo = massVctPointTwo[pointN2];
        speed1 = rand.Next(1, 4);
        speed2 = rand.Next(1, 6);
    }

    void Update()
    {
        passiveRunnerOne.transform.LookAt(activRunnerOne.transform);
        passiveRunnerTwo.transform.LookAt(activeRunnerTwo.transform);

        distOneThree = Vector3.Distance(activRunnerOne.transform.position, passiveRunnerOne.transform.position);
        distTwoFour = Vector3.Distance(activeRunnerTwo.transform.position, passiveRunnerTwo.transform.position);

        if (distOneThree <= 0.7f && check1)
        {
            swapObject = activRunnerOne;
            activRunnerOne = passiveRunnerOne;
            passiveRunnerOne = swapObject;
            check1 = false;
        }
        else if (distOneThree > 1)
            check1 = true;

        if (distTwoFour <= 0.7f && check2)
        {
            swapObject = activeRunnerTwo;
            activeRunnerTwo = passiveRunnerTwo;
            passiveRunnerTwo = swapObject;
            check2 = false;
        }
        else if (distTwoFour > 1)
            check2 = true;

        Movement(activRunnerOne, massVctPointOne, targetVctOne, speed1, true);
        Movement(activeRunnerTwo, massVctPointTwo, targetVctTwo, speed2, false);

        passiveRunnerOne.transform.position = Vector3.MoveTowards(passiveRunnerOne.transform.position, massVctPointOne[6], Time.deltaTime);
        passiveRunnerTwo.transform.position = Vector3.MoveTowards(passiveRunnerTwo.transform.position, massVctPointTwo[7], Time.deltaTime);
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
        }

        runner.transform.LookAt(target);
        runner.transform.position = Vector3.MoveTowards(runner.transform.position, target, Time.deltaTime * speed);
        
    }
}