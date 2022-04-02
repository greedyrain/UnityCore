using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager
{
    private InGameData inGameData = new InGameData();
    public InGameData InGameData
    {
        get { return inGameData; }
    }

    private static GameDataManager instance = new GameDataManager();
    public static GameDataManager Instance
    {
        get { return instance; }
    }

    private MusicData musicData;
    public MusicData MusicData
    {
        get { return musicData; }
    }

    private List<HeroData> herosData;
    public List<HeroData> HerosData
    {
        get { return herosData; }
    }

    private PlayerData playerData;
    public PlayerData PlayerData
    {
        get { return playerData; }
    }

    private List<WeaponData> weaponsData;
    public List<WeaponData> WeaponsData
    {
        get { return weaponsData; }
    }

    private List<ZombieData> zombiesData;
    public List<ZombieData> ZombiesData
    {
        get { return zombiesData; }
    }

    private List<ZombieSpawnData> zombieSpawnData;
    public List<ZombieSpawnData> ZombieSpawnData
    {
        get { return zombieSpawnData; }
    }

    private List<LevelData> levelData;
    public List<LevelData> LevelData
    {
        get { return levelData; }
    }

    private List<TowerData> towersData;
    public List<TowerData> TowersData
    {
        get { return towersData; }
    }

    private GameDataManager()
    {
        musicData = JsonManager.Instance.LoadData<MusicData>("MusicData");
        herosData = JsonManager.Instance.LoadData<List<HeroData>>("HerosData");
        playerData = JsonManager.Instance.LoadData<PlayerData>("PlayerData");
        weaponsData = JsonManager.Instance.LoadData<List<WeaponData>>("WeaponsData");
        zombiesData = JsonManager.Instance.LoadData<List<ZombieData>>("ZombiesData");
        zombieSpawnData = JsonManager.Instance.LoadData<List<ZombieSpawnData>>("ZombieSpawnData");
        levelData = JsonManager.Instance.LoadData<List<LevelData>>("LevelData");
        towersData = JsonManager.Instance.LoadData<List<TowerData>>("TowersData");
    }

    public void SaveMusicData()
    {
        JsonManager.Instance.SaveData(musicData, "MusicData");
    }

    public void SavePlayerData()
    {
        JsonManager.Instance.SaveData(playerData, "PlayerData");
    }
}
