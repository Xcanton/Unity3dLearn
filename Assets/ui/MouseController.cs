using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Meta
{
    public class MouseController : MonoBehaviour
    {
        public int current;
        public Status_enum factory_status;
        List<UnityEngine.UI.Toggle> Toggle_List = new List<UnityEngine.UI.Toggle>();

        private void Awake()
        {
            current = 0;
            factory_status = new Status_enum();

            Toggle_List.Add(transform.Find("generateToggle").GetComponent<UnityEngine.UI.Toggle>());
            Toggle_List.Add(transform.Find("selectToggle").GetComponent<UnityEngine.UI.Toggle>());
            Toggle_List.Add(transform.Find("moveToggle").GetComponent<UnityEngine.UI.Toggle>());
        }

        // Start is called before the first frame update
        void Start()
        {
            transform.Find("generateToggle").GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((bool value) => { if (value) { current = (int)Status_enum.generate; GameObject.Find("Factory").GetComponent<factory>().setFactoryStatus((Status_enum)int.Parse(current.ToString())); } });
            transform.Find("selectToggle").GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((bool value) => { if (value) { current = (int)Status_enum.select; GameObject.Find("Factory").GetComponent<factory>().setFactoryStatus((Status_enum)int.Parse(current.ToString())); } });
            transform.Find("moveToggle").GetComponent<UnityEngine.UI.Toggle>().onValueChanged.AddListener((bool value) => { if (value) { current = (int)Status_enum.move; GameObject.Find("Factory").GetComponent<factory>().setFactoryStatus((Status_enum)int.Parse(current.ToString())); GameObject.Find("Factory").GetComponent<factory>().colorChange(GameObject.Find("Factory").GetComponent<factory>().nameCubeDict[GameObject.Find("Factory").GetComponent<factory>().lastActive]); GameObject.Find("Factory").GetComponent<factory>().lastActive = null; } });
        }

        // Update is called once per frame
        void Update()
        {
            current = GameObject.Find("Factory").GetComponent<factory>().getFactoryStatus();
            Toggle_List[current].GetComponent<UnityEngine.UI.Toggle>().isOn = true;
        }
    }
}