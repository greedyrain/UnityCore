using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePanel : BasePanel
{
    public Button menuBtn;
    public TMP_Text waveText, moneyText,hpText;
    public Image hpImg;
    private int HP, maxHP,remainWave,maxWave;
    public override void Init()
    {
        HP = GameDataManager.Instance.InGameData.protectZoneHP;
        maxHP = GameDataManager.Instance.InGameData.protectZoneHP;
        remainWave = GameDataManager.Instance.InGameData.remainWave;
        maxWave = GameDataManager.Instance.InGameData.maxWave;
        menuBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });

        UpdateHP(HP);
        SetWaveText(remainWave);
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
        waveText.text = $"{remainWave} / {maxWave}";
    }
}
