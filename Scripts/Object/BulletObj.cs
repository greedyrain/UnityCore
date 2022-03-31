using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    Player bulletOwner;
    public float moveSpeed;
    int damage;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();//先获取物体上的声音脚本，修改脚本的音量，再手动播放（play on awake取消）
        audioSource.mute = !GameDataManager.Instance.MusicData.isSoundOn;
        audioSource.volume = GameDataManager.Instance.MusicData.soundVolume;
        audioSource.Play();
        bulletOwner = FindObjectOfType<Player>();//获取场景中的Player脚本，通过脚本获得ID，去调取枪械的伤害；
        damage = GameDataManager.Instance.WeaponsData[bulletOwner.id - 1].atk;
        Destroy(gameObject, 3);
        //子弹的正方向，看向由屏幕中心点射出的一条线；
    }

    // Update is called once per frame
    void Update()
    {
        //子弹生成时会不停向前移动；
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime,Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //获得击中的Enemy身上的脚本，执行掉血；
            other.GetComponent<ZombieObj>().GetHurt(damage);
        }
    }
}
