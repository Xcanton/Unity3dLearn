using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class StatusDropdown : MonoBehaviour
{
    public Events events = new Events();

    // Start is called before the first frame update
    void Start()
    {
        // 获取当前下拉列表
        Dropdown dropdown = GetComponent<Dropdown>();

        // 初始化下拉列表
        List<Dropdown.OptionData> options = dropdown.options;
        options.Clear();
        foreach( Status_enum step in Enum.GetValues(typeof(Status_enum)))
        {
            options.Add(new Dropdown.OptionData(step.ToString()));
        }
        dropdown.options = options;

        // 记录上一状态，用作判断选择
        //string former_status = options[dropdown.value].ToString();

        // 下拉框有参监听
        dropdown.onValueChanged.AddListener((int index) => dropdownItemChanged(index));
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
