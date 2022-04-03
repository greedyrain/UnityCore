using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    int levelID,maxWave,currentWave=0;
    public int resetCount = 0,zombieAmount = 0;

    public Transform startPos;
    Player playerObj;
    AudioSource audioSource;

    public List<ZombieSpawnPos> spawnList;//外部拖拽进list里，GameManager一开始就会将这些点一个个初始化；
    public bool isCheck = false, isLevelClear,isFail=false;

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
        print(Application.persistentDataPath);
        Init(GameDataManager.Instance.InGameData.selectHeroID);
        audioSource = GetComponent<AudioSource>();
        SetMusicVolume(GameDataManager.Instance.MusicData.musicVolume);
        SetMusicOnOrOff(GameDataManager.Instance.MusicData.isMusicOn);
        levelID = GameDataManager.Instance.InGameData.levelID;
        maxWave = GameDataManager.Instance.LevelData[levelID - 1].maxWave;
        foreach (ZombieSpawnPos spawnPos in spawnList)
        {
            spawnPos.Init(levelID);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFail)
        {
            UIManager.Instance.HidePanel<GamePanel>();
            TipsPanel tipsPanel = UIManager.Instance.ShowPanel<TipsPanel>();
            tipsPanel.ChangeText("Game Over!", "You Fail!", "0$");
        }
        //如果在check状态内，当僵尸的数量归零时，则触发游戏胜利；
        if (isCheck)
        {
            if (zombieAmount<=0)
            {
                isLevelClear = true;
                //通关，弹出通关面板；
                UIManager.Instance.HidePanel<GamePanel>();
                TipsPanel tipsPanel = UIManager.Instance.ShowPanel<TipsPanel>();
                tipsPanel.ChangeText("Congratulations!", "Level Clear!", $"{GameDataManager.Instance.LevelData[levelID-1].reward}$");
                //通关后修改玩家的金钱；
                GameDataManager.Instance.PlayerData.money += GameDataManager.Instance.LevelData[levelID - 1].reward;
                GameDataManager.Instance.SavePlayerData();
                isCheck = false;
                //通关后解锁下一个关卡；
                GameDataManager.Instance.PlayerData.unlockLevelID.Add(levelID + 1);
            }
        }
    }

    //生成游戏人物，start时会调用；
    public void Init(int roleID)
    {
        playerObj = Instantiate(Resources.Load<Player>($"Role/{roleID}"), startPos.position, startPos.rotation);
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

    public void ResetWaveCounter()
    {
        //外部触发的计数器，当生成点生成完了所有的僵尸之后，会增加技术，计数器数值满足条件后（所有生成点都生成完所有僵尸）则重置生成点的数值；
        //触发该方法的条件是，波数还没有到上限；
        if (currentWave <= maxWave)
        {
            resetCount++;
            if (resetCount == spawnList.Count && currentWave < maxWave)
            {
                foreach (ZombieSpawnPos spawnPos in spawnList)
                {
                    spawnPos.ResetStatus();
                }
                resetCount = 0;
                currentWave++;
                UIManager.Instance.GetPanel<GamePanel>().SetWaveText(currentWave);
            }
            //当所所有的僵尸生成点生成完所有的僵尸后，进入check状态，检查场景内所剩余的僵尸；
            if (currentWave == maxWave && resetCount == spawnList.Count)
                isCheck = true;
        }
    }

    public void AddZombieAmount()
    {
        zombieAmount++;
    }

    public void ReduceZombieAmount()
    {
        zombieAmount--;
    }
}
