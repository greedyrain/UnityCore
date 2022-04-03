using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    //GameObject canvas;
    //private void Awake()
    //{
    //    canvas = GameObject.Find("Canvas");
    //    if (canvas == null)
    //    {
    //        Instantiate(Resources.Load<GameObject>("UI/Canvas"));
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowPanel<BeginPanel>();
    }
}
