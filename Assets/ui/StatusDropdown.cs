using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace Meta
{
    public class StatusDropdown : MonoBehaviour
    {
        public Events events = new Events();

        // Start is called before the first frame update
        void Start()
        {
            // ��ȡ��ǰ�����б�
            Dropdown dropdown = GetComponent<Dropdown>();

            // ��ʼ�������б�
            List<Dropdown.OptionData> options = dropdown.options;
            options.Clear();
            foreach (Status_enum step in Enum.GetValues(typeof(Status_enum)))
            {
                options.Add(new Dropdown.OptionData(step.ToString()));
            }
            dropdown.options = options;

            // ��¼��һ״̬�������ж�ѡ��
            //string former_status = options[dropdown.value].ToString();

            // �������вμ���
            dropdown.onValueChanged.AddListener((int index) => dropdownItemChanged(index));

            // 
            events.StatusChange.AddListener(GameObject.Find("Factory").GetComponent<factory>().dropdownchange);

            //
            GameObject.Find("Factory").GetComponent<factory>().events.dropdownChange.AddListener((int value) => dropdown.value = value);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void dropdownItemChanged(int index)
        {
            events.StatusChange.Invoke(index);
        }
    }
}