using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerButtonObj : MonoBehaviour
{
    int id;
    public Button button;
    public TMP_Text hotKey, cost;
    TowerSetPos setPos;
    public Image preview;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //按钮出现时，先获得ID和hotkey，之后当按下hotkey时，会判断金钱是否足够，如果够则创建，不够则没有反馈；
        if (Input.inputString == $"{hotKey.text}")
        {
            if (GameDataManager.Instance.InGameData.inGameMoney >= GameDataManager.Instance.TowersData[id - 1].cost)
            {
                GameDataManager.Instance.InGameData.inGameMoney -= GameDataManager.Instance.TowersData[id - 1].cost;
                setPos.SetTower(id);
                UIManager.Instance.HidePanel<TowerPanel>();
            }
            else
            {

            }
        }
    }

    public void Init(TowerSetPos setPos, TowerData data)
    {
        //由TowerPanel动态创建时读取的数据传入id。
        this.setPos = setPos;
        id = data.id;
        button.onClick.AddListener(() =>
        {
            setPos.SetTower(id);
            UIManager.Instance.HidePanel<TowerPanel>();
        });
        //设置快捷键的文本和花费文本；
        hotKey.text = data.hotKey.ToString();
        cost.text = data.cost.ToString();
        preview.sprite = Resources.Load<Sprite>($"{data.imgRes}");
    }
}
