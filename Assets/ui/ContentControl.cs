using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Meta
{
    public class ContentControl : MonoBehaviour
    {
        GameObject item;

        // Start is called before the first frame update
        void Start()
        {
            item = (GameObject)Resources.Load("ScrollViewItem");
            GameObject.Find("Factory").GetComponent<factory>().events.factoryGenerate.AddListener(generate);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void generate(string id, string name)
        {
            GameObject temp = UnityEngine.Object.Instantiate(item, transform);
            temp.transform.Find("Image/idText").transform.GetComponent<Text>().text = id;
            temp.transform.Find("Image/nameText").transform.GetComponent<Text>().text = name;

        }
    }
}
