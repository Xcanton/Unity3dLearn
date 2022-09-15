using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

namespace QFramework.CubeUI
{
    public class UIApp : MonoBehaviour
    {
        public cubeList mModel;
        // Start is called before the first frame update
        void Start()
        {

            ResKit.Init();

            UIKit.OpenPanel<bagpack>(new bagpackData() { Model = mModel });
            UIKit.OpenPanel<emptymap>(new emptymapData() );
            UIKit.OpenPanel<emptyBoard>(new emptyBoardData() );
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnApplicationQuit()
        {
            mModel.Save();
        }
    }

    [System.Serializable]
    public class cubeList
    {
        public List<cubeInfo> mCubeItems = new List<cubeInfo>();

        public static cubeList Load()
        {
            var jsonContent = PlayerPrefs.GetString("CubeListData", string.Empty);

            if (jsonContent.IsNullOrEmpty())
            {
                return new cubeList();
            }
            else
            {
                return JsonUtility.FromJson<cubeList>(jsonContent);
            }
        }

        public void Save()
        {
            PlayerPrefs.SetString("CubeListData", JsonUtility.ToJson(this));
        }
    }

    [System.Serializable]
    public class cubeInfo
    {
        public string cube_name;
        public string cube_id;
        public cubeLoc cubeLoc;
        public bool is_selected;
    }

    [System.Serializable]
    public class cubeLoc
    {
        public float x;
        public float y;
        public float z;

        public cubeLoc(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    }
}