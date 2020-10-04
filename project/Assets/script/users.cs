using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class students 
{
    
    public int ID;
    public string Matrikelnummer;
    public string name;
    public string email;
    public string TEL;
    public string Key;

    public int Academyindex;
    public int Subjectindex;
    public List<int> classindex;
}
[System.Serializable]
public class lesson
{
    public string name;
    public int ID;
    public List<string> times;
    public List<int> weektimes;
    public List<int> daytimes;
    public int jieshu;
}
[System.Serializable]
public class classes
{
    public List<lesson> Lessons;
    public int Academyindex;
    public int Subjectindex;
}
[System.Serializable]
public class Save
{
    public List<classes> class_save;
    public List<students> student_save;
}