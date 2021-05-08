using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpline : MonoBehaviour
{

    private Vector3[] splinePoint;
    private int splineCount;

    public bool drawSpline = true;

    [TextArea]
    public string comment;

    // Start is called before the first frame update
    void Start()
    {
        splineCount = transform.childCount;
        splinePoint = new Vector3[splineCount];

        for (int i = 0; i < splineCount; i++)
        {
            splinePoint[i] = transform.GetChild(i).position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (splineCount > 1)
        {
            for (int i = 0; i < splineCount; i++)
            {
                if (drawSpline == true)
                {
                    Debug.DrawLine(splinePoint[i], splinePoint[i + 1], Color.green);
                }
            }
        }
    }

    public Vector3 WhereOnSpline (Vector3 pos)
    {
        int closestSplinePoint = GetClosestSplinePoint(pos);

        if(closestSplinePoint == 0)
        {
            return SplineSegment(splinePoint[0], splinePoint[1], pos);
        }

        else if (closestSplinePoint == splineCount - 1)
        {
            return SplineSegment(splinePoint[splineCount - 1], splinePoint[splineCount - 2], pos);
        }

        else
        {
            Vector3 leftSeq = SplineSegment(splinePoint[closestSplinePoint - 1], splinePoint[closestSplinePoint], pos);
            Vector3 rightSeq = SplineSegment(splinePoint[closestSplinePoint + 1], splinePoint[closestSplinePoint], pos);

            if ((pos - leftSeq).sqrMagnitude <= (pos - rightSeq).sqrMagnitude)
            {
                return leftSeq;
            }

            else
            {
                return rightSeq;
            }
        }
    }

    // Finds the closest spline point
    private int GetClosestSplinePoint(Vector3 pos)
    {
        int closestPoint = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < splineCount; i++)
        {
            float sqrDistance = (splinePoint[i] - pos).sqrMagnitude;
            if(shortestDistance == 0.0f || sqrDistance < shortestDistance)
            {
                shortestDistance = sqrDistance;
                closestPoint = i;
            }
        }

        return closestPoint;
    }

    public  Vector3 SplineSegment (Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 seqDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(seqDirection, v1ToPos);

        if (distanceFromV1 < 0.0f)
        {
            return v1;
        }

        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }

        else
        {
            Vector3 fromV1 = seqDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }
}
