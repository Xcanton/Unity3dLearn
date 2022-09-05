using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Delete : MonoBehaviour
{
    public Events events = new Events();

    // Start is called before the first frame update
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        btn.GetComponentInChildren <Text>().text = "Delete";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnClick()
    {
    }
}
