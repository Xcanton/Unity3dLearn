using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollViewItem : MonoBehaviour, IPointerClickHandler
{
    public Events events = new Events();
    public UnityEvent leftClick;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = GameObject.Find("Factory").GetComponent<factory>().outer_generate_return();
        transform.Find("Image/idText").GetComponent<Text>().text = temp.GetComponent<cube>().id;
        transform.Find("Image/nameText").GetComponent<Text>().text = temp.GetComponent<cube>().name;
        leftClick.AddListener(new UnityAction(ButtonLeftClick));


        Button btn = transform.Find("Image/Button").GetComponent<Button>();
        btn.GetComponentInChildren<Text>().text = "Delete";
        //btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(OnClick);
        events.itemDelete.AddListener(GameObject.Find("Factory").GetComponent<factory>().itemDelete);
        events.itemClicked.AddListener(GameObject.Find("Factory").GetComponent<factory>().ViewPointItemClicked);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            leftClick?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ButtonLeftClick()
    {
        events.itemClicked.Invoke(transform.Find("Image/idText").GetComponent<Text>().text);
        Debug.Log("called");
    }

    void OnClick()
    {
        Debug.Log("start to destroy");
        Debug.Log(transform.parent.parent.gameObject);
        Destroy(transform.parent.parent.gameObject);
        Debug.Log("successfully destroy");
        events.itemDelete.Invoke(transform.Find("Image/idText").GetComponent<Text>().text);
    }

}
