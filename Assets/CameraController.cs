using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Ball;
    private Vector3 initial;
    private float offset = -10f;
    private float speed = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        initial = gameObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        gameObject.transform.position = Vector3.Lerp(initial,new Vector3(initial.x, initial.y, Ball.transform.position.z + offset),speed);
    }
}
