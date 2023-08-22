using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCheck : MonoBehaviour
{
    public enum Direction { Left ,Right };
    public Direction passingDir;
    public Material passedFlagMat, failedFlagMat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            float dirCheck = transform.position.x + other.transform.position.x;

            if(passingDir == Direction.Left)
            {
                if(other.transform.position.x < transform.position.x)
                {
                    PassSuccessful();
                }
                else
                {
                    PassUnsuccessful();
                }
            }
            else if (passingDir == Direction.Right)
            {
                if(other.transform.position.x > transform.position.x)
                {
                    PassSuccessful();
                }
                else
                {
                    PassUnsuccessful();
                }
            }
        }
    }


    private void PassSuccessful()
    {
        GetComponent<MeshRenderer>().material = passedFlagMat;
    }

    private void PassUnsuccessful()
    {
        GetComponent<MeshRenderer>().material = failedFlagMat;
        Timer.time += 1;
    }


}
