using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Common;

public class MessBox : MonoBehaviour
{
    public Text title;
    public Button Sure;
    public Button Cancle;

    public int a;
    public int b;
    public int c;

    // Start is called before the first frame update
    void Start()
    {
        Sure = GameObject.Find("ButtonGroup/Conform").GetComponent<Button>();
        Cancle = GameObject.Find("ButtonGroup/Cancle").GetComponent<Button>();
        title = GameObject.Find("Title").GetComponent<Text>();
        Sure.onClick.AddListener(MessageBox.Sure);
        Cancle.onClick.AddListener(MessageBox.Cancle);
    }

    // Update is called once per frame
    void Update()
    {

    }
}