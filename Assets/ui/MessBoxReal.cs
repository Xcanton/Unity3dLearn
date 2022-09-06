using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Common
{
    public delegate void Confim();
    public delegate void Confim_vec(int a, int b, int c);
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
            Time.timeScale = 1;
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
            Time.timeScale = 1;
            Messagebox.SetActive(true);
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;
        }
        public static void  Sure()
        {
            if (confim_vec != null)
            {
                string a = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_x/InputField_x").GetComponent<InputField>().text;
                string b = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_y/InputField_y").GetComponent<InputField>().text;
                string c = Messagebox.transform.Find("MessageBox/InputboxGroup/Text_z/InputField_z").GetComponent<InputField>().text;

                confim_vec(int.Parse(a), int.Parse(b), int.Parse(c));
                Debug.Log("自定义方法结束");
                GameObject.Destroy(Messagebox);
                Debug.Log("Destroy");
                TitleStr = "标题";
                Time.timeScale = 0;

                GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
            }
        }
        public static void Cancle()
        {
            Result = 2;
            GameObject.Destroy(Messagebox);
            TitleStr = "标题";
            Time.timeScale = 0;
            GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
        }
    }
}