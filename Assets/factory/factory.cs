using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class factory : MonoBehaviour
{
    // 声明共享的共有变量
    public GameObject nomalCube;  //  方块的prefabs，需要在start前加载
    public List<GameObject> Acube_List;  //  方块列表，用来控制与显示指定方块
    public int index;
    public float speed; // 移动速度
    public Color default_cube;
    public Color activate_cube;
    public List<string> left_click_status;
    public int status_index;

    // Awake is called before the first frame Start
    private void Awake()
    {
        // 共享变量的定义
        Acube_List = new List<GameObject>();
        nomalCube = (GameObject)Resources.Load("nomalCube");  // 加载预制件
        index = -1;
        speed = 3;
        default_cube = Color.white;
        activate_cube = Color.blue;
        left_click_status = new List<string>() { "generate", "select" };
        status_index = 0 ;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 从磁盘中恢复上一次正常结束时的状态
        // 防止空状态，通过temp暂存，以防后续index报错
        int temp = PlayerPrefs.GetInt("index", -1);

        // 当上一次正常结束时，若有存储的状态，则开始恢复
        if (temp >= 0)
        {
            string[] temp_cube_list = PlayerPrefs.GetString("Acube_List").Split('\n');
            for (int i = 0; i < temp_cube_list.Length; i++)
            {
                string temp_cube = temp_cube_list[i];
                // 通过自定义结构体进行json的反序列化
                cubeInfo temp_cube_json = JsonUtility.FromJson<cubeInfo>(temp_cube);
                generate_cube(temp_cube_json.x, temp_cube_json.y, temp_cube_json.z);
            }
            index = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 对当前index下的Cube激活起颜色
        colorChange("activate");

        // 单测代码块
        //if (Input.GetKeyDown(KeyCode.RightCommand)) { getMouseRayObject(); }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            status_index++;
            status_index %= left_click_status.Count;
        }

        // 方块生成： 检测鼠标左键（代码：0），从预制体中生成新方块并加入队列中，修改当前激活的方块index
        if (Input.GetMouseButtonDown(0))
        {
            switch (left_click_status[status_index])
            {
                case "generate":
                    // 射线碰撞，返回碰撞到的坐标
                    Vector3 current_left_click = getMouseRayWorldVect();
                    //generate_cube(current_left_click.x, (current_left_click.y > 0.5) ? current_left_click.y : 0.5f, current_left_click.z);
                    if (!current_left_click.Equals(new Vector3(-1, -1, -1)))
                    {
                        // +0.5是由于方块大小，会导致穿模
                        generate_cube(current_left_click.x, current_left_click.y + 0.5f, current_left_click.z);
                    }
                    break;
                case "select":
                    getMouseRayObject();
                    break;
            }
            

        }

        //  方块删除，检测鼠标右键，当前index下的方块进行一个删除的大动作，并从方块列表中删除该方块
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L))
        {
            if (index >= 0)
            {
                Destroy(Acube_List[index]);  // 销毁对象
                Acube_List.Remove(Acube_List[index]);  // 移除列表指针
                index = Acube_List.Count - 1;
            }
        }

        ////按键生成
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    generate_cube(1f * (Acube_List.Count - 2), 0f, 5f);

        //}

        ////按键删除
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    //Acube_List[index].DestroyImmediate();
        //    if (index >= 0)
        //    {
        //        Destroy(Acube_List[index]);
        //        Acube_List.Remove(Acube_List[index]);
        //        index = Acube_List.Count - 1;
        //    }
        //}

        // 当前状态有实例化的方块时，对其运动进行计算
        if (index >= 0) {
            movementControl(Acube_List[index]);
        }

        // 队列左移，选择对应方块；若处于初始的方块则不再移动
        if (Input.GetKeyDown(KeyCode.O))
        {
            // 上一方块恢复默认颜色
            colorChange();
            index = (Acube_List.Count == 0) ? -1 : (index>0)?index-1:0;
        }

        // 队列右移，选择对应方块；若处于最新的方块则不再移动
        if (Input.GetKeyDown(KeyCode.P))
        {
            // 上一方块恢复默认颜色
            colorChange();
            index = (index == Acube_List.Count - 1) ? Acube_List.Count - 1 : index + 1;
        }

    }

    // 修改材料材质的颜色
    void colorChange(string tobetype = "default")
    {
        if (index >= 0)
        {
            // 恢复默认颜色
            if (tobetype == "default") {
                Acube_List[index].GetComponent<MeshRenderer>().material.SetColor("_Color",default_cube);
            }
            // 调成指定的激活颜色
            else
            {
                Acube_List[index].GetComponent<MeshRenderer>().material.SetColor("_Color", activate_cube);
            }
        }
    }

    // 方块运动控制
    void movementControl(GameObject current)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            current.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            current.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            current.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            current.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            current.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            current.transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }


    // 当程序正常退出时进行的必要程序
    public void OnApplicationQuit()
    {
        // 对当前的状态进行记录
        List<string> cube_data = new List<string>();
        for (int i = 0; i < Acube_List.Count; i++)
        {
            // Hashtable会导致json后多一个逗号，导致解析出现问题；由于事先未指定长度
            //Hashtable temp = new Hashtable();
            //GameObject current_cube = Acube_List[i];
            //temp["x"] = current_cube.transform.position.x;
            //temp["y"] = current_cube.transform.position.y;
            //temp["z"] = current_cube.transform.position.z;
            //temp["colortype"] = (i == index)? "activate" : "default";
            //var json_temp = HashtableToJson(temp);

            // 通过自定义结构体构造json
            cubeInfo temp = new cubeInfo();
            GameObject current_cube = Acube_List[i];
            temp.x = current_cube.transform.position.x;
            temp.y = current_cube.transform.position.y;
            temp.z = current_cube.transform.position.z;
            temp.colortype = (i == index)? "activate" : "default";
            string json_temp = JsonUtility.ToJson(temp);

            cube_data.Add(json_temp.ToString());
        }

        // 通过playerprefs进行存储
        PlayerPrefs.SetString("Acube_List", string.Join("\n", cube_data));
        PlayerPrefs.SetInt("index", index);
        PlayerPrefs.Save();
    }

    // Hashtable转json字符串
    public static string HashtableToJson(Hashtable hr, int readcount = 0)

    {
        string json = "{";
        foreach (DictionaryEntry row in hr)
        {
            try
            {
                string key = "\"" + row.Key + "\":";
                if (row.Value is Hashtable)
                {
                    Hashtable t = (Hashtable)row.Value;
                    if (t.Count > 0)
                    {
                        json += key + HashtableToJson(t, readcount++) + ",";
                    }
                    else { json += key + "{},"; }
                }
                else
                {
                    string value = "\"" + row.Value.ToString() + "\",";
                    json += key + value;
                }
            }
            catch { }
        }



        //  json = MyString.ClearEndChar(json);   


        json = json + "}";
        return json;
    }

    // 生成方块代码
    void generate_cube(float x, float y, float z)
    {
        colorChange();
        // 从预制体实例化一个对象
        GameObject temp = UnityEngine.Object.Instantiate(nomalCube);
        temp.transform.position = new Vector3(x, y, z);  // 设置其初始值
        Acube_List.Add(temp);
        index = Acube_List.Count - 1;
    }

    // 射线检测，获取鼠标点击的当前平面坐标
    Vector3 getMouseRayWorldVect()
    {
        // 指定主相机向当前鼠标位置发射射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 声明hit变量用以存储碰撞信息
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        else // 返回值兼容，由于返回值不兼容null
        {
            return new Vector3(-1, -1, -1);
        }
        
    }

    void getMouseRayObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform target_object = hit.collider.transform;
            for(int i =0; i < Acube_List.Count; i++)
            {
                if (Acube_List[i].transform.Equals(target_object))
                {
                    colorChange();
                    index = i;
                    colorChange("activate");
                }
            }
        }
    }

    // 获取鼠标世界坐标
    Vector3 getMouseWorldVect()
    {
        Vector3 mouse_p = Input.mousePosition;
        Vector3 screen_p = Camera.main.WorldToScreenPoint(transform.position);
        mouse_p.z = screen_p.z;
        Vector3 mouse_world_p = Camera.main.ScreenToWorldPoint(mouse_p);
        return mouse_world_p;
    }

    
}

// 自定义可叠戴结构体，用作生成json
[System.Serializable]
public class cubeInfo {
    public float x;
    public float y;
    public float z;
    public string colortype;
}
