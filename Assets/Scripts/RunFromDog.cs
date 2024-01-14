using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunFromDog : MonoBehaviour
{
    public Transform dogTransform;
    public float deerSeparationDistance = 8.0f;
    public float deerSpeed = 3.0f;
    public float boundaryRadius = 20.0f;
    public float angle = 95f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToDog = Vector3.Distance(dogTransform.position, transform.position);

        if (distanceToDog < deerSeparationDistance)
        {
            Vector3 directionToDog = transform.position - dogTransform.position;
            Vector3 fleePosition = transform.position + directionToDog.normalized * deerSeparationDistance;

            if (Vector3.Distance(Vector3.zero, fleePosition) < boundaryRadius)
            {
                transform.rotation = Quaternion.LookRotation(directionToDog);
                transform.position = Vector3.MoveTowards(transform.position, fleePosition, Time.deltaTime * deerSpeed);
            }
            else
            {
                float adjustedTurnAngle = angle * Mathf.Sign(Vector3.Cross(Vector3.forward, directionToDog).y);
                transform.rotation *= Quaternion.AngleAxis(adjustedTurnAngle * Time.deltaTime, Vector3.up);
                //transform.rotation *= Quaternion.AngleAxis(angle * Time.deltaTime, Vector3.up);
                transform.position += transform.forward * Time.deltaTime * deerSpeed;
            }
        }
    }

    
}
