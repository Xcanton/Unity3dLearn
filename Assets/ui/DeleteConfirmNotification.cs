using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DeleteConfirm
{
    public delegate void Confim();
    public class MessageBox
    {
        static GameObject Messagebox;
        static int Result = -1;
        public static Confim confim;
        public static string TitleStr;
        public static void Show()
        {
            Messagebox = (GameObject)Resources.Load("Prefab/delete_notification");
            Messagebox = GameObject.Instantiate(Messagebox, GameObject.Find("Canvas").transform) as GameObject;
            Messagebox.transform.localScale = new Vector3(1, 1, 1);
            Messagebox.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            Messagebox.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            Messagebox.GetComponent<RectTransform>().offsetMax = Vector2.zero;
            Messagebox.SetActive(true);
            Messagebox.transform.Find("background/Text").GetComponent<Text>().text = "ȷ��ɾ���÷���ô";
            GameObject.Find("Factory").GetComponent<factory>().able_generate = false;
        }

        public static void Sure()
        {
            if (confim != null)
            {

                confim();
                Debug.Log("�Զ��巽������");
                GameObject.Destroy(Messagebox);
                Debug.Log("Destroy");
                TitleStr = "����";

                GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
            }
        }
        public static void Cancle()
        {
            Result = 2;
            GameObject.Destroy(Messagebox);
            TitleStr = "����";
            GameObject.Find("Factory").GetComponent<factory>().able_generate = true;
        }
    }
}