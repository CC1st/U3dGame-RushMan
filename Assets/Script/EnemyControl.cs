using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    //箱子飞来的方向
    public Vector2 dir;
    //动画播放器
    private Animator ani;
    //移动计数器
    private int distance = 100;
    private int Isright = 1;
   
    
    void Start()
    {
        ani = GetComponent<Animator>();
    }

   
    void Update()
    {
        if (dir != Vector2.zero)
        {
            transform.Translate((Vector2.up + dir) * 15 * Time.deltaTime);
        }
        //来回移动
        else
        {
            transform.Translate(Vector2.right * Isright * 10 * Time.deltaTime);
            if (--distance == 0)
            {
                Isright = Isright > 0 ? -1 : 1;
                distance = 100;
            }
        }
    }

    //被箱子攻击
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Box")
        {
            //播放死亡动画
            ani.SetBool("IsDead", true);
            Destroy(GetComponent<CapsuleCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(gameObject, 3f);
            AudioManager.Instance.PlaySound("hit");
            //设定飞出方向
            dir = new Vector2(collision.collider.GetComponent<BoxControl>().direction,0);
        }
    }
}
