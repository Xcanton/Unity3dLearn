using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEditor.UIElements;
using UnityEngine;

public class cube : MonoBehaviour
{
    // 定义事件
    public Events events = new Events();
    public string id;
    public bool status;

    public float speed; // 移动速度
    public UnityEngine.UI.Text nameLabel;
    public GameObject nvas;

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

        Vector3 namepos = Camera.main.WorldToScreenPoint(this.transform.position);
        namepos.y += 1;
        //nvas.transform.position = namepos;
        nvas.transform.forward = Camera.main.transform.forward;

        nameLabel.GetComponent<UnityEngine.UI.Text>().text = transform.name.ToString();
    }

    void getMouseRayObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && (GameObject.Find("Factory").GetComponent<factory>().status_index != (int)Status_enum.move || string.IsNullOrEmpty(GameObject.Find("Factory").GetComponent<factory>().lastActive)))
        {
            Debug.Log("击中");
            Transform target_object = hit.collider.transform;

            Debug.Log(target_object);
            Debug.Log(transform);
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

    //IEnumerator OnMouseDown()
    //{
    //    if (status)
    //    {
    //        // 将物体由世界坐标系转化为屏幕坐标系，由Vector3结构体变量Screen Space存储，以用来明确屏幕坐标系Z轴的位置
    //        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
    //        // 完成了两个步骤，1由于鼠标的坐标系是2维的，需要转化成3维的世界坐标系；2只有三位的情况下才能来计算鼠标位置与物体的距离
    //        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

    //        while (Input.GetMouseButton(0)) 
    //        {
    //            // 得到现在鼠标的2维坐标系位置
    //            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
    //            // 将当前鼠标的2维位置转化成三位的位置，再加上鼠标的移动量
    //            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    //            // CurPosition就是物体应该的移动向量赋给transform的position属性
    //            transform.position = curScreenSpace;
    //            yield return new WaitForFixedUpdate();
    //        }
    //    }
    //}
}
