using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : BasePanel
{
    public Button menuBtn;
    public TMP_Text waveText, moneyText,hpText,zombieText;
    public Image hpImg;
    private int HP, maxHP,currentWave,maxWave;
    public override void Init()
    {
        HP = GameDataManager.Instance.InGameData.protectZoneHP;
        maxHP = GameDataManager.Instance.InGameData.protectZoneHP;
        maxWave = GameDataManager.Instance.LevelData[GameDataManager.Instance.InGameData.levelID-1].maxWave;
        menuBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        UpdateHP(HP);
        SetWaveText(currentWave);
        SetMoneyText(GameDataManager.Instance.InGameData.inGameMoney);
    }

    public void UpdateHP(int currentHP)
    {
        HP = currentHP;
        hpImg.fillAmount = (float)HP / maxHP;

        hpText.text = $"{HP} / {maxHP}";
    }

    public void SetMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void SetWaveText(int wave)
    {
        currentWave = wave;
        waveText.text = $"{currentWave} / {maxWave}";
    }

    public void SetZombieText(int zombieCount)
    {
        zombieText.text = zombieCount.ToString();
    }
}
