using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_CameraMode { Normal,Aim}

public class CameraObj : MonoBehaviour
{
    private static CameraObj instance;
    public static CameraObj Instance => instance;

    Animator anim;
    Player playerObj;
    Camera cameraObj;
    Vector3 cameraPos, cameraDir;
    float cameraDistance = 8, cameraAngle = 20, overHeadDis = 3;
    bool isPlayerSet;
    public E_CameraMode cameraMode;

    private void Awake()
    {
        cameraObj = transform.GetComponent<Camera>();
        instance = this;
        anim = GetComponent<Animator>();
        cameraMode = E_CameraMode.Normal;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "BeginScene" && isPlayerSet)
        {
            if (!GameManager.Instance.isLevelClear && !GameManager.Instance.isFail)
            {
                CameraFollow();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (cameraMode == E_CameraMode.Normal)
                cameraMode = E_CameraMode.Aim;
            else if (cameraMode == E_CameraMode.Aim)
                cameraMode = E_CameraMode.Normal;
        }
    }

    public void SetCameraAnimation(string animName)
    {
        switch (animName)
        {
            case "TurnLeft":
                anim.SetTrigger("TurnLeft");
                break;
            case "TurnRight":
                anim.SetTrigger("TurnRight");
                break;
        }
    }

    void CameraFollow()
    {
        switch (cameraMode)
        {
            case E_CameraMode.Normal:
                cameraDistance += Input.GetAxis("Mouse ScrollWheel");//通过滚轮控制相机距离
                cameraDistance = Mathf.Clamp(cameraDistance, 4, 15);//设置相机最远最近距离
                cameraAngle -= Input.GetAxis("Mouse Y") * 2;
                cameraAngle = Mathf.Clamp(cameraAngle, -90, 90);//夹紧函数控制相机角度边界值;

                cameraPos = playerObj.transform.position + playerObj.transform.up * overHeadDis;//计算出距离头顶的位置
                cameraDir = Quaternion.AngleAxis(cameraAngle, playerObj.transform.right) * -playerObj.transform.forward;//
                cameraPos += cameraDir * cameraDistance;
                cameraObj.transform.position = cameraPos;
                cameraObj.transform.rotation = Quaternion.LookRotation(-cameraDir);
                break;
            case E_CameraMode.Aim:
                cameraAngle -= Input.GetAxis("Mouse Y") * 2;
                cameraAngle = Mathf.Clamp(cameraAngle, -90, 90);//夹紧函数控制相机角度边界值;
                cameraDistance = 2;
                cameraPos = playerObj.transform.position + playerObj.transform.up * overHeadDis;//计算出距离头顶的位置
                cameraPos += playerObj.transform.right;
                cameraDir = Quaternion.AngleAxis(cameraAngle, playerObj.transform.right) * -playerObj.transform.forward;//
                cameraPos += cameraDir * cameraDistance;
                cameraObj.transform.position = cameraPos;
                cameraObj.transform.rotation = Quaternion.LookRotation(-cameraDir);
                break;
        }
    }

    public void SetPlayer(Player playerObj)
    {
        this.playerObj = playerObj;
        isPlayerSet = true;
    }
}
