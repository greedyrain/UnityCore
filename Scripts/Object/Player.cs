using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;
    public float moveH, moveV;
    public float moveSpeed, rotateSpeed;
    Animator anim;
    bool isDead;
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
        anim.SetFloat("SpeedV", moveV);
        anim.SetFloat("SpeedH", moveH);
        cc.SimpleMove(transform.forward * moveV * moveSpeed);
        cc.SimpleMove(transform.right * moveH * moveSpeed);

        //人物转动相关：
        if (Input.GetMouseButton(1))
            transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 10, transform.up);

        SetAnimate();
    }

    void SetAnimate()
    {
        if (Input.GetMouseButtonDown(0))
            anim.SetTrigger("Attack");
        anim.SetBool("IsDead", isDead);
    }

    public void KnifeEvent()
    {
        print("123123");
    }
}
