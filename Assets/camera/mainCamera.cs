using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta
{
    public class mainCamera : MonoBehaviour
    {

        public float x_move;
        public float y_move;
        public float distance;
        public int effect_pi_range;
        //public GameObject rotate_target;

        // Start is called before the first frame update
        void Start()
        {
            x_move = 0f;
            y_move = 0f;
            distance = 0f;
            effect_pi_range = 50;

            // 镜头向下倾斜45度
            Vector3 temp = GameObject.Find("Factory").transform.position;
            transform.RotateAround(temp, Vector3.right, 45);

            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().sizeDelta);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().rect);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().offsetMax);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().offsetMin);
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.Q))
            {
                // 摄像机左旋
                cameraRotate(1);
            }

            if (Input.GetKey(KeyCode.E))
            {
                // 摄像机右旋
                cameraRotate(-1);
            }

            // 检测摄像机移动
            cameraMoveWithMouse();
        }

        void cameraMoveWithMouse()
        {
            //Vector3 current_diff = getMouseWorldVect() - transform.position;
            ////float distance = current_diff.magnitude;
            //if (-5 <= x_move && x_move <= 5)
            //{

            //    distance = (current_diff[0] < -5 || current_diff[0] > 5) ? current_diff[0] : 0f;
            //}
            //if (-5 > x_move || x_move > 5)
            //{
            //    float temp = current_diff[0];
            //    distance = (temp * x_move < 0) ? current_diff[0] : 0;
            //}
            //if (distance != 0)
            //{
            //    distance = (distance > 0) ? 1f : -1f;
            //    Camera.main.transform.Translate(Vector3.right * 0.1f * distance);
            //}
            //x_move += distance * 0.1f;
            //return;

            Vector2 current_diff = Input.mousePosition;
            int xleft = (int)current_diff[0];
            float xright = Screen.width - current_diff[0];
            int yleft = (int)current_diff[1];
            float yright = Screen.height - current_diff[1];

            // 镜头左右移动

            float menu_wide = Screen.width;
            distance = (-5 <= x_move && x_move <= 5) ? ((menu_wide < xleft && xleft < menu_wide + effect_pi_range) || xright < effect_pi_range) ? current_diff[0] : 0f : (x_move < 0) ? (xright < effect_pi_range) ? current_diff[0] : 0 : (xleft < effect_pi_range) ? current_diff[0] : 0;
            //xleft < effect_pi_range
            if (distance != 0)
            {
                // 由于摄像机并未左右倾斜，所以左右移动的坐标方向与相机方向一致
                distance = (distance > Screen.width / 2) ? 1 : -1;
                //Camera.main.transform.Translate(Vector3.right * distance);
                transform.Translate(Vector3.right * distance * 0.1f);
            }
            x_move += distance * 0.1f;

            // 获取左侧菜单栏宽度
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().sizeDelta);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().rect);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().offsetMax);
            //Debug.Log(GameObject.Find("Canvas/Menu_Image").GetComponent<RectTransform>().offsetMin);
            // 镜头前后移动
            distance = (-5 <= y_move && y_move <= 5) ? (yleft < effect_pi_range || yright < effect_pi_range) ? current_diff[1] : 0f : (y_move < 0) ? (yright < effect_pi_range) ? current_diff[1] : 0 : (yleft < effect_pi_range) ? current_diff[1] : 0;

            if (distance != 0)
            {
                // 由于摄像机向下俯视45度，所以前后移动摄像机时需要进行坐标变换
                distance = (distance > Screen.height / 2) ? 1 : -1;
                //Camera.main.transform.Translate(Vector3.right * distance);
                Vector3 temp = transform.forward;
                //将坐标y轴设置成0
                temp.y = 0;

                // 坐标标准化后与移动距离相加
                Vector3 origin_direct = temp.normalized * distance * 0.1f;

                // 全局坐标变换
                //Camera.main.transform.Translate(origin_direct); // 不行，相机沿当前指向方向前进
                Vector3 cameraPos = transform.position;
                cameraPos += origin_direct;

                // 改变相机global坐标
                transform.position = cameraPos;
            }
            y_move += distance * 0.1f;
        }

        void cameraRotate(int x)
        {
            transform.RotateAround(transform.position, Vector3.up, x);
        }


    }

}
