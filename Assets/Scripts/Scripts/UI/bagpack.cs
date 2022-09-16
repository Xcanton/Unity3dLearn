using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Meta;
using System.Linq;

namespace QFramework.CubeUI
{
	public class bagpackData : UIPanelData
	{
		// 用于存储当前背包中的方块信息
		public cubeList Model;
    }

	public enum UIBagPackEvent
	{
		start = QMgrID.UI,
		OnDataChange,
		CreateCube,
		DeleteCube,  // 背包注册并监听该事件，通过每个info item触发发送该事件sendMsg，实现背包列表的修改与回传
		End
	}

	public partial class bagpack : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as bagpackData ?? new bagpackData();
            // please add init code here

            OnDataChange();

            // 注册UI事件
            RegisterEvent(UIBagPackEvent.OnDataChange);
			RegisterEvent(UIBagPackEvent.CreateCube);
			RegisterEvent(UIBagPackEvent.DeleteCube);

            // 绑定监听
            Menu.Create_cube.onClick.AddListener(() =>
			{
				var cube_object = GameObject.Find("Factory").GetComponent<factory>().outer_generate_return();
                mData.Model.mCubeItems.Add(new cubeInfo()
                {
                    cube_name = cube_object.name,
                    cube_id = cube_object.GetComponent<cube>().id,
                    cubeLoc = new cubeLoc(cube_object.transform.position.x, cube_object.transform.position.y, cube_object.transform.position.z),
                    is_selected = true
                });
                Debug.Log(mData.Model.mCubeItems);
                Debug.Log(mData.Model.mCubeItems.Count());

                OnDataChange();
            });
			Debug.Log("背包挂监听");
			GameObject.Find("Factory").GetComponent<factory>().events.factoryGenerate.AddListener((string id, string name, Vector3 loc_vect) =>
			{
				mData.Model.mCubeItems.Add(new cubeInfo()
				{
					cube_id = id,
					cube_name = name,
					cubeLoc = new cubeLoc(loc_vect.x, loc_vect.y, loc_vect.z),
					is_selected=true
				});

                OnDataChange();
            });


        }

		void OnDataChange()
		{
			Menu.ScrollView.content.DestroyAllChild();

			mData.Model.mCubeItems.ForEach(item =>
			{
				InfoItem.Instantiate()
				.Parent(Menu.ScrollView.content)
				.LocalIdentity()
				.ApplySelfTo(self => self.Init(item))
				.Show();
			});
		}

		public class DeleteCubeMsg : QMsg
		{
			public cubeInfo Item_data;
			
			public DeleteCubeMsg(cubeInfo item_data): base((int)UIBagPackEvent.DeleteCube)
			{
				Item_data = item_data;
			}
        }

		protected override void ProcessMsg(int eventId, QMsg msg)
		{
			switch (eventId)
			{
				case (int)UIBagPackEvent.DeleteCube:

					var deleteMsg = msg as DeleteCubeMsg;

                    mData.Model.mCubeItems.RemoveAll(item => item == deleteMsg.Item_data);
					OnDataChange();
                    break;
				
			}
		}


		protected override void OnOpen(IUIData uiData = null)
		{
			
		}

		private void Update()
		{
			
			//if (Input.GetKeyDown(KeyCode.T))
			//{
			//	mData.Model.mCubeItems.Add(new cubeInfo()
			//	{
			//		cube_name = "",
			//		cube_id = "",
			//		cubeLoc = new cubeLoc(0f,0f,0f),
			//		is_selected = false
			//	});
			//}
		}

		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}

}
