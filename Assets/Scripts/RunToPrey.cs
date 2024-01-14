using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunToPrey : MonoBehaviour
{
    public Transform dogTransform;
    public Transform[] deerTransforms;
    public float dogSpeed = 1.0f;
    public float boreTime = 5.0f;
    private float bored = 5.0f;
    private Transform currentDeer;
    // Start is called before the first frame update
    void Start()
    {
        currentDeer = FindClosestDeer();
    }

    Transform FindClosestDeer()
    {
        Transform closestDeer = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform deer in deerTransforms)
        {
            float distance = Vector3.Distance(dogTransform.position, deer.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDeer = deer;
            }
        }

        return closestDeer;
    }

    Transform FindRandomDeer()
    {
        return deerTransforms[Random.Range(0,deerTransforms.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //Transform closestDeer = FindClosestDeer();

        //if (closestDeer != null)
        //{
        //    MoveTowardsDeer(closestDeer);
        //}

        bored -= Time.deltaTime;
        if (bored < 0)
        {
            bored = boreTime;
            currentDeer = FindRandomDeer();
        }
        if(currentDeer != null)
        {
            MoveTowardsDeer(currentDeer);
        }
    }

    void MoveTowardsDeer(Transform targetDeer)
    {
        
        Vector3 directionToDeer = targetDeer.position - dogTransform.position;
        transform.rotation = Quaternion.LookRotation(directionToDeer);

        transform.position = Vector3.MoveTowards(transform.position, targetDeer.transform.position, Time.deltaTime * dogSpeed); 
    }
}
