using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    //动画
    private Animator ani;
    //是否在地面
    private bool IsGround;
    //移动速度
    public float speed = 2;
    //跳跃高度
    public float height;
    //举着的箱子
    private BoxControl box;
    //箱子的坐标
    private Transform boxTrans;
    //生命值
    public Text leftShow;
    private int leftCount;
    //无敌时长
    private float nextTime = 0;
    //无敌状态
    private bool isUndefect = false;
    //结束界面
    public GameObject overScene;
    //胜利界面
    public GameObject winScene;


    void Start()
    {
        ani = GetComponent<Animator>();
        boxTrans = transform.GetChild(0);
        leftCount = 3;
        //对象失活
        overScene.SetActive(false);
    }


    void Update()
    {
        //获取水平轴 左-1 右1 无0
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            //跑步动画
            ani.SetBool("IsRun", true);
            //转向
            transform.localScale = new Vector3(horizontal > 0 ? 5 : -5, 5, 1);
            //移动
            transform.Translate(Vector2.right * horizontal *speed* Time.deltaTime);
        }
        else
        {
            //站立动画
            ani.SetBool("IsRun", false);
        }

        //跳跃
        if (Input.GetKeyDown(KeyCode.J) && IsGround)
        {
            //跳跃
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * height);
            AudioManager.Instance.PlaySound("jump");
        }
        //抬箱子或攻击
        if (Input.GetKeyDown(KeyCode.K))
        {
            //抬箱子
            if (box != null && boxTrans.childCount == 0)
            {
                //举起箱子
                box.transform.SetParent(boxTrans);
                box.transform.localPosition = Vector2.zero;
                //举起音效
                AudioManager.Instance.PlaySound("getbox");
                //播放抬箱子动画
                ani.SetFloat("Isbox", 1);
            }
            //攻击
            else if (boxTrans.childCount > 0)
            {
                boxTrans.GetChild(0).GetComponent<BoxControl>().direction = transform.localScale.x;
                boxTrans.GetComponentInChildren<BoxControl>().Move(1);
                AudioManager.Instance.PlaySound("attack");
                ani.SetFloat("Isbox", 0);
                ani.SetTrigger("Attack");
            }
        }
        //掉出地图
        if (transform.position.y < -74f)
        {
            //生命-1
            DesLeftCount();
            //回到原点
            transform.position = new Vector3(-462.3f, -19.5f, 0);
        }
    }
    //在地面
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //向下掉落
        if ((collision.collider.tag == "Ground"||collision.collider.tag=="Box")
            &&GetComponent<Rigidbody2D>().velocity.y<=0)
        {
            //播放站立动画
            ani.SetBool("IsJump", false);
            IsGround = true;
            if(GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                //播放落地声音
                AudioManager.Instance.PlaySound("endjump");
            }           
        }
        //碰到敌人
        if (collision.collider.tag == "Enemy"&&!isUndefect)
        {
            //生命值-1
            DesLeftCount();
        }
        //维持无敌状态
        if (isUndefect)
        {
            nextTime += Time.deltaTime;
            if (nextTime > 0.02f)
            {
                nextTime = 0;
                isUndefect = false;
            }
        }
    }
    //减去生命值
    private void DesLeftCount()
    {
        leftCount--;
        leftShow.text = "×" + leftCount;
        isUndefect = true;
        ani.SetTrigger("Undefect");

        if (leftCount == 0)
        {
            overScene.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //不在地面
    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.collider.tag == "Ground"||collision.collider.tag=="Box")
            &&GetComponent<Rigidbody2D>().velocity.y>0)
        {
            //跳跃动画
            ani.SetBool("IsJump", true);
            IsGround = false;
        }
    }

    //检测进入触发对象
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Box")
        {
            //保存箱子 
            box = collider.GetComponent<BoxControl>();
        }
        //抵达终点
        if (collider.tag == "End")
        {
            //弹出胜利对话框
            winScene.SetActive(true);
            //暂停游戏
            Time.timeScale = 0;
        }
    }
    //检测离开触发的对象
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Box")
        {
            box = null;
        }
    }
}
