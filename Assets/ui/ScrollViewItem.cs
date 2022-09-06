using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Common;
using static UnityEngine.Networking.UnityWebRequest;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    public Events events = new Events();
    public UnityEvent leftClick;

    // Start is called before the first frame update
    void Start()
    {
        leftClick.AddListener(new UnityAction(ButtonLeftClick));


        Button btn = transform.Find("Image/Button").GetComponent<Button>();
        btn.GetComponentInChildren<Text>().text = "Delete";
        //btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick);

        //GameObject.Find("Factory").GetComponent<factory>().events.itemDelete.AddListener(GameObject.Find("Factory").GetComponent<factory>().itemDelete);
        //GameObject.Find("Factory").GetComponent<factory>().events.itemClicked.AddListener(GameObject.Find("Factory").GetComponent<factory>().ViewPointItemClicked);

        GameObject.Find("Factory").GetComponent<factory>().events.factoryDestory.AddListener(deleteSelfItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int a;
            int b;
            int c;
            MessageBox.Show("请输入方块坐标");
            MessageBox.confim_vec = (int a, int b, int c) =>
            {
                GameObject.Find("Factory").GetComponent<factory>().moveCube(new Vector3(a, b, c), transform.Find("Image/idText").GetComponent<Text>().text);
                Debug.Log("移动完成");
            };
            //MessageBox.confim = () => { Debug.Log(1); };
            leftClick.Invoke();
        }


    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ButtonLeftClick()
    {
        GameObject.Find("Factory").GetComponent<factory>().ViewPointItemClicked(transform.Find("Image/idText").GetComponent<Text>().text);
    }

    private void deleteSelfItem(string str)
    {
        if (transform.Find("Image/idText").GetComponent<Text>().text.Equals(str))
        {
            Destroy(gameObject);
        }
    }

    void OnClick()
    {
        GameObject.Find("Factory").GetComponent<factory>().itemDelete(transform.Find("Image/idText").GetComponent<Text>().text);
        //GameObject.Find("Factory").GetComponent<factory>().events.itemDelete.Invoke(transform.Find("Image/idText").GetComponent<Text>().text);
        Debug.Log("destroy cube");
        Destroy(gameObject);
        Debug.Log("destory item");
    }

}
