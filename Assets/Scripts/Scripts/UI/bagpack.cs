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
		// ���ڴ洢��ǰ�����еķ�����Ϣ
		public cubeList Model;
    }

	public enum UIBagPackEvent
	{
		start = QMgrID.UI,
		OnDataChange,
		CreateCube,
		DeleteCube,  // ����ע�Ტ�������¼���ͨ��ÿ��info item�������͸��¼�sendMsg��ʵ�ֱ����б���޸���ش�
		End
	}

	public partial class bagpack : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as bagpackData ?? new bagpackData();
            // please add init code here

            OnDataChange();

            // ע��UI�¼�
            RegisterEvent(UIBagPackEvent.OnDataChange);
			RegisterEvent(UIBagPackEvent.CreateCube);
			RegisterEvent(UIBagPackEvent.DeleteCube);

            // �󶨼���
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
			Debug.Log("�����Ҽ���");
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
