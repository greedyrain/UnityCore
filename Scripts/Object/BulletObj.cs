using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    Player bulletOwner;
    public float moveSpeed;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        bulletOwner = FindObjectOfType<Player>();
        damage = GameDataManager.Instance.WeaponsData[bulletOwner.id - 1].atk;
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //获得击中的Enemy身上的脚本，执行掉血；
            other.GetComponent<ZombieObj>().GetHurt(damage);
        }
    }
}
