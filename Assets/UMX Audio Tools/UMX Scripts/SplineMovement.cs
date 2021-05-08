using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMovement : MonoBehaviour
{

    public BasicSpline spline;
    public Transform characterFollowObject;

    [TextArea]
    public string comment;

    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = spline.WhereOnSpline(characterFollowObject.position);
    }
}
