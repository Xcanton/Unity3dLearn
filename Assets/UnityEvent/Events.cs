using UnityEngine.Events;
using UnityEngine;


namespace Meta
{
    // [System.Serializable]
    public class Events
    {
        public UnityEvent<string, bool> CubeChange = new UnityEvent<string, bool>();
        public UnityEvent<int> StatusChange = new UnityEvent<int>();
        public UnityEvent<int> CurrentDelete = new UnityEvent<int>();
        public UnityEvent<string> itemClicked = new UnityEvent<string>();
        public UnityEvent<string> itemDelete = new UnityEvent<string>();
        public UnityEvent<int> dropdownChange = new UnityEvent<int>();

        public UnityEvent<string, string, Vector3> factoryGenerate = new UnityEvent<string, string, Vector3>();
        public UnityEvent<string> factoryDestory = new UnityEvent<string>();

        public UnityEvent<Vector3> cubeChangePosition = new UnityEvent<Vector3>();
    }
}