using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapChoosePanel : BasePanel
{
    int levelID;
    public Button leftBtn, rightBtn,backBtn,startBtn;
    public TMP_Text mapName, mapDescription;
    public Image mapPreview;
    public override void Init()
    {
        leftBtn.onClick.AddListener(() =>
        {
            //切换显示的地图信息；
            levelID--;
            if (levelID<=0)
            {
                levelID = GameDataManager.Instance.LevelData.Count;
            }
            UpdatePanel(levelID);
        });

        rightBtn.onClick.AddListener(() =>
        {
            //切换显示的地图信息；
            levelID++;
            if (levelID > GameDataManager.Instance.LevelData.Count)
            {
                levelID = 1;
            }
            UpdatePanel(levelID);
        });

        backBtn.onClick.AddListener(() =>
        {
            //返回英雄选择界面；
            UIManager.Instance.ShowPanel<HeroChoosePanel>();
            UIManager.Instance.HidePanel<MapChoosePanel>();
        });

        startBtn.onClick.AddListener(() =>
        {
            //进入游戏界面；
            GameDataManager.Instance.InGameData.levelID = levelID;
            UIManager.Instance.HidePanel<MapChoosePanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
            SceneManager.LoadScene("GameScene");

        });
        levelID = GameDataManager.Instance.InGameData.levelID;
        //if (levelID <= 0)
        //    levelID = 1;
        UpdatePanel(levelID);
    }

    public void UpdatePanel(int id)
    {
        //刷新界面上显示的地图信息；
        mapName.text = GameDataManager.Instance.LevelData[id - 1].levelName;
        mapDescription.text = GameDataManager.Instance.LevelData[id - 1].levelDescription;
        mapPreview.sprite = Resources.Load<Sprite>($"{GameDataManager.Instance.LevelData[id - 1].mapRes}");
        if (GameDataManager.Instance.PlayerData.unlockLevelID.Contains(id))
            startBtn.gameObject.SetActive(true);
        else
            startBtn.gameObject.SetActive(false);
    }
}
