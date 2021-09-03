using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private LineRenderer lr;
    public Transform[] points;
    public GameObject startPos;
    public GameObject endPos;
    // Start is called before the first frame update
    void Start()
    {
        lr= GetComponent<LineRenderer>();
        SetUpLine(points);
        
    }

    private void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    // Update is called once per frame
    void Update()
    {
        var distance = Vector3.Distance(startPos.transform.position, endPos.transform.position);
        for (int i=0; i< points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
