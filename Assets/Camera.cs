using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    [Range(0,1)]
    public float lerp;
    public Vector3 offset;
    public Vector2 shake;
    public Vector2 shakeSpeed;
    //public float zOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shakeVector = new Vector3(Mathf.Sin(Time.time*shakeSpeed.x)*shake.x,Mathf.Cos(Time.time*shakeSpeed.y)*shake.y,0);
        transform.position = Vector3.Slerp(transform.position,target.position + offset + shakeVector,lerp*Time.deltaTime*60);
    }
}
