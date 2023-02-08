using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxControl : MonoBehaviour
{
    public float speed;
    public float direction;
    void Start()
    {
        
    }

 
    void Update()
    {
        transform.Translate(Vector2.right * speed * 30 * Time.deltaTime);
    }

    public void Move(float dir)
    {
        speed = dir;
        transform.parent = null;
        Destroy(gameObject, 3f);
    }
}
