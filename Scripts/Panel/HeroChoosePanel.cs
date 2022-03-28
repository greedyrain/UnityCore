using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroChoosePanel : BasePanel
{
    public Button leftBtn, rightBtn, buyBtn, backBtn, startBtn;
    public override void Init()
    {
        leftBtn.onClick.AddListener(()=>
        {
            //切换场景上显示的角色-1
        });

        rightBtn.onClick.AddListener(() =>
        {
            //切换场景上显示的角色+1
        });

        buyBtn.onClick.AddListener(() =>
        {
            //判断金钱是否足够
            //足够则购买该英雄
            //修改玩家数据中已解锁的英雄
        });

        backBtn.onClick.AddListener(() =>
        {
            //回到BeginPanel
            UIManager.Instance.ShowPanel<BeginPanel>();
            UIManager.Instance.HidePanel<HeroChoosePanel>();
            //相机运动回去
        });

        startBtn.onClick.AddListener(() =>
        {
            //进入选择场景界面
            UIManager.Instance.ShowPanel<MapChoosePanel>();
            UIManager.Instance.HidePanel<HeroChoosePanel>();
        });
    }
}
