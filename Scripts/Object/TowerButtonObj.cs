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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.inputString == $"{hotKey.text}")
        {
            setPos.SetTower(id);
            UIManager.Instance.HidePanel<TowerPanel>();
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
        });
        //设置快捷键的文本和花费文本；
        hotKey.text = data.hotKey.ToString();
        cost.text = data.cost.ToString();
    }
}
