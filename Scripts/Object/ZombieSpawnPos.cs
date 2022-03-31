using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPos : MonoBehaviour
{
    public int levelID;
    public int randomID,spawnCount;
    public float spawnCD, remainSpawnCD;
    public bool isDone,isInit = false;
    ZombieSpawnData spawnData;

    // Update is called once per frame
    void Update()
    {
        if (isInit)
        {
            if (!isDone)
            {
                remainSpawnCD -= Time.deltaTime;
                if (remainSpawnCD <= 0)
                    Spawn();
            }
        }
    }

    public void ResetStatus()
    {
        //随机生成每一波的怪物只数；
        spawnCount = Random.Range(spawnData.minCount, spawnData.maxCount);
        isDone = false;
    }

    public void Spawn()
    {
        //随机一个数，让动态实例化方法去寻找对应的资源；
        randomID = Random.Range(1, 12);
        ZombieObj zombie = Instantiate(Resources.Load<ZombieObj>($"Zombie/Zombie{randomID}"));
        GameManager.Instance.AddZombieAmount();
        //设置实例的位置旋转信息；
        zombie.transform.position = transform.position;
        zombie.transform.rotation = transform.rotation;
        //重置CD，count-1
        remainSpawnCD = spawnCD;
        spawnCount--;
        if (spawnCount<1)
        {
            isDone = true;
            GameManager.Instance.ResetWaveCounter();
        }
            
    }

    public void Init(int levelID)
    {
        this.levelID = levelID;
        spawnData = GameDataManager.Instance.ZombieSpawnData[levelID - 1];
        spawnCD = spawnData.spawnCD;
        isInit = true;
    }
}
