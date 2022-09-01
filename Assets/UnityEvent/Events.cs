using UnityEngine.Events;
using UnityEngine;

// [System.Serializable]
public class Events
{
    public UnityEvent<string, bool> CubeChange = new UnityEvent<string, bool>();
}