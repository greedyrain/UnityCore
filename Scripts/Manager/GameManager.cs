using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform startPos;
    Player playerObj;
    Camera camera;
    Vector3 cameraPos,cameraDir;
    float cameraDistance = 5,cameraAngle = 30,overHeadDis = 3;
    // Start is called before the first frame update
    void Start()
    {
        Init(4);
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }

    public void Init(int roleID)
    {
        playerObj = Instantiate(Resources.Load<Player>($"Role/{roleID}"));
        playerObj.transform.position = startPos.position;
    }

    void CameraFollow()
    {
        cameraDistance += Input.GetAxis("Mouse ScrollWheel");//通过滚轮控制相机距离
        cameraDistance = Mathf.Clamp(cameraDistance,3, 10);//设置相机最远最近距离
        if (Input.GetKeyDown(KeyCode.UpArrow))//设置相机俯视角度
            cameraAngle += 10;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            cameraAngle -= 10;
        cameraAngle = Mathf.Clamp(cameraAngle, 10, 70);//夹紧函数控制相机角度边界值
                                                       //；
        cameraPos = playerObj.transform.position + playerObj.transform.up * overHeadDis;//计算出距离头顶的位置
        cameraDir = Quaternion.AngleAxis(cameraAngle, playerObj.transform.right) * -playerObj.transform.forward;//
        cameraPos += cameraDir * cameraDistance;
        camera.transform.position = cameraPos;
        camera.transform.rotation = Quaternion.LookRotation(-cameraDir);
    }
}
