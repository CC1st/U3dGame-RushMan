using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //跟随对象
    public Transform target;
    //画面边界
    public float Max;
    public float Min;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.position;
        v.x = target.position.x;
        if (v.x > Max)
        {
            v.x = Max;
        }
        else if (v.x < Min)
        {
            v.x = Min;
        }
        transform.position = v;
    }
}
