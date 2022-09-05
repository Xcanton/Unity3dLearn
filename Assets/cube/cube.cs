using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEngine;

public class cube : MonoBehaviour
{
    // �����¼�
    public Events events = new Events();
    public string id;
    public bool status;

    public float speed; // �ƶ��ٶ�

    private void Awake()
    {
        //Debug.Log("cube��awake" + Time.frameCount);
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

    // �����˶�����
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
    //        // ����������������ϵת��Ϊ��Ļ����ϵ����Vector3�ṹ�����Screen Space�洢����������ȷ��Ļ����ϵZ���λ��
    //        Vector3 ScreenSpace = Camera.main.WorldToScreenPoint(transform.position);
    //        // ������������裬1������������ϵ��2ά�ģ���Ҫת����3ά����������ϵ��2ֻ����λ������²������������λ��������ľ���
    //        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z));

    //        while (Input.GetMouseButton(0)) 
    //        {
    //            // �õ���������2ά����ϵλ��
    //            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenSpace.z);
    //            // ����ǰ����2άλ��ת������λ��λ�ã��ټ��������ƶ���
    //            Vector3 CurPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
    //            // CurPosition��������Ӧ�õ��ƶ���������transform��position����
    //            transform.position = curScreenSpace;
    //            yield return new WaitForFixedUpdate();
    //        }
    //    }
    //}
}
