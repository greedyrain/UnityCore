using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    bool isShow;
    CanvasGroup canvasGroup;

    float alphaSpeed = 10;
    public UnityAction hideCallBack;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }
    protected virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //淡入淡出
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha > 1)
                canvasGroup.alpha = 1;
        }
        else if (!isShow && canvasGroup.alpha >0)
        {
            canvasGroup.alpha -= Time.deltaTime * alphaSpeed;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }
    }

    public abstract void Init();

    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }

    public virtual void HideMe(UnityAction callBack)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallBack = callBack;
    }
}
