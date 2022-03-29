using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public Transform startPos;
    Player playerObj;
    Camera camera;
    Vector3 cameraPos, cameraDir;
    float cameraDistance = 8, cameraAngle = 20, overHeadDis = 3;
    AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init(GameDataManager.Instance.InGameData.selectHeroID);
        camera = Camera.main;
        audioSource = GetComponent<AudioSource>();
        SetMusicVolume(GameDataManager.Instance.MusicData.musicVolume);
        SetMusicOnOrOff(GameDataManager.Instance.MusicData.isMusicOn);
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }

    //生成游戏人物，start时会调用；
    public void Init(int roleID)
    {
        playerObj = Instantiate(Resources.Load<Player>($"Role/{roleID}"));
        playerObj.transform.position = startPos.position;
        playerObj.Init(GameDataManager.Instance.HerosData[roleID - 1].defaultWeapon);
    }

    //设置音乐音量，供外部调用；
    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
        GameDataManager.Instance.MusicData.musicVolume = volume;
        GameDataManager.Instance.SaveMusicData();
    }

    //设置是否静音，供外部调用；
    public void SetMusicOnOrOff(bool isOn)
    {
        audioSource.mute = !isOn;
        GameDataManager.Instance.MusicData.isMusicOn = isOn;
        GameDataManager.Instance.SaveMusicData();
    }

    void CameraFollow()
    {
        cameraDistance += Input.GetAxis("Mouse ScrollWheel");//通过滚轮控制相机距离
        cameraDistance = Mathf.Clamp(cameraDistance, 4, 15);//设置相机最远最近距离
        cameraAngle -= Input.GetAxis("Mouse Y") * 2;

        cameraAngle = Mathf.Clamp(cameraAngle, 10, 70);//夹紧函数控制相机角度边界值;

        cameraPos = playerObj.transform.position + playerObj.transform.up * overHeadDis;//计算出距离头顶的位置
        cameraDir = Quaternion.AngleAxis(cameraAngle, playerObj.transform.right) * -playerObj.transform.forward;//
        cameraPos += cameraDir * cameraDistance;
        camera.transform.position = cameraPos;
        camera.transform.rotation = Quaternion.LookRotation(-cameraDir);
    }
}
