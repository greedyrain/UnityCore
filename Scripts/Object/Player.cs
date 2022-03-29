using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    CharacterController cc;
    public float moveH, moveV;
    public float moveSpeed, rotateSpeed;
    Animator anim;
    bool isDead;
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
        if (Input.GetMouseButton(1))
            transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 10, transform.up);

        SetAnimate();
    }

    void SetAnimate()
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "GameScene")
            anim.SetTrigger("Attack");
        anim.SetBool("IsDead", isDead);
    }

    public void KnifeEvent()
    {
        ProtectZone.Instance.GetHurt(10);
    }

    public void Init(int id)
    {
        string weaponPath = GameDataManager.Instance.WeaponsData[id - 1].res;
        Instantiate(Resources.Load<GameObject>(weaponPath),weaponContainer);
    }
}
