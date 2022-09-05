using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        Debug.Log("Button Clicked");
        GameObject.Find("Factory").GetComponent<factory>().outer_generate();
        Debug.Log("Function Finished");
    }

}
