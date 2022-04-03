using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum E_WeaponType { Grenade, Bullet, knife }

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get { return instance; }
    }

    CharacterController cc;
    public int id;
    public int HP, maxHP, atk;
    public float moveH, moveV;
    public float moveSpeed, rotateSpeed;
    Animator anim;
    bool isDead = false;
    public Transform weaponContainer;
    Ray cameraRay;
    RaycastHit hitInfo;
    List<Transform> firePosList = new List<Transform>();
    public E_WeaponType weaponType;

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
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CameraObj.Instance.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        #region GameScene Code
        //人物转动相关：
        //在GameScene中，鼠标左右移动就能够改变人物移动的方向；
        if (SceneManager.GetActiveScene().name != "BeginScene" && !GameManager.Instance.isLevelClear && !GameManager.Instance.isFail)
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
            //人物转动代码
            transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 5, transform.up);
            SetAnimate();
        }

        //如果在BeginScene中（选择人物界面），要转动人物，则需要按下右键；
        else if (SceneManager.GetActiveScene().name == "BeginScene")
        {
            if (Input.GetMouseButton(1))
                transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -5, transform.up);
            cc.SimpleMove(Vector3.zero);
        }
        #endregion
    }

    void SetAnimate()
    {
        //在GameScene中才会设置动画；
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "GameScene")
            anim.SetTrigger("Attack");
        anim.SetBool("IsDead", isDead);
        switch (CameraObj.Instance.cameraMode)
        {
            case E_CameraMode.Aim:
                anim.SetBool("IsAim", true);
                break;
            case E_CameraMode.Normal:
                anim.SetBool("IsAim", false);
                break;
        }
    }

    //刀攻击触发的事件；
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(weaponContainer.position, 1f);
        foreach (Collider coll in colliders)
        {
            if (coll.CompareTag("Enemy"))
            {
                coll.GetComponent<ZombieObj>().GetHurt(GameDataManager.Instance.WeaponsData[id - 1].atk);
            }
        }
    }

    public void GrenadeShootEvent()
    {
        //生成子弹
        foreach (Transform pos in firePosList)
        {
            BulletObj bullet = new BulletObj();
            switch (weaponType)
            {
                case E_WeaponType.Grenade:
                    bullet = Instantiate(Resources.Load<BulletObj>("Bullet/Grenade"));
                    break;
                case E_WeaponType.Bullet:
                    bullet = Instantiate(Resources.Load<BulletObj>("Bullet/Bullet"));
                    break;
            }
            bullet.SetBulletOwner(gameObject);
            bullet.transform.position = pos.position;//设置子弹的初始位置
            cameraRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Input.mousePosition.z));//创建屏幕中心发射出的射线；
            if (Physics.Raycast(cameraRay, out hitInfo, 1000))//进行射线检测，如果碰到物体，则让子弹朝着物体前进；
                bullet.transform.LookAt(hitInfo.point);
            else//如果没有碰到物体，则让子弹朝着屏幕中心射线的终点飞去；
                bullet.transform.LookAt(cameraRay.GetPoint(1000));
        }
    }

    public void GunShootEvent()
    {
        //生成子弹
        foreach (Transform pos in firePosList)
        {
            GameObject bullet = new GameObject();
            switch (weaponType)
            {
                case E_WeaponType.Grenade:
                    bullet = Instantiate(Resources.Load<GameObject>("Bullet/Grenade"));
                    break;
                case E_WeaponType.Bullet:
                    bullet = Instantiate(Resources.Load<GameObject>("Bullet/Bullet"));
                    break;
            }
            bullet.transform.position = pos.position;//设置子弹的初始位置
            cameraRay = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Input.mousePosition.z));//创建屏幕中心发射出的射线；
            if (Physics.Raycast(cameraRay, out hitInfo, 1000))//进行射线检测，如果碰到物体，则让子弹朝着物体前进；
                bullet.transform.LookAt(hitInfo.point);
            else//如果没有碰到物体，则让子弹朝着屏幕中心射线的终点飞去；
                bullet.transform.LookAt(cameraRay.GetPoint(1000));
        }
    }

    public void Init(int id)
    {
        HP = GameDataManager.Instance.HerosData[id - 1].hp;
        maxHP = GameDataManager.Instance.HerosData[id - 1].hp;
        string weaponPath = GameDataManager.Instance.WeaponsData[id - 1].res;
        Instantiate(Resources.Load<GameObject>(weaponPath), weaponContainer);

    }

    public void GetHurt(int damage)
    {
        if (isDead)
            return;
        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            anim.SetBool("IsDead", isDead);
        }
    }

    public void SetFirePos(Transform firePos)
    {
        firePosList.Add(firePos);
    }

    public void SetWeaponType(E_WeaponType type)
    {
        weaponType = type;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(weaponContainer.position, 1f);
    }
}
