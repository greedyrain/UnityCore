using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            if (SceneManager.GetActiveScene().name != "GameScene")
            {
                UIManager.Instance.ShowPanel<BeginPanel>();
            }
            GameDataManager.Instance.SaveMusicData();
        });

        soundToggle.onValueChanged.AddListener((isOn) =>
        {
            //GameDataManager中的isSoundOn改变
            GameDataManager.Instance.MusicData.isSoundOn = isOn;
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                //GameManager.Instance.SetMusicOnOrOff(isOn);
            }
        });

        musicToggle.onValueChanged.AddListener((isOn) =>
        {
            //GameDataManager中的isMusicOn改变
            GameDataManager.Instance.MusicData.isMusicOn = isOn;
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                GameManager.Instance.SetMusicOnOrOff(isOn);
            }
        });

        soundSlider.onValueChanged.AddListener((volume) =>
        {
            //GameDataManager中的soundVolume改变
            GameDataManager.Instance.MusicData.soundVolume = volume;
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                //GameManager.Instance.SetMusicVolume();
            }
        });

        musicSlider.onValueChanged.AddListener((volume) =>
        {
            //GameDataManager中的musicVolume改变
            GameDataManager.Instance.MusicData.musicVolume = volume;
            if (SceneManager.GetActiveScene().name == "GameScene")
            {
                GameManager.Instance.SetMusicVolume(volume);
            }
        });

        soundToggle.isOn = GameDataManager.Instance.MusicData.isSoundOn;
        musicToggle.isOn = GameDataManager.Instance.MusicData.isMusicOn;
        soundSlider.value = GameDataManager.Instance.MusicData.soundVolume;
        musicSlider.value = GameDataManager.Instance.MusicData.musicVolume;
    }
}
