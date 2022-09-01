using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEngine;

public class cube : MonoBehaviour
{
    // 定义事件
    public Events events = new Events();
    public string id;
    public bool status;

    public float speed; // 移动速度

    private void Awake()
    {
        //Debug.Log("cube的awake" + Time.frameCount);
        id = "";
        status = false;
        speed = 3;

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

        if (status)
        {
            movementControl();
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
                events.CubeChange.Invoke(id, true);
            }
        }
    }

    // 方块运动控制
    void movementControl()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

}
