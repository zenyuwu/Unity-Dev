using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PathFollower : MonoBehaviour
{
    [SerializeField] SplineContainer splineContainer;
    [Range(0,40)] public float speed = 1;

    float tdistance = 0; //distance along spline (0-1)

    //length in world coordinates
    public float length { get { return splineContainer.CalculateLength(); } }

    //distance in world coordinates
    public float distance 
    { 
        get { return tdistance * length; } 
        set { tdistance = value /  length; }
    }

    void Update()
    {
        distance += speed * Time.deltaTime;
        UpdateTransform(math.frac(tdistance));
    }

    void UpdateTransform(float t)
    {
        Vector3 position = splineContainer.EvaluatePosition(t);
        Vector3 up = splineContainer.EvaluateUpVector(t);
        Vector3 forward = Vector3.Normalize(splineContainer.EvaluateTangent(t));
        //crossing the up and the forward gives us the right :)
        Vector3 right = Vector3.Cross(up, forward);
        transform.position = position;
        transform.rotation = Quaternion.LookRotation(forward, up);
    }
}
