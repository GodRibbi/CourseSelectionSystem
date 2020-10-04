using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class yonghuliebiao : MonoBehaviour
{
    public Transform yonghumoban;
    public ScrollRect liebiao;

    public bool iskechen;

    // Update is called once per frame
    private static yonghuliebiao instance;
    public static yonghuliebiao Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }
    void Awake()
    {
        Instance = this;
        if (iskechen)
            All.Instance.kechencuanjian();
        else
            chuanzaoshuoyou();
    }
    public GameObject chuanzaodangeyonhu()
    {
        GameObject go = GameObject.Instantiate(yonghumoban.gameObject, liebiao.content);
        go.SetActive(true);
        return go;
    }

    public void chuanzaoshuoyou()
    {
        for (int i = 0; i < All.user.Count; i++)
        {
            GameObject go = chuanzaodangeyonhu();
            All.Instance.setdatateacher(i, go);
        }
    }


}
