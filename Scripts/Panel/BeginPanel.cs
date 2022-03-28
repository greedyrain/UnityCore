using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button startBtn, settingBtn, aboutBtn, quitBtn;
    public override void Init()
    {
        startBtn.onClick.AddListener(() =>
        {
            //进入角色选择面板
            UIManager.Instance.ShowPanel<HeroChoosePanel>();
            UIManager.Instance.HidePanel<BeginPanel>();
        });
        settingBtn.onClick.AddListener(() =>
        {
            //打开设置面板；
            UIManager.Instance.ShowPanel<SettingPanel>();
            UIManager.Instance.HidePanel<BeginPanel>();
        });
        aboutBtn.onClick.AddListener(() =>
        {
            //打开制作人员名单列表
        });
        quitBtn.onClick.AddListener(() =>
        {
            //退出游戏
            Application.Quit();
        });
    }
}
