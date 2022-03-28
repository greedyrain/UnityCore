using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TipsPanel : BasePanel
{
    public TMP_Text title,description,reward;
    public Button confirmBtn;
    public override void Init()
    {
        confirmBtn.onClick.AddListener(() =>
        {
            //关闭TipsPanel，返回主菜单；
            UIManager.Instance.HidePanel<TipsPanel>();
            SceneManager.LoadScene("BeginScene");
            UIManager.Instance.ShowPanel<BeginPanel>();
        });
    }

    public void ChangeText(string title,string description,string reward)
    {
        this.title.text = title;
        this.description.text = description;
        this.reward.text = reward;
    }
}
