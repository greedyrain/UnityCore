using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController cc;
    public int id;
    public int HP, maxHP,atk;
    public float moveH, moveV;
    public float moveSpeed, rotateSpeed;
    Animator anim;
    bool isDead = false;
    public Transform weaponContainer;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //人物移动相关：
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("SpeedV", moveV);
            anim.SetFloat("SpeedH", moveH);
            cc.SimpleMove(transform.forward * moveV * moveSpeed);
            cc.SimpleMove(transform.right * moveH * moveSpeed);
        }
        else
        {
            anim.SetFloat("SpeedV", moveV * 0.5f);
            anim.SetFloat("SpeedH", moveH * 0.5f);
            cc.SimpleMove(transform.forward * moveV * moveSpeed * 0.5f);
            cc.SimpleMove(transform.right * moveH * moveSpeed * 0.5f);
        }

        //人物转动相关：
        //在GameScene中，鼠标左右移动就能够改变人物移动的方向；
        if (SceneManager.GetActiveScene().name == "GameScene")
            transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5, transform.up);
        //如果在BeginScene中（选择人物界面），要转动人物，则需要按下右键；
        else if (SceneManager.GetActiveScene().name == "BeginScene")
        {
            if (Input.GetMouseButton(1))
                transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -5, transform.up);
        }

        SetAnimate();
    }

    void SetAnimate()
    {
        //在GameScene中才会设置动画；
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "GameScene")
            anim.SetTrigger("Attack");
        anim.SetBool("IsDead", isDead);
    }

    //刀攻击触发的事件；
    public void KnifeEvent()
    {
        ProtectZone.Instance.GetHurt(10);
    }

    public void HandGunShootEvent()
    {
        //生成子弹
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Bullet/Bullet"));
        bullet.transform.position = weaponContainer.position;
        bullet.transform.rotation = transform.rotation;
    }

    public void HeavyGunShootEvent()
    {
        //生成子弹
        GameObject bullet = Instantiate(Resources.Load<GameObject>("Bullet/Bullet"));
        bullet.transform.position = weaponContainer.position;
        bullet.transform.rotation = transform.rotation;
    }

    public void Init(int id)
    {
        HP = GameDataManager.Instance.HerosData[id - 1].hp;
        maxHP = GameDataManager.Instance.HerosData[id - 1].hp;
        string weaponPath = GameDataManager.Instance.WeaponsData[id - 1].res;
        Instantiate(Resources.Load<GameObject>(weaponPath), weaponContainer);

    }

    public void GetHurt(int damage)
    {
        if (isDead)
            return;
        HP -= damage;
        print(HP);
        if (HP <= 0)
        {
            HP = 0;
            anim.SetBool("IsDead", isDead);
        }
    }
}
