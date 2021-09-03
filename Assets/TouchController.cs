using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Player2;
    public GameObject Player1;
    public  Vector3 startPos;
    public  Vector3 endPos;
    private LineRenderer lr;
    public Vector3[] points;
    private Vector3 initialTouch;
    private Vector3 screenPoint;
    private Coroutine moveRoutine;
    public int trajectoryResolution = 20;



    private void Awake()
    {
        startPos = new Vector3(Ball.transform.position.x, Player1.transform.position.y, Player1.transform.position.z);
        endPos = new Vector3(Ball.transform.position.x, Player2.transform.position.y, Player2.transform.position.z);
        lr = GetComponent<LineRenderer>();
        
    }

    private void SetUpLine(Vector3[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Start()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        points = new Vector3[trajectoryResolution];

    }
    private void DrawTrajectory(Vector3 initial, Vector3 final,float diff)
    {
        diff = Mathf.Clamp(diff, -5f, 5f);
        float i = 0;
        int x = 0;
        Vector3 posx;
        while (i < trajectoryResolution)
        {
            posx = Vector3.right * -diff * Mathf.Sin(i / trajectoryResolution * Mathf.PI);

            points[x] = Vector3.Lerp(initial, final, i / trajectoryResolution) + posx;
            i += 1f;
            x += 1;
            
        }
        
        
        lr.positionCount = points.Length;
        lr.SetPositions(points);
        lr.enabled = true;
        //this.points[i] = Vector3.Lerp(initial, final, elapsedTime);

    }
    IEnumerator MoveUp()
    {
        
        float elapsed = 0f;
        int i = 0;

        for(int x =0; x< points.Length -1 ; x++)
        {
            while (elapsed < 1f)
            {
                elapsed += Time.deltaTime * 10f;
                Ball.transform.position = Vector3.Lerp(points[x], points[x + 1], elapsed);
                i++;
                yield return null;
            }
            elapsed = 0f;
        }
        Ball.transform.position = endPos;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            initialTouch = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            
        }
        else if (Input.GetMouseButton(0))
        {
           
            float diff = initialTouch.x - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)).x;
            DrawTrajectory(startPos, endPos,diff);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            initialTouch = Vector3.zero;
            moveRoutine = StartCoroutine(MoveUp());
            lr.enabled = false;

        }
    }
}
