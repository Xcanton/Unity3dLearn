using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using Common;
using static UnityEngine.Networking.UnityWebRequest;


namespace Meta
{
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
                Debug.Log(GameObject.Find("Factory").GetComponent<factory>().getCubePos(transform.Find("Image/idText").GetComponent<Text>().text));
                MessageBox.Show("ÇëÊäÈë·½¿é×ø±ê", GameObject.Find("Factory").GetComponent<factory>().getCubePos(transform.Find("Image/idText").GetComponent<Text>().text), transform.Find("Image/nameText").GetComponent<Text>().text);
                MessageBox.confim_vec = (float a, float b, float c, string str) =>
                {
                    GameObject.Find("Factory").GetComponent<factory>().moveCube(new Vector3(a, b, c), transform.Find("Image/idText").GetComponent<Text>().text, str);
                };
                //MessageBox.confim = () => { Debug.Log(1); };
                leftClick.Invoke();
            }


        }

        // Update is called once per frame
        void Update()
        {
            transform.Find("Image/positionText").GetComponent<Text>().text = GameObject.Find("Factory").GetComponent<factory>().getCubePos(transform.Find("Image/idText").GetComponent<Text>().text).ToString();
            transform.Find("Image/nameText").GetComponent<Text>().text = GameObject.Find("Factory").GetComponent<factory>().getCubeName(transform.Find("Image/idText").GetComponent<Text>().text);
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
}
