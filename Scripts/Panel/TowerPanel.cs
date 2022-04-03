using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : BasePanel
{
    public Button exitBtn;
    public ScrollRect scrollRect;
    TowerSetPos setPos;
    public override void Init()
    {
        exitBtn.onClick.AddListener(() =>
        {
            //关闭该界面
            UIManager.Instance.HidePanel<TowerPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        //根据GameDataManager的Tower数据生成相应数量的TowerBtnObj;

    }

    public void SetTowerPos(TowerSetPos setPos)
    {
        this.setPos = setPos;
    }

    public void CreateButton()
    {
        for (int i = 0; i < GameDataManager.Instance.TowersData.Count; i++)
        {
            TowerButtonObj towerButtonObj = Instantiate(Resources.Load<TowerButtonObj>("Prefabs/TowerBtnObj"), scrollRect.content);
            towerButtonObj.Init(setPos, GameDataManager.Instance.TowersData[i]);
        }
    }
}
