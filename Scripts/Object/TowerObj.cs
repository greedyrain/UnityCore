using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObj : MonoBehaviour
{
    int id, checkRadius, cost;
    float atkCD, remainAtkCD;
    string res, bulletRes;
    public Transform head, muzzle;
    public bool isLockOn;
    public List<Transform> targetList = new List<Transform>();
    Transform target;

    private void Start()
    {
        Init(1);
    }

    private void Update()
    {
        //炮塔有两种状态：锁定状态、非锁定状态；
        //非锁定状态下，会遍历当前这一瞬间离炮塔最近的一个目标，将其锁定，锁定之后为了放置目标丢失而导致数组读取报错，直接将数组清空，并进入锁定状态；
        //锁定状态下，会不断进行攻击，当攻击的目标死亡gameObject销毁时，会重新进入非锁定状态；
        remainAtkCD -= Time.deltaTime;

        //锁定状态下，清空数组，以备下一次重新扫描目标；
        if (isLockOn)
            targetList.Clear();
        //设置一个目标池，选择最近的目标进行攻击；

        //非锁定状态下，扫描检测范围内的目标，将所有目标加入目标数字；
        if (!isLockOn)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
            foreach (Collider coll in colliders)
            {
                if (coll.CompareTag("Enemy") && !targetList.Contains(coll.transform))
                    targetList.Add(coll.transform);
            }
        }

        //遍历目标数组，找出距离最近的一个设置为目标，并进入锁定状态；
        if (targetList.Count > 0 && target == null)
        {
            target = targetList[0];//设置一个默认的目标，经过下面的逻辑处理后得到新的目标；
            foreach (Transform tar in targetList)
            {
                //如果目标列表中存在距离比默认目标近的物体，则设置该物体为目标；
                if ((tar.position - transform.position).magnitude < (target.position - transform.position).magnitude)
                    target = tar;
            }
            isLockOn = true;
        }

        if (target != null)
            Fire(target);

        if (target == null)
            isLockOn = false;
    }

    public void Init(int id)
    {
        if (id <= 0)
            id = 1;
        TowerData data = GameDataManager.Instance.TowersData[id - 1];
        this.id = id;
        checkRadius = data.checkRadius;
        cost = data.cost;
        atkCD = data.atkCD;
        bulletRes = data.bulletRes;
    }

    public void Fire(Transform target)
    {
        head.LookAt(target);
        if (remainAtkCD <= 0)
        {
            remainAtkCD = atkCD;
            GameObject bullet = Instantiate(Resources.Load<GameObject>(bulletRes));
            bullet.transform.position = muzzle.position;
            bullet.transform.LookAt(target.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
