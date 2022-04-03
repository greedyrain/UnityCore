using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectZone : MonoBehaviour
{
    private static ProtectZone instance;
    public static ProtectZone Instance
    {
        get { return instance; }
    }
    //暴露位置；
    //更新血量，更新GamePanel上的UI显示；
    //对外提供受伤的函数，丧尸靠近后进行攻击会调用该函数；
    //血量归0时游戏结束；

    public int maxHP, currentHP;
    public bool isDead;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentHP = GameDataManager.Instance.InGameData.protectZoneHP;
        maxHP = GameDataManager.Instance.InGameData.protectZoneHP;
    }

    public void GetHurt(int damage)
    {
        if (isDead)
            return;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isDead = true;
        }
        UIManager.Instance.GetPanel<GamePanel>().UpdateHP(currentHP);
        if (isDead)
            GameManager.Instance.isFail = true;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
