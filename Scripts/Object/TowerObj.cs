using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObj : MonoBehaviour
{
    int id, checkRadius, cost;
    float atkCD,remainAtkCD;
    string res, bulletRes;
    public Transform head;

    private void Start()
    {
        
    }

    private void Update()
    {
        remainAtkCD -= Time.deltaTime;
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider coll in colliders)
        {
            if (coll.CompareTag("Enemy"))
            {
                Fire(coll.transform);
            }
        }
    }

    public void Init(int id)
    {
        if (id<=0)
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
        }
    }
}
