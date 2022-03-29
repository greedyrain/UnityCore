using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieObj : MonoBehaviour
{
    //自动寻路，找到目标点，并开始走动；
    //进入一定范围后会攻击目标；
    //有一定的检测范围，如果该范围内只存在玩家时，会优先攻击玩家，而如果同时存在玩家和目标点，则会优先攻击目标点；
    public int id;
    public int HP, maxHP, atk;
    float moveSpeed, rotateSpeed, atkCD;

    public float checkRadius,damageRadius,attackRange;
    bool isDead, toPlayer,toZone,attacking;

    Transform target;
    public Transform attackCenter;
    Animator anim;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = GameObject.Find("ProtectZone").transform;
        agent.SetDestination(target.position);
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (!toZone)//没有靠近保护区则进入该分支；
        {
            //检测周围的物体；
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
            foreach (Collider collider in colliders)//遍历周围的物体；
            {
                if (collider.CompareTag("ProtectZone"))//如果物体的Tag是保护区，则直接设置目标为保护区，并退出循环；
                {
                    toZone = true;
                    target = collider.transform;
                    agent.SetDestination(target.position);
                    break;
                }
                if (collider.CompareTag("Player"))//如果不是保护区，是player，则走向player，但检测依然在继续；
                {
                    target = collider.transform;
                    toPlayer = true;
                    agent.SetDestination(target.position);
                }
            }
        }

        //检测是否有目标进入攻击范围，有则进入攻击状态；
        foreach (Collider coll in Physics.OverlapSphere(transform.position,attackRange))
        {
            if (coll.CompareTag("ProtectZone") || coll.CompareTag("Player"))
            {
                attacking = true;
                anim.SetTrigger("Attack");
            }
        }
    }

    public void GetHurt(int damage)//受伤的方法；
    {
        HP -= damage;
        print($"收到伤害：{damage}");
        print("僵尸掉血");
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            anim.SetBool("IsDead", isDead);//死亡后摧毁物体；
            Destroy(gameObject, 4);
        }
    }

    public void Attack()//动画播放是，会在某一帧出现一个检测区域，这一瞬间会对检测区域没的物体造成伤害；
    {
        Collider[] colliders = Physics.OverlapSphere(attackCenter.position, damageRadius);
        foreach (Collider coll in colliders)
        {
            if (coll.CompareTag("ProtectZone"))
                coll.GetComponent<ProtectZone>().GetHurt(atk);
            if (coll.CompareTag("Player"))
                coll.GetComponent<Player>().GetHurt(atk);
            print("Attack");
        }
    }

    public void Init()
    {
        ZombieData data = GameDataManager.Instance.ZombiesData[id - 1];
        HP = data.HP;
        maxHP = data.HP;
        atk = data.atk;
        moveSpeed = data.moveSpeed;
        rotateSpeed = data.rotateSpeed;
        atkCD = data.atkCD;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackCenter.position, damageRadius);
    }
}