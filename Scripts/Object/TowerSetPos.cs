using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSetPos : MonoBehaviour
{
    bool isSet;
    public float checkRadius;

    private void OnTriggerEnter(Collider other)
    {
        //如果进入检测范围的，
        if (other.CompareTag("Player") && other.GetComponent<Player>().id == 1)
        {
            TowerPanel towerPanel = UIManager.Instance.ShowPanel<TowerPanel>();
            towerPanel.SetTowerPos(this);
            towerPanel.CreateButton();
            print("In");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<Player>().id == 1)
        {
            UIManager.Instance.HidePanel<TowerPanel>();
            print("Out");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    public void SetTower(int id)
    {
        //根据id创建炮塔。id由按钮传来；
        print(id + "号炮塔被建造");
        Instantiate(Resources.Load<GameObject>(GameDataManager.Instance.TowersData[id - 1].res),transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
