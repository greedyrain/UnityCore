using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    GameObject bulletOwner;
    public float moveSpeed;
    int damage;
    public E_WeaponType type;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();//先获取物体上的声音脚本，修改脚本的音量，再手动播放（play on awake取消）
        audioSource.mute = !GameDataManager.Instance.MusicData.isSoundOn;
        audioSource.volume = GameDataManager.Instance.MusicData.soundVolume;
        audioSource.Play();
        Destroy(gameObject, 3);
        //子弹的正方向，看向由屏幕中心点射出的一条线；
    }

    // Update is called once per frame
    void Update()
    {
        //子弹生成时会不停向前移动；
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //获得击中的Enemy身上的脚本，执行掉血；
            other.GetComponent<ZombieObj>().GetHurt(damage);
        }
        if (!(other.CompareTag("Player") || other.CompareTag("ProtectZone") || other.CompareTag("Weapon")))
        {
            if (type == E_WeaponType.Grenade)
            {
                GameObject eff = Instantiate(Resources.Load<GameObject>("Effect/ExplosionEffect"));
                AudioSource effAudioSource = eff.GetComponent<AudioSource>();
                effAudioSource.mute = !GameDataManager.Instance.MusicData.isSoundOn;
                effAudioSource.volume = GameDataManager.Instance.MusicData.soundVolume;
                eff.transform.position = transform.position;
                Destroy(eff, 2);
                foreach (Collider collider in Physics.OverlapSphere(eff.transform.position, 5))
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        collider.GetComponent<ZombieObj>().GetHurt(damage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    public void SetBulletOwner(GameObject owner)
    {
        switch (owner.tag)
        {
            case "Player":
                bulletOwner = owner;
                damage = GameDataManager.Instance.WeaponsData[owner.GetComponent<Player>().id - 1].atk;
                break;
            case "Tower":
                bulletOwner = owner;
                damage = GameDataManager.Instance.TowersData[owner.GetComponent<TowerObj>().id - 1].atk;
                break;
        }
    }
}
