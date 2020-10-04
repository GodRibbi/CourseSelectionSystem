using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class save_load : MonoBehaviour
{
    void Awake()
    {
        Load();
    }
    public void exitsave()
    {
        Save save = new Save();
        if (save.class_save == null)
        {
            save.class_save = new List<classes>();
        }
        if (save.student_save == null)
        {
            save.student_save = new List<students>();
        }
        foreach (classes classsave in All.Classes)
        {
            save.class_save.Add(classsave);
        }
        foreach (students stu in All.user)
        {
            save.student_save.Add(stu);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("quit");
        Application.Quit();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/gamesave.save") && !All.isload)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            Debug.Log(save.class_save.Count);
            Debug.Log(save.student_save.Count);
            file.Close();

            All.user = new List<students>();
            All.Classes = new List<classes>();
            foreach (classes classsave in save.class_save)
            {
                All.Classes.Add(classsave);
            }
            foreach (students stu in save.student_save)
            {
                All.user.Add(stu);
            }
            Debug.Log(All.user.Count);
            Debug.Log(All.user[0].Academyindex + " " + All.user[0].Key + " " + All.user[0].email + " " +
                All.user[0].ID + " " + All.user[0].Matrikelnummer + " " + All.user[0].name + " " +
                All.user[0].Subjectindex + " " + All.user[0].TEL);
            Debug.Log("yes");
            zhucheguanli.CountID = All.user.Count;
            All.isload = true;
        }
        else if (!All.isload)
        {
            All.Classes = new List<classes>(new classes[]
        {
            new classes(){ Academyindex = 1, Subjectindex = 1 },//"计算机技术与科学", "软件工程一班", "软件工程二班", "网络工程", "信息管理", "电子商务"
            new classes(){ Academyindex = 1, Subjectindex = 2 },
            new classes(){ Academyindex = 1, Subjectindex = 3 },
            new classes(){ Academyindex = 1, Subjectindex = 4 },
            new classes(){ Academyindex = 1, Subjectindex = 5 },
            new classes(){ Academyindex = 1, Subjectindex = 6 },
            new classes(){ Academyindex = 2, Subjectindex = 1 },//"金融学", "工商管理", "会计学", "人力资源", "统计学", "管理学"
            new classes(){ Academyindex = 2, Subjectindex = 2 },
            new classes(){ Academyindex = 2, Subjectindex = 3 },
            new classes(){ Academyindex = 2, Subjectindex = 4 },
            new classes(){ Academyindex = 2, Subjectindex = 5 },
            new classes(){ Academyindex = 2, Subjectindex = 6 },
        });
            All.user = new List<students>();
            All.isload = true;
        }
    }
}
