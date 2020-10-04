using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class xuankebianji : MonoBehaviour
{
    public Dropdown academy;
    public Dropdown subject;

    public InputField Name;
    public InputField jieshu;

    public List<GameObject> Go;
    public GameObject moban;
    public ScrollRect liebiao;
    private int j = 0;
    public GameObject tip;

    private int academyindex;
    private int subjectindex;
    private bool issubjectcheck = false;

    private int index;

    private List<string> daytime = new List<string> { "1-2节", "3-4节", "5-6节", "7-8节", "9-11节" };
    private List<string> weektime = new List<string> { "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
    private static List<string> classtimes = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //清空列表
        academy.ClearOptions();
        //增加选项
        academy.AddOptions(All.Instance.academynames);
    }

    // Update is called once per frame
    void Update()
    {
        academy.onValueChanged.AddListener(checkacademyname);
        subject.onValueChanged.AddListener(checksubjectname);
    }



    public void checkacademyname(int value)
    {
        academyindex = value;
        switch (value)
        {
            case 1:
                subject.ClearOptions();
                subject.AddOptions(All.Instance.computernames);
                issubjectcheck = true;
                break;
            case 2:
                subject.ClearOptions();
                subject.AddOptions(All.Instance.moneynames);
                issubjectcheck = true;
                break;
        }
    }

    public void checksubjectname(int value)
    {
        subjectindex = value;
        if (value == 0)
        {
            issubjectcheck = false;
        }
        else
        {
            issubjectcheck = true;
        }
    }


    public void jieshuquedin()
    {
        int js;
        js = int.Parse(jieshu.text);
        if (js == j)
        {
            return;
        }
        else if (js > j)
        {
            for (int i = j; i < js; i++)
            {
                GameObject go = chuanzaodangeyonhu();
                Go.Add(go);

            }
        }
        else if (js < j)
        {
            for (int i = j - 1; i > js - 1; i--)
            {
                Go[i].SetActive(false);
                Go.RemoveAt(i);
            }
        }
        for (int i = 0; i < Go.Count; i++)
        {
            Go[i].SetActive(true);
        }
        j = js;

    }

    public GameObject chuanzaodangeyonhu()
    {
        GameObject go = GameObject.Instantiate(moban, liebiao.content);
        return go;
    }
    public void tijiao()
    {
        for (int i = 0; i < Go.Count; i++)
        {
            for (int l = 0; l < Go.Count; l++)
            {
                if (i == l)
                {
                    continue;
                }
                if (Go[i].transform.Find("星期").GetComponent<Dropdown>().value == Go[l].transform.Find("星期").GetComponent<Dropdown>().value
                    && Go[i].transform.Find("天").GetComponent<Dropdown>().value == Go[l].transform.Find("天").GetComponent<Dropdown>().value)
                {
                    tip.SetActive(true);
                    tip.transform.Find("Text").GetComponent<Text>().text = "上课时间冲突";
                    return;
                }
            }
        }
        for (int i = 0; i < 12; i++)
        {
            if (All.Classes[i].Academyindex == academyindex && All.Classes[i].Subjectindex == subjectindex)
            {
                index = i;
                break;
            }
        }
        lesson Le = new lesson();
        All.classID++;
        Le.ID = All.classID;
        Le.name = Name.text;
        Le.jieshu = int.Parse(jieshu.text);
        Le.times = new List<string>();
        if (Le.daytimes == null)
        {
            Le.daytimes = new List<int>();
        }
        if (Le.weektimes == null)
        {
            Le.weektimes = new List<int>();
        }
        for (int i = 0; i < Go.Count; i++)
        {
            Debug.Log(weektime[Go[i].transform.Find("星期").GetComponent<Dropdown>().value] + " " + daytime[Go[i].transform.Find("天").GetComponent<Dropdown>().value]);
            Le.times.Add(weektime[Go[i].transform.Find("星期").GetComponent<Dropdown>().value] + " " + daytime[Go[i].transform.Find("天").GetComponent<Dropdown>().value]);
            Le.daytimes.Add(Go[i].transform.Find("天").GetComponent<Dropdown>().value);
            Le.weektimes.Add(Go[i].transform.Find("星期").GetComponent<Dropdown>().value);
            foreach (string str in classtimes)
            {
                for (int j = 0; j < Le.daytimes.Count; j++)
                {
                    if (str == ((Le.daytimes[j] + 1) * 10 + Le.weektimes[j]).ToString())
                    {
                        tip.SetActive(true);
                        tip.transform.Find("Text").GetComponent<Text>().text = "时间重复！";
                        return;
                    }
                }
            }
        }
        if (All.Classes[index].Lessons == null)
        {
            All.Classes[index].Lessons = new List<lesson>();
        }
        foreach (lesson Lesson in All.Classes[index].Lessons)
        {
            if (Lesson.name == Le.name)
            {
                tip.SetActive(true);
                tip.transform.Find("Text").GetComponent<Text>().text = "课程名字重复！";
                return;
            }
        }
        for (int j = 0; j < Le.daytimes.Count; j++)
            classtimes.Add(((Le.daytimes[j] + 1) * 10 + Le.weektimes[j]).ToString());
        All.Classes[index].Lessons.Add(Le);
        Debug.Log(All.Classes[index].Lessons.Count);
        tip.SetActive(true);
        tip.transform.Find("Text").GetComponent<Text>().text = "提交成功";
    }
    public void tips()
    {
        tip.SetActive(false);
    }
}
