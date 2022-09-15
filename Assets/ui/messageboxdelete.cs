using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeleteConfirm;
using UnityEngine.UI;


namespace Meta
{
    public class messageboxdelete : MonoBehaviour
    {

        public Button Sure;
        public Button Cancle;

        // Start is called before the first frame update
        void Start()
        {
            Sure = GameObject.Find("ButtonGroup/Conform").GetComponent<Button>();
            Cancle = GameObject.Find("ButtonGroup/Cancle").GetComponent<Button>();
            Sure.onClick.AddListener(MessageBox.Sure);
            Cancle.onClick.AddListener(MessageBox.Cancle);
            Sure.transform.Find("Text").GetComponent<Text>().text = "确认";
            Cancle.transform.Find("Text").GetComponent<Text>().text = "取消";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
