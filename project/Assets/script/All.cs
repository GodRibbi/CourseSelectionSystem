using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class All : MonoBehaviour
{
    public static List<classes> Classes;
    public static bool isload = false;

    public int classroom;

    //学生修改信息
    public GameObject changeinformation;

    //修改信息失败提示框
    public Text tips;
    public GameObject Tips;
    public static int classID = 0;

    //课程具体名称
    public List<string> academynames = new List<string> { "请选择学院", "计算机学院", "经济管理学院" };
    public List<string> computernames = new List<string> { "请先选择学院", "计算机技术与科学", "软件工程一班", "软件工程二班", "网络工程", "信息管理", "电子商务" };
    public List<string> moneynames = new List<string> { "请先选择学院", "金融学", "工商管理", "会计学", "人力资源", "统计学", "管理学" };



    public static List<students> user;

    public static int usercount;
    public bool ismanager = false;
    public string managerID = "123";
    public string managerkey = "123";

    public GameObject Matrikelnummer;
    public GameObject name;

    private static All instance;
    public static All Instance
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
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        if (changeinformation != null)
        {
            setdata(usercount, changeinformation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Matrikelnummer != null)
        {
            Matrikelnummer.GetComponent<Text>().text = user[usercount].Matrikelnummer.ToString();
            name.GetComponent<Text>().text = user[usercount].name.ToString();
        }
    }

    /// <summary>
    /// 储存信息
    /// </summary>
    public void chuchunshuju(students usera)
    {
        user.Add(usera);
        Debug.Log(All.user[0].Academyindex + " " + All.user[0].Key + " " + All.user[0].email + " " +
            All.user[0].ID + " " + All.user[0].Matrikelnummer + " " + All.user[0].name + " " +
            All.user[0].Subjectindex + " " + All.user[0].TEL + " " + user.Count);
    }
    /// <summary>
    /// 登录检查账号
    /// </summary>
    public bool jiancazhanhao(string ID, string key)
    {
        for (int i = 0; i < user.Count; i++)
        {
            if (ID == user[i].Matrikelnummer && key == user[i].Key)
            {
                usercount = i;
                return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 初始化储存信息
    /// </summary>
    public void setdata(int count, GameObject go)
    {
        go.transform.Find("ID").GetComponent<Text>().text = user[count].ID.ToString();
        go.transform.Find("学号").GetComponent<InputField>().text = user[count].Matrikelnummer.ToString();
        go.transform.Find("邮箱").GetComponent<InputField>().text = user[count].email.ToString();
        go.transform.Find("密码").GetComponent<InputField>().text = user[count].Key.ToString();
        go.transform.Find("姓名").GetComponent<InputField>().text = user[count].name.ToString();
        go.transform.Find("手机号").GetComponent<InputField>().text = user[count].TEL.ToString();
        go.transform.Find("学院列表").GetComponent<Dropdown>().ClearOptions();
        go.transform.Find("系列表").GetComponent<Dropdown>().ClearOptions();
        go.transform.Find("学院列表").GetComponent<Dropdown>().AddOptions(All.Instance.academynames);
        Debug.Log(user[count].Subjectindex + " " + user[count].Academyindex);
        if (user[count].Academyindex == 1)
        {
            go.transform.Find("系列表").GetComponent<Dropdown>().AddOptions(All.Instance.computernames);
        }
        else if (user[count].Academyindex == 2)
        {
            go.transform.Find("系列表").GetComponent<Dropdown>().AddOptions(All.Instance.moneynames);
        }
        go.transform.Find("学院列表").GetComponent<Dropdown>().value = user[count].Academyindex;
        go.transform.Find("系列表").GetComponent<Dropdown>().value = user[count].Subjectindex;
        go.transform.Find("按钮").GetComponent<Button>().onClick.AddListener(
            delegate () { changedata(count, go); });
    }

    public void setdatateacher(int count, GameObject go)
    {
        go.transform.Find("ID").GetComponent<Text>().text = user[count].ID.ToString();
        go.transform.Find("学号").GetComponent<Text>().text = user[count].Matrikelnummer.ToString();
        go.transform.Find("邮箱").GetComponent<Text>().text = user[count].email.ToString();
        go.transform.Find("密码").GetComponent<Text>().text = user[count].Key.ToString();
        go.transform.Find("姓名").GetComponent<Text>().text = user[count].name.ToString();
        go.transform.Find("手机号").GetComponent<Text>().text = user[count].TEL.ToString();
        go.transform.Find("学院列表").GetComponent<Text>().text = academynames[user[count].Academyindex];
        if (user[count].Academyindex == 1)
        {
            go.transform.Find("系列表").GetComponent<Text>().text = computernames[user[count].Subjectindex];
        }
        else if (user[count].Academyindex == 2)
        {
            go.transform.Find("系列表").GetComponent<Text>().text = moneynames[user[count].Subjectindex];
        }
        go.transform.Find("按钮").GetComponent<Button>().onClick.AddListener(delegate () { xiugaixinxi(user[count].ID - 1); });
        go.transform.Find("选课详情").GetComponent<Button>().onClick.AddListener(delegate () { xuanke(user[count].ID - 1); });
    }
    /// <summary>
    /// 老师改变信息
    /// </summary> 
    void changedata(int index, GameObject go)
    {
        if (zhucheguanli.Instance.checkchange(tips.gameObject, Tips, go.transform.Find("学号").GetComponent<InputField>().text,
            go.transform.Find("密码").GetComponent<InputField>().text, go.transform.Find("邮箱").GetComponent<InputField>().text,
            go.transform.Find("手机号").GetComponent<InputField>().text, go.transform.Find("姓名").GetComponent<InputField>().text))
        {
            user[index].Matrikelnummer = go.transform.Find("学号").GetComponent<InputField>().text;
            user[index].email = go.transform.Find("邮箱").GetComponent<InputField>().text;
            user[index].Key = go.transform.Find("密码").GetComponent<InputField>().text;
            user[index].name = go.transform.Find("姓名").GetComponent<InputField>().text;
            user[index].TEL = go.transform.Find("手机号").GetComponent<InputField>().text;
            user[index].Academyindex = go.transform.Find("学院列表").GetComponent<Dropdown>().value;
            user[index].Subjectindex = go.transform.Find("系列表").GetComponent<Dropdown>().value;
        }
        else
        {
            return;
        }
    }

    public void checktip()
    {
        Tips.SetActive(false);
    }

    public void xiugaixinxi(int index)
    {
        usercount = index;
        SceneManager.LoadScene("修改信息老师");
    }
    public void kechencuanjian()
    {
        for (int i = 0; ; i++)
        {
            if (Classes[i].Academyindex == user[usercount].Academyindex && Classes[i].Subjectindex == user[usercount].Subjectindex)
            {
                classroom = i;
                break;
            }
        }
        for (int i = 0; i < Classes[classroom].Lessons.Count; i++)
        {
            bool chongfu = false;
            if (user[usercount].classindex == null)
            {
                user[usercount].classindex = new List<int>();
            }
            for (int j = 0; j < user[usercount].classindex.Count; j++)
            {
                if (Classes[classroom].Lessons[i].ID == user[usercount].classindex[j])
                {
                    chongfu = true;
                }
            }
            if (chongfu)
            {
                continue;
            }

            GameObject go = yonghuliebiao.Instance.chuanzaodangeyonhu();
            chuanjiankechenliebiao(go, i);
        }
    }

    public void chuanjiankechenliebiao(GameObject go, int index)
    {
        go.SetActive(true);
        go.transform.Find("课程名称").GetComponent<Text>().text = Classes[classroom].Lessons[index].name.ToString();
        go.transform.Find("时间").GetComponent<Text>().text = Classes[classroom].Lessons[index].times[0].ToString();
        Debug.Log(Classes[classroom].Lessons[index].times[0].ToString());
        for (int i = 1; i < Classes[classroom].Lessons[index].jieshu; i++)
        {
            Debug.Log(Classes[classroom].Lessons[index].times[i].ToString());
            go.transform.Find("时间").GetComponent<Text>().text = go.transform.Find("时间").GetComponent<Text>().text + '\n' + Classes[classroom].Lessons[index].times[i].ToString();
        }
        go.transform.Find("按钮").GetComponent<Button>().onClick.AddListener(
            delegate () { xuankecheck(Classes[classroom].Lessons[index].ID, go); });

    }

    public void xuankecheck(int count, GameObject Go)
    {
        Tips.SetActive(true);
        Go.SetActive(false);
        user[usercount].classindex.Add(count);
    }

    public void xuanke(int index)
    {
        usercount = index;
        SceneManager.LoadScene("选课详情老师");
    }
}

//老师修改信息检查  完成