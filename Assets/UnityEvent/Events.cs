using UnityEngine.Events;
using UnityEngine;

// [System.Serializable]
public class Events
{
    public UnityEvent<string, bool> CubeChange = new UnityEvent<string, bool>();
    public UnityEvent<int> StatusChange = new UnityEvent<int>();
    public UnityEvent<int> CurrentDelete = new UnityEvent<int>();
    public UnityEvent<string> itemClicked = new UnityEvent<string> ();
    public UnityEvent<string> itemDelete = new UnityEvent<string>();
}