using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponObj : MonoBehaviour
{
    public E_WeaponType type;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Player.Instance.SetFirePos(transform.GetChild(i));
        }
        Player.Instance.SetWeaponType(type);
    }
}
