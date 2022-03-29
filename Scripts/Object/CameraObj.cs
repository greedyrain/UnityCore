using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObj : MonoBehaviour
{
    private static CameraObj instance;
    public static CameraObj Instance => instance;

    //Player playerObj;
    //Camera camera;
    //Vector3 cameraPos, cameraDir;
    //float cameraDistance = 8, cameraAngle = 20, overHeadDis = 3;

    Animator anim;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
        //camera = GetComponent<Camera>();
    }

    void Start()
    {
        //playerObj = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCameraAnimation(string animName)
    {
        switch (animName)
        {
            case "TurnLeft":
                anim.SetTrigger("TurnLeft");
                break;
        }
    }

    //void CameraFollow()
    //{
    //    cameraDistance += Input.GetAxis("Mouse ScrollWheel");//通过滚轮控制相机距离
    //    cameraDistance = Mathf.Clamp(cameraDistance, 4, 15);//设置相机最远最近距离
    //    if (Input.GetKeyDown(KeyCode.UpArrow))//设置相机俯视角度
    //        cameraAngle += 5;
    //    if (Input.GetKeyDown(KeyCode.DownArrow))
    //        cameraAngle -= 5;
    //    if (Input.GetMouseButton(1))
    //    {
    //        cameraAngle += Input.GetAxis("Mouse Y");
    //    }
    //    cameraAngle = Mathf.Clamp(cameraAngle, 10, 70);//夹紧函数控制相机角度边界值;

    //    cameraPos = playerObj.transform.position + playerObj.transform.up * overHeadDis;//计算出距离头顶的位置
    //    cameraDir = Quaternion.AngleAxis(cameraAngle, playerObj.transform.right) * -playerObj.transform.forward;//
    //    cameraPos += cameraDir * cameraDistance;
    //    camera.transform.position = cameraPos;
    //    camera.transform.rotation = Quaternion.LookRotation(-cameraDir);
    //}
}
