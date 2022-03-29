using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapChoosePanel : BasePanel
{
    public Button leftBtn, rightBtn,backBtn,startBtn;
    public TMP_Text mapName, mapDescription;
    public override void Init()
    {
        leftBtn.onClick.AddListener(() =>
        {
            //切换显示的地图信息；
        });

        rightBtn.onClick.AddListener(() =>
        {
            //切换显示的地图信息；
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
            UIManager.Instance.HidePanel<MapChoosePanel>();
            UIManager.Instance.ShowPanel<GamePanel>();
            SceneManager.LoadScene("GameScene");
        });
    }
}
