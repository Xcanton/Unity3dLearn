using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Meta;

namespace Common
{
    public delegate void Confim();
    public delegate void Confim_vec(float a, float b, float c, string str);
    public class MessageBox
    {
        static GameObject Messagebox;
        static int Result = -1;
        public static Confim confim;
        public static Confim_vec confim_vec;
        public static string TitleStr;
        public static void Show(string content)
        {
            Messagebox = (GameObject)Resources.Load("Prefab/Background");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
            Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            Messagebox.SetActive(true);
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;
        }
        public static void Show(string title, string content)
        {
            TitleStr = title;
            Messagebox = (GameObject)Resources.Load("Prefab/Background");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
            Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            Messagebox.SetActive(true);
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;
        }
        public static void Show(string title, Vector3 pos)
        {
            TitleStr = title;

            Messagebox = (GameObject)Resources.Load("Prefab/Background");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
            Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            Messagebox.SetActive(true);

            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x/InputField_x").GetComponent<InputField>().text = pos[0].ToString();
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y/InputField_y").GetComponent<InputField>().text = pos[1].ToString();
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z/InputField_z").GetComponent<InputField>().text = pos[2].ToString();
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;

            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x").GetComponent<Text>().text = "请输入x坐标";
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y").GetComponent<Text>().text = "请输入y坐标";
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z").GetComponent<Text>().text = "请输入z坐标";
            Messagebox.transform.Find("MessageBox/Content/Text").GetComponent<Text>().text = TitleStr;
        }
        public static void Show(string title, Vector3 pos, string name)
        {
            TitleStr = title;

            Messagebox = (GameObject)Resources.Load("Prefab/Background");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
            Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            Messagebox.SetActive(true);

            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x/InputField_x").GetComponent<InputField>().text = pos[0].ToString();
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y/InputField_y").GetComponent<InputField>().text = pos[1].ToString();
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z/InputField_z").GetComponent<InputField>().text = pos[2].ToString();
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_name/InputField_name").GetComponent<InputField>().text = name;
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;

            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x").GetComponent<Text>().text = "请输入x坐标";
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y").GetComponent<Text>().text = "请输入y坐标";
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z").GetComponent<Text>().text = "请输入z坐标";
            Messagebox.transform.Find("MessageBox/Content/Text").GetComponent<Text>().text = TitleStr;
            Messagebox.transform.Find("MessageBox/InputboxGroup/Text_name").GetComponent<Text>().text = "请输入z坐标";
        }
        public static void  Sure()
        {
            if (confim_vec != null)
            {
                string a = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x/InputField_x").GetComponent<InputField>().text;
                string b = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y/InputField_y").GetComponent<InputField>().text;
                string c = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z/InputField_z").GetComponent<InputField>().text;
                string d = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_name/InputField_name").GetComponent<InputField>().text;

                confim_vec(float.Parse(a), float.Parse(b), float.Parse(c), d);
                Debug.Log("自定义方法结束");
                GameObject.Destroy(Messagebox);
                Debug.Log("Destroy");
                TitleStr = "标题";

                GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
            }
        }
        public static void Cancle()
        {
            Result = 2;
            GameObject.Destroy(Messagebox);
            TitleStr = "标题";
            GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
        }
    }
}