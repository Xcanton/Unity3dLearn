using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Meta
{

    public class Create : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(OnClick);
            btn.GetComponentInChildren<Text>().text = "Create";
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnClick()
        {
            MessageBox.Show("ÇëÊäÈë·½¿é×ø±ê");
            MessageBox.confim_vec = (float a, float b, float c, string str) =>
            {
                //GameObject.Find("Factory").GetComponent<factory>().moveCube(new Vector3(a, b, c), transform.Find("Image/idText").GetComponent<Text>().text);
                GameObject.Find("Factory").GetComponent<factory>().outer_generate(a, b, c, str);
            };

        }

    }

}