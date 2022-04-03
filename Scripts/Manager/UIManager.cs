using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance
    {
        get { return instance; }
    }
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    public Transform canvas;

    private UIManager()
    {
        //canvas = GameObject.Find("Canvas").transform;
        if (canvas == null)
        {
            canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas")).transform;
        }
        GameObject.DontDestroyOnLoad(canvas.gameObject);
    }
    public T GetPanel<T>() where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        return null;
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>($"UI/{panelName}"), canvas);

        T panel = panelObj.GetComponent<T>();
        panelDic.Add(panelName,panel);
        panel.ShowMe();

        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if (isFade)
            {
                panelDic[panelName].HideMe(()=>
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
}
