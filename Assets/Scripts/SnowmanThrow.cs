using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public int amountToPool;

    public GameObject snowBall;
    public float throwDistance;
    public int throwSpeed;

    private bool justThown = false;
    private int frameInterval = 5;
    private GameObject target;
    private Vector3 thrownY = new Vector3(0, 0.33f, 0);

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(snowBall);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % frameInterval == 0)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);

            if (distanceToTarget < throwDistance && justThown == false)
            {
                ThrowSnowball();
            }
        }
    }

    private void ThrowSnowball()
    {
        justThown = true;

        GameObject tempSnowBall = GetPooledObject();
        if (tempSnowBall != null)
        {
            tempSnowBall.transform.position = transform.position;
            tempSnowBall.transform.rotation = transform.rotation;
            tempSnowBall.SetActive(true);

            Rigidbody tempRb = tempSnowBall.GetComponent<Rigidbody>();
            Vector3 targetDirection = Vector3.Normalize(target.transform.position - transform.position);

            //Add a small throw angle
            targetDirection += thrownY;
            tempRb.AddForce(targetDirection * throwSpeed);     
        }

        Invoke("ThrowOver", 0.1f);
    }

    private void ThrowOver()
    {
        justThown = false;
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
