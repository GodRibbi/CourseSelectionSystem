using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonmanager : MonoBehaviour
{
    
    public GameObject img;
    public GameObject text;
    public InputField ID;
    public InputField key;



    void Awake()
    {
            
    }
    //场景转换

    public void xuankexiangqin()
    {
        SceneManager.LoadScene("选课详情");
    }

    public void zhuche()
    {
        SceneManager.LoadScene("注册");
    }


    public void laoshifanhuidenlu()
    {
        All.Instance.ismanager = false;
        SceneManager.LoadScene("登录界面");
    }

    public void zhentiankechen()
    {
        SceneManager.LoadScene("选课编辑");
    }

    public void xiugaixinxi()
    {
        SceneManager.LoadScene("修改信息");
    }

    public void fanhuidenlu()
    {
        SceneManager.LoadScene("登录界面");
    }

    public void xueshenguanli()
    {
        SceneManager.LoadScene("学生管理");
    }

    public void xuankejiemian()
    {
        SceneManager.LoadScene("选课界面");
    }

    public void kaishidenlu()
    {
        
        if (IDquedin()==1)
        {
            All.Instance.ismanager = true;
            SceneManager.LoadScene("学生管理");
        }
        else if (IDquedin()==2)
        {
            SceneManager.LoadScene("选课详情");
        }
        else
        {
            text.GetComponent<Text>().text = "密码或用户名错误，请重新输入";
            img.SetActive(true);
        }
    }

    public void xiaoshi()
    {
        img.SetActive(false);
    }

    public int IDquedin()
    {
        string thisID = string.Copy(ID.text);
        string thiskey = string.Copy(key.text);
        if (thisID == All.Instance.managerID && thiskey == All.Instance.managerkey)
        {
            return 1;
        }
        else if (All.Instance.jiancazhanhao(thisID, thiskey))
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
