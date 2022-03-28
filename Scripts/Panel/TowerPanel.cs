using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerPanel : BasePanel
{
    public Button exitBtn;
    public ScrollRect scrollRect;
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
}
