using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class zhucheguanli : MonoBehaviour
{
    public Dropdown academy;
    public Dropdown subject;
    private bool issubjectcheck = false;
    private int academyindex;
    private int subjectindex;
    public bool ischange;

    public GameObject ID;
    public GameObject key;
    public GameObject Email;
    public GameObject checkkey;
    public GameObject TEL;
    public GameObject Name;


    public GameObject jiemian;
    public GameObject Tips;

    private string thisID;
    private string thiskey;
    private string thisEmail;
    private string thischeckkey;
    private string thisTEL;
    private string thisName;

    private static int countID = 0;

    private string chuowuxinxi0 = "有空未填写";
    private string chuowuxinxi1 = "两次密码不一样请确认";
    private string chuowuxinxi2 = "电话号码格式错误，请重新输入";
    private string chuowuxinxi3 = "电子邮箱格式错误，请重新输入";
    private string chuowuxinxi4 = "未选择班级信息，请重新检查";
    private string chuowuxinxi5 = "注册成功";
    private string chuowuxinxi6 = "修改成功";
    private string chuowuxinxi7 = "学号重复，请重新输入";

    private bool shifouchengon = false;


    //正则规则      
    //中国电信133.153.177.180.181.189              
    private static string dianxin = @"^1(3[3]|5[3]|7[7]|8[019])\d{8}$";
    Regex dianxinReg = new Regex(dianxin);
    //联通手机号正则         
    //中国联通130.131.132.155.156.185.186 .145.176               
    private static string liantong = @"^1(3[0-2]|4[5]|7[6]|5[56]|8[56])\d{8}$";
    Regex liantongReg = new Regex(liantong);
    //移动手机号正则         
    //中国移动 134.135.136.137.138.139.150.151.152.157.158.159.182.183.184.187.188 ,147(数据卡)        
    private static string yidong = @"^1(3[4-9]|4[7]|5[012789]|8[3478])\d{8}$";
    Regex yidongReg = new Regex(yidong);

    public static int CountID { get => countID; set => countID = value; }

    private static zhucheguanli instance;
    public static zhucheguanli Instance
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

    private void Awake()
    {
        Instance = this;
        if (jiemian != null)
            jiemian.SetActive(false);
        shifouchengon = false;
        if (!ischange)
        {
            //清空列表
            academy.ClearOptions();
            //增加选项
            academy.AddOptions(All.Instance.academynames);
        }
    }

    /// <summary>
    /// 根据学院选择系
    /// </summary>
    /// <param name="value"></param>
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
    /// <summary>
    ///检测系
    /// </summary>
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
    void Update()
    {
        if (!ischange)
        {
            thisID = ID.GetComponent<InputField>().text;
            thiskey = key.GetComponent<InputField>().text;
            thisEmail = Email.GetComponent<InputField>().text;
            thischeckkey = checkkey.GetComponent<InputField>().text;
            thisTEL = TEL.GetComponent<InputField>().text;
            thisName = Name.GetComponent<InputField>().text;
        }


        //监听列表的变化
        academy.onValueChanged.AddListener(checkacademyname);
        subject.onValueChanged.AddListener(checksubjectname);
    }
    /// <summary>
    /// 检查修改是否正确
    /// </summary>
    public bool checkchange(GameObject Tips, GameObject jiemian, string thisID, string thiskey, string thisEmail, string thisTEL, string thisName)
    {
        if (thisID != "" && thiskey != "" && thisEmail != "" && thisTEL != "" && thisName != "")
        {

            if (UserTelNumeIsLegal(thisTEL))
            {
                if (isMailFormat(thisEmail))
                {
                    if (checkIDkey(thisID))
                    {
                        if (issubjectcheck)
                        {
                            Tips.GetComponent<Text>().text = chuowuxinxi6.ToString();
                            jiemian.SetActive(true);
                            return true;
                        }
                        else
                        {
                            Tips.GetComponent<Text>().text = chuowuxinxi4.ToString();
                            jiemian.SetActive(true);
                            return false;
                        }
                    }
                    else
                    {
                        Tips.GetComponent<Text>().text = chuowuxinxi7.ToString();
                        jiemian.SetActive(true);
                        return false;
                    }
                }
                else
                {
                    Tips.GetComponent<Text>().text = chuowuxinxi3.ToString();
                    jiemian.SetActive(true);
                    return false;
                }
            }
            else
            {
                Tips.GetComponent<Text>().text = chuowuxinxi2.ToString();
                jiemian.SetActive(true);
                return false;
            }
        }
        else
        {
            Tips.GetComponent<Text>().text = chuowuxinxi0.ToString();
            jiemian.SetActive(true);
            return false;
        }
    }
    public bool checkIDkey(string id)
    {
        Debug.Log(All.user.Count);
        foreach (students stu in All.user)
        {
            Debug.Log(stu.Matrikelnummer);
            if (stu.Matrikelnummer == id)
            {
                return false;
            }
        }
        return true;
    }
    public void buttoncheck()
    {
        if (thisID != "" && thiskey != "" && thisEmail != "" && thischeckkey != "" && thisTEL != "" && thisName != "")
        {
            if (thiskey == thischeckkey)
            {
                if (UserTelNumeIsLegal(thisTEL))
                {
                    if (isMailFormat(thisEmail))
                    {
                        if (checkIDkey(thisID))
                        {
                            if (issubjectcheck)
                            {
                                Tips.GetComponent<Text>().text = chuowuxinxi5.ToString();
                                jiemian.SetActive(true);
                                shifouchengon = true;
                            }
                            else
                            {
                                Tips.GetComponent<Text>().text = chuowuxinxi4.ToString();
                                jiemian.SetActive(true);
                                shifouchengon = false;
                            }
                        }
                        else
                        {
                            Tips.GetComponent<Text>().text = chuowuxinxi7.ToString();
                            jiemian.SetActive(true);
                            shifouchengon = false;
                        }
                    }
                    else
                    {
                        Tips.GetComponent<Text>().text = chuowuxinxi3.ToString();
                        jiemian.SetActive(true);
                        shifouchengon = false;
                    }
                }
                else
                {
                    Tips.GetComponent<Text>().text = chuowuxinxi2.ToString();
                    jiemian.SetActive(true);
                    shifouchengon = false;
                }
            }
            else
            {
                Tips.GetComponent<Text>().text = chuowuxinxi1.ToString();
                jiemian.SetActive(true);
                shifouchengon = false;
            }
        }
        else
        {
            Tips.GetComponent<Text>().text = chuowuxinxi0.ToString();
            jiemian.SetActive(true);
            shifouchengon = false;
        }
    }
    //联系电话是否合法
    public bool UserTelNumeIsLegal(string userTelNum)
    {
        return dianxinReg.IsMatch(userTelNum) || liantongReg.IsMatch(userTelNum) || yidongReg.IsMatch(userTelNum);
    }
    //邮箱是否正确
    public bool isMailFormat(string mailFormat)
    {
        return Regex.IsMatch(mailFormat, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
    }

    public void buttonother()
    {
        if (shifouchengon)
        {
            Debug.Log(CountID + " " + thisID + " " + thisName + " " + thisTEL + " " + thisEmail + " " + thiskey + " " + subjectindex + " " + academyindex);
            //传参储存
            students usera = new students()
            {
                ID = CountID + 1,
                Matrikelnummer = thisID,
                name = thisName,
                TEL = thisTEL,
                email = thisEmail,
                Key = thiskey,
                Subjectindex = subjectindex,
                Academyindex = academyindex,
            };
            All.Instance.chuchunshuju(usera);
            CountID++;
            SceneManager.LoadScene("登录界面");
        }
        else
        {
            jiemian.SetActive(false);
        }
    }
}
