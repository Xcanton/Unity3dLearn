using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;


public class factory : MonoBehaviour
{
    // 声明共享的共有变量
    public GameObject nomalCube;  //  方块的prefabs，需要在start前加载

    public Dictionary<string, GameObject> nameCubeDict;
    public List<string> names;
    public Status_enum status_;
    System.Random id_dunction = new System.Random();

    public int status_index;
    public string lastActive;

    public Color default_cube;
    public Color activate_cube;
    public Events events = new Events();

    public bool able_generate = true;


    // Awake is called before the first frame Start
    private void Awake()
    {
        // 共享变量的赋值
        nomalCube = (GameObject)Resources.Load("nomalCube");  // 加载预制件

        nameCubeDict = new Dictionary<string, GameObject>();
        names = new List<string>();
        status_ = new Status_enum();
        status_index = 0;
        lastActive = null;

        default_cube = Color.white;
        activate_cube = Color.blue;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 从磁盘中恢复上一次正常结束时的状态
        // 防止空状态，通过temp暂存，以防后续index报错
        string temp_dict = PlayerPrefs.GetString("nameCubeDict", "");
        string temp_list = PlayerPrefs.GetString("names", "");
        string temp_str = PlayerPrefs.GetString("lastActive", "");
        Dictionary<string, cubeInfo> keyValuePairs = new Dictionary<string, cubeInfo>();

        if (!temp_dict.Equals("") && !temp_list.Equals(""))
        {
            string[] temp_info = temp_dict.Split('\n');
            foreach (var item in temp_info)
            {
                cubeInfo keyValue = JsonUtility.FromJson<cubeInfo>(item);
                keyValuePairs[keyValue.name] = keyValue;
            }

            string[] split_temp_list = temp_list.Split('\n');
            foreach (var item in split_temp_list)
            {
                names.Add(item.ToString());
                GameObject temp = generate_cube(keyValuePairs[item.ToString()].x, keyValuePairs[item.ToString()].y, keyValuePairs[item.ToString()].z, keyValuePairs[item.ToString()].name, keyValuePairs[item.ToString()].str);
                nameCubeDict[item.ToString()] = temp;
            }
        }

        if (!temp_str.Equals(""))
        {
            lastActive = temp_str;
            colorChange(nameCubeDict[lastActive], "activate");
            nameCubeDict[lastActive].GetComponent<cube>().status = true;
        }

    }

    void RaySelected(string cube_name, bool cube_isactivate)
    {
        if (cube_isactivate)
        {
            if (!string.IsNullOrEmpty(lastActive))
            {
                colorChange(nameCubeDict[lastActive]);
                nameCubeDict[lastActive].GetComponent<cube>().status = false;
            }
            lastActive = cube_name;
            colorChange(nameCubeDict[lastActive], "activate");
            nameCubeDict[lastActive].GetComponent<cube>().status = true;
        }
        else
        {
            colorChange(nameCubeDict[cube_name]);
            nameCubeDict[lastActive].GetComponent<cube>().status = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (lastActive != null)
        {
            // colorChange(nameCubeDict[lastActive], "activate");
        }

        // 单测代码块
        //if (Input.GetKeyDown(KeyCode.RightCommand)) { getMouseRayObject(); }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            status_index++;
            status_index %= System.Enum.GetNames(status_.GetType()).Length;
            if (status_index == (int)Status_enum.move)
            {
                colorChange(nameCubeDict[lastActive]);
                lastActive = null;
            }
            events.dropdownChange.Invoke(status_index);
        }


        if (Input.GetMouseButtonDown(0))
        {
            switch ((Status_enum)int.Parse(status_index.ToString()))
            {
                case Status_enum.generate:

                    if (lastActive != null)
                    {
                        colorChange(nameCubeDict[lastActive]);
                        nameCubeDict[lastActive].GetComponent<cube>().status = false;
                    }

                    // 射线碰撞，返回碰撞到的坐标
                    Vector3 current_left_click = getMouseRayWorldVect();
                    //generate_cube(current_left_click.x, (current_left_click.y > 0.5) ? current_left_click.y : 0.5f, current_left_click.z);
                    if (!current_left_click.Equals(new Vector3(-1, -1, -1)))
                    {
                        long temp_id;
                        temp_id = System.DateTime.Now.Ticks;

                        // +0.5是由于方块大小，会导致穿模
                        GameObject temp = generate_cube(current_left_click.x, current_left_click.y + 0.5f, current_left_click.z, temp_id.ToString());

                        lastActive = temp_id.ToString();
                        names.Add(lastActive);
                        nameCubeDict[lastActive] = temp;


                        colorChange(nameCubeDict[lastActive], "activate");
                    }
                    break;
                case Status_enum.select:
                    break;
                case Status_enum.move:
                    Debug.Log(lastActive);
                    if (lastActive == null)
                    {
                    }
                    else
                    {
                        colorChange(nameCubeDict[lastActive], "activate");
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        if (Physics.Raycast(ray, out hit))
                        {
                            Transform target_object = hit.collider.transform;
                            if (target_object == GameObject.Find("Plane").transform || target_object == GameObject.Find("Cube").transform)
                            {
                                Vector3 temp = hit.point;
                                temp.y += 0.5f;
                                nameCubeDict[lastActive].transform.position = temp;
                            }
                        }
                    }
                    break;
            }


        }

        //  方块删除，检测鼠标右键，当前index下的方块进行一个删除的大动作，并从方块列表中删除该方块
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.L))
        {
            DeleteCubeFunc();
        }

        // 队列左移，选择对应方块；若处于初始的方块则不再移动
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (lastActive != null)
            {
                colorChange(nameCubeDict[lastActive]);
                nameCubeDict[lastActive].GetComponent<cube>().status = false;
            }

            if (names.Count > 0)
            {
                int index = names.IndexOf(lastActive);
                lastActive = names[(index == 0)?index:index-1];
            }

            colorChange(nameCubeDict[lastActive], "activate");
        }

        // 队列右移，选择对应方块；若处于最新的方块则不再移动
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (lastActive != null)
            {
                colorChange(nameCubeDict[lastActive]);
                nameCubeDict[lastActive].GetComponent<cube>().status = false;
            }

            if (names.Count > 0)
            {
                int index = names.IndexOf(lastActive);
                lastActive = names[(index == names.Count-1) ? index : index + 1];
            }

            colorChange(nameCubeDict[lastActive], "activate");
        }

    }


    // 当程序正常退出时进行的必要程序
    public void OnApplicationQuit()
    {
        List<string> save_list = new List<string>();
        foreach (var item in nameCubeDict)
        {
            cubeInfo temp = new cubeInfo();
            temp.x = item.Value.transform.position.x;
            temp.y = item.Value.transform.position.y;
            temp.z = item.Value.transform.position.z;
            temp.name = item.Key;
            temp.str = nameCubeDict[item.Key].transform.name;
            save_list.Add(JsonUtility.ToJson(temp).ToString());
        }
        // 通过playerprefs进行存储
        PlayerPrefs.SetString("nameCubeDict", string.Join("\n", save_list));
        PlayerPrefs.SetString("names", string.Join("\n", names));
        PlayerPrefs.SetString("lastActive", lastActive);
        PlayerPrefs.Save();
    }

    void DeleteCubeFunc(string str = null)
    {
        if (string.IsNullOrEmpty(str))
        {
            str = lastActive;
        }
        if (str != null)
        {
            Debug.Log(str);
            Destroy(nameCubeDict[str]);
            events.factoryDestory.Invoke(str);

            nameCubeDict.Remove(str);
            names.Remove(str);
            if (names.Count > 0)
            {
                lastActive = names[names.Count - 1];
                colorChange(nameCubeDict[lastActive], "activate");
            }
            else
            {
                lastActive = null;
            }
        }
    }

    // 生成方块代码
    GameObject generate_cube(float x, float y, float z, string name)
    {
        // 从预制体实例化一个对象
        GameObject temp = UnityEngine.Object.Instantiate(nomalCube);
        //Debug.Log("实例化"+ Time.frameCount);
        temp.transform.position = new Vector3(x, y, z);  // 设置其初始值
        temp.GetComponent<cube>().id = name;
        temp.GetComponent<cube>().status = true;

        events.factoryGenerate.Invoke(name,temp.name);

        temp.GetComponent<cube>().events.CubeChange.AddListener(RaySelected);
        return temp;
    }

    GameObject generate_cube(float x, float y, float z, string name, string str)
    {
        // 从预制体实例化一个对象
        GameObject temp = UnityEngine.Object.Instantiate(nomalCube);
        //Debug.Log("实例化"+ Time.frameCount);
        temp.transform.position = new Vector3(x, y, z);  // 设置其初始值
        temp.GetComponent<cube>().id = name;
        temp.GetComponent<cube>().status = true;

        events.factoryGenerate.Invoke(name, temp.name);

        temp.GetComponent<cube>().events.CubeChange.AddListener(RaySelected);
        temp.transform.name = str;
        return temp;
    }

    public void outer_generate(float x = 0, float y = 0, float z = 0)
    {
        if (lastActive != null)
        {
            Debug.Log("last activate is not null");
            colorChange(nameCubeDict[lastActive]);
            nameCubeDict[lastActive].GetComponent<cube>().status = false;
        }

        switch ((Status_enum)int.Parse(status_index.ToString()))
        {
            case Status_enum.generate:
                Debug.Log("status is generate");
                long temp_id;
                temp_id = System.DateTime.Now.Ticks;
                Debug.Log("cube id got");

                // +0.5是由于方块大小，会导致穿模
                GameObject temp = generate_cube(x, y, z, temp_id.ToString());
                Debug.Log("cub instantiated");

                lastActive = temp_id.ToString();
                names.Add(lastActive);
                nameCubeDict[lastActive] = temp;
                Debug.Log("cube add");

                colorChange(nameCubeDict[lastActive], "activate");
                break;
            case Status_enum.select:
                Debug.Log("status is select");
                break;
        }
    }

    public void outer_generate(float x = 0, float y = 0, float z = 0, string str = null)
    {
        if (lastActive != null)
        {
            Debug.Log("last activate is not null");
            colorChange(nameCubeDict[lastActive]);
            nameCubeDict[lastActive].GetComponent<cube>().status = false;
        }

        switch ((Status_enum)int.Parse(status_index.ToString()))
        {
            case Status_enum.generate:
                Debug.Log("status is generate");
                long temp_id;
                temp_id = System.DateTime.Now.Ticks;
                Debug.Log("cube id got");

                // +0.5是由于方块大小，会导致穿模
                GameObject temp = generate_cube(x, y, z, temp_id.ToString());
                Debug.Log("cub instantiated");

                lastActive = temp_id.ToString();
                names.Add(lastActive);
                nameCubeDict[lastActive] = temp;
                Debug.Log("cube add");

                colorChange(nameCubeDict[lastActive], "activate");

                Debug.Log(str);
                if (!string.IsNullOrEmpty(str))
                {
                    Debug.Log("改名字");
                    temp.transform.name = str;
                }
                break;
            case Status_enum.select:
                Debug.Log("status is select");
                break;
        }
    }
    public GameObject outer_generate_return(float x = 0, float y = 0, float z = 0)
    {
        if (lastActive != null)
        {
            Debug.Log("last activate is not null");
            colorChange(nameCubeDict[lastActive]);
            nameCubeDict[lastActive].GetComponent<cube>().status = false;
        }

        switch ((Status_enum)int.Parse(status_index.ToString()))
        {
            case Status_enum.generate:
                Debug.Log("status is generate");
                long temp_id;
                temp_id = System.DateTime.Now.Ticks;
                Debug.Log("cube id got");

                // +0.5是由于方块大小，会导致穿模
                GameObject temp = generate_cube(0f, 0.5f, 0f, temp_id.ToString());
                Debug.Log("cub instantiated");

                lastActive = temp_id.ToString();
                names.Add(lastActive);
                nameCubeDict[lastActive] = temp;
                Debug.Log("cube add");

                colorChange(nameCubeDict[lastActive], "activate");
                return temp;
                break;
            case Status_enum.select:
                Debug.Log("status is select");
                
                break;
        }
        return null;
    }

    // 射线检测，获取鼠标点击的当前平面坐标
    Vector3 getMouseRayWorldVect()
    {
        
        // 指定主相机向当前鼠标位置发射射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 声明hit变量用以存储碰撞信息
        RaycastHit hit;
        Debug.Log(able_generate);
        Debug.Log(Physics.Raycast(ray, out hit));
        if (Physics.Raycast(ray, out hit) && able_generate)
        {
            return hit.point;
        }
        else // 返回值兼容，由于返回值不兼容null
        {
            return new Vector3(-1, -1, -1);
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

    // 修改材料材质的颜色
    public void colorChange(GameObject current, string
        tobetype = "default")
    {
        // 恢复默认颜色
        if (tobetype == "default")
        {
            current.GetComponent<MeshRenderer>().material.SetColor("_Color", default_cube);
        }
        // 调成指定的激活颜色
        else
        {
            current.GetComponent<MeshRenderer>().material.SetColor("_Color", activate_cube);
        }
    }

    public void dropdownchange(int indexd)
    {
        status_index = indexd;
    }

    public void ViewPointItemClicked(string str)
    {
        colorChange(nameCubeDict[lastActive]);
        colorChange(nameCubeDict[str], "activate");
        lastActive = str;
    }

    public void itemDelete(string str)
    {
        DeleteCubeFunc(str);
    }

    public void moveCube(Vector3 vect, string name)
    {
        nameCubeDict[name].transform.position = vect;
    }

    public void moveCube(Vector3 vect, string name, string str)
    {
        nameCubeDict[name].transform.position = vect;
        nameCubeDict[name].name = str;
    }

    public Vector3 getCubePos(string name)
    {
        return nameCubeDict[name].transform.position;
    }

    public string getCubeName(string name)
    {
        return nameCubeDict[name].transform.name;
    }

    public int getFactoryStatus()
    {
        return status_index;
    }

    public void setFactoryStatus(Status_enum inpt)
    {
        switch (inpt)
        {
            case Status_enum.generate:
                status_index = 0;
                break;
            case Status_enum.select:
                status_index = 1;
                break;
            case Status_enum.move:
                status_index = 2;
                break;
        }
    }

}

// 自定义可叠戴结构体，用作生成json
[System.Serializable]
public class cubeInfo {
    public float x;
    public float y;
    public float z;
    public string name;
    public string str;
}
