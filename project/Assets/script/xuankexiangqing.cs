using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class xuankexiangqing : MonoBehaviour
{
    public GameObject moban;
    public GameObject weizhi;
    public GameObject tips;

    private GameObject[,] kechenliebiao = new GameObject[7, 5];
    private int classroom;
    void Awake()
    {
        jianchexuanke();
    }
    public GameObject chuanzaodangeyonhu()
    {
        GameObject go = GameObject.Instantiate(moban.gameObject, weizhi.transform);
        return go;
    }

    public void chuanzaoshuoyou()
    {

        for (int j = 0; j < 5; j++)
            for (int i = 0; i < 7; i++)
            {
                kechenliebiao[i, j] = chuanzaodangeyonhu();
                kechenliebiao[i, j].SetActive(true);
                kechenliebiao[i, j].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                kechenliebiao[i, j].transform.Find("按钮").gameObject.SetActive(false);
            }
    }

    public void jianchexuanke()
    {
        if (All.user[All.usercount].classindex == null)
            return;
        chuanzaoshuoyou();
        for (int i = 0; i < All.Classes.Count; i++)
        {
            if (All.Classes[i].Academyindex == All.user[All.usercount].Academyindex && All.Classes[i].Subjectindex == All.user[All.usercount].Subjectindex)
            {
                classroom = i;
                break;
            }
        }
        foreach (int i in All.user[All.usercount].classindex)
        {
            for (int j = 0; j < All.Classes[classroom].Lessons.Count; j++)
            {
                if (All.Classes[classroom].Lessons[j].ID == i)
                {
                    for (int v = 0; v < All.Classes[classroom].Lessons[j].weektimes.Count; v++)
                    {
                        Debug.Log(All.Classes[classroom].Lessons[j].weektimes[v] + " " + All.Classes[classroom].Lessons[j].daytimes[v]);
                        kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                            GetComponent<Image>().color = new Color(1, 1, 1, 1);
                        kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                            transform.Find("文本").GetComponent<Text>().text = All.Classes[classroom].Lessons[j].name.ToString();
                        kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                            transform.Find("按钮").gameObject.SetActive(true);
                        kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                            transform.Find("按钮").GetComponent<Button>().onClick.AddListener(
                            delegate () { tuixuan(i); });
                    }
                }
            }
        }
    }

    public void tuixuan(int index)
    {

        for (int i = 0; i < All.user[All.usercount].classindex.Count; i++)
        {
            Debug.Log(i + " " + index);

            if (All.user[All.usercount].classindex[i] == index)
            {
                All.user[All.usercount].classindex.RemoveAt(i);
                Debug.Log(All.user[All.usercount].classindex.Count);
            }
        }
        for (int j = 0; j < All.Classes[classroom].Lessons.Count; j++)
        {
            if (All.Classes[classroom].Lessons[j].ID == index)
            {
                for (int v = 0; v < All.Classes[classroom].Lessons[j].weektimes.Count; v++)
                {
                    Debug.Log(All.Classes[classroom].Lessons[j].weektimes[v] + " " + All.Classes[classroom].Lessons[j].daytimes[v]);
                    kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                        GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                        transform.Find("文本").GetComponent<Text>().text = "";
                    kechenliebiao[All.Classes[classroom].Lessons[j].weektimes[v], All.Classes[classroom].Lessons[j].daytimes[v]].
                        transform.Find("按钮").gameObject.SetActive(false);
                }
            }
        }
        tips.SetActive(true);
    }

    public void tip()
    {
        tips.SetActive(false);
    }
}
