using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class broadcast : MonoBehaviour
{
    // 定义事件
    public Events events;
    public string name;

    private void Awake()
    {
        name = "";
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            getMouseRayObject();
        }
    }

    void getMouseRayObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform target_object = hit.collider.transform;
            
            if (target_object == transform)
            {
                events.CubeChange.Invoke(name, true);
            }
        }
    }
}
