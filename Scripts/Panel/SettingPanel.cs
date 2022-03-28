using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button confirmBtn;
    public Toggle soundToggle, musicToggle;
    public Slider soundSlider, musicSlider;
    public override void Init()
    {
        confirmBtn.onClick.AddListener(() =>
        {
            //关闭界面
            UIManager.Instance.HidePanel<SettingPanel>();
            UIManager.Instance.ShowPanel<BeginPanel>();
        });

        soundToggle.onValueChanged.AddListener((isOn) =>
        {
            //GameDataManager中的isSoundOn改变
        });

        musicToggle.onValueChanged.AddListener((isOn) =>
        {
            //GameDataManager中的isMusicOn改变
        });

        soundSlider.onValueChanged.AddListener((volume) =>
        {
            //GameDataManager中的soundVolume改变
        });

        musicSlider.onValueChanged.AddListener((volume) =>
        {
            //GameDataManager中的musicVolume改变
        });
    }
}
