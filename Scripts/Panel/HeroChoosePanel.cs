using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroChoosePanel : BasePanel
{
    public Button leftBtn, rightBtn, buyBtn, backBtn, startBtn;
    public TMP_Text description, money, unlockMoney;
    GameObject playerObj;
    Transform heroCreatePos;
    int heroID = 1;
    public override void Init()
    {
        leftBtn.onClick.AddListener(() =>
        {
            //切换场景上显示的角色-1
            heroID--;
            if (heroID <= 0)
                heroID = GameDataManager.Instance.HerosData.Count;
            CreatHero(heroID);
        });

        rightBtn.onClick.AddListener(() =>
        {
            //切换场景上显示的角色+1
            heroID++;
            if (heroID > GameDataManager.Instance.HerosData.Count)
                heroID = 1;
            CreatHero(heroID);
        });

        buyBtn.onClick.AddListener(() =>
        {
            //判断金钱是否足够
            if (GameDataManager.Instance.PlayerData.money >= GameDataManager.Instance.HerosData[heroID - 1].lockMoney)
            {
                GameDataManager.Instance.PlayerData.money -= GameDataManager.Instance.HerosData[heroID - 1].lockMoney;
                GameDataManager.Instance.PlayerData.unlockHerosID.Add(heroID);
                GameDataManager.Instance.SavePlayerData();
                UpdatePanelData();
            }
            //足够则购买该英雄
            //修改玩家数据中已解锁的英雄
        });

        backBtn.onClick.AddListener(() =>
        {
            //回到BeginPanel
            UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<HeroChoosePanel>();
            CameraObj.Instance.SetCameraAnimation("TurnRight");
            Destroy(playerObj);
            //相机运动回去
        });

        startBtn.onClick.AddListener(() =>
        {
            //进入选择场景界面
            GameDataManager.Instance.InGameData.selectHeroID = heroID;
            UIManager.Instance.ShowPanel<MapChoosePanel>();
            UIManager.Instance.HidePanel<HeroChoosePanel>();
            Destroy(playerObj);
        });

        heroCreatePos = GameObject.Find("HeroCreatPos").transform;
        CreatHero(heroID);
    }

    public void CreatHero(int id)
    {
        //生成人物模型，设置位置；
        Destroy(playerObj);
        //创建一个人物模型
        playerObj = Instantiate(Resources.Load<GameObject>(GameDataManager.Instance.HerosData[id - 1].res),
                                heroCreatePos.position, heroCreatePos.rotation);
        //根据人物模型ID创建对应的武器；
        playerObj.GetComponent<Player>().Init(GameDataManager.Instance.HerosData[id - 1].defaultWeapon);
        //修改界面上的信息；
        UpdatePanelData();
    }

    public void UpdatePanelData()
    {
        if (GameDataManager.Instance.PlayerData.unlockHerosID.Contains(heroID))
        {
            buyBtn.gameObject.SetActive(false);
            startBtn.gameObject.SetActive(true);
        }
        else
        {
            buyBtn.gameObject.SetActive(true);
            startBtn.gameObject.SetActive(false);
        }
        money.text = GameDataManager.Instance.PlayerData.money.ToString();
        description.text = GameDataManager.Instance.HerosData[heroID - 1].tips;
        unlockMoney.text = GameDataManager.Instance.HerosData[heroID - 1].lockMoney.ToString();
    }
}
