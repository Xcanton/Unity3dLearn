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
                mData.Model.mCubeItems.Add(new cubeInfo()
                {
                    cube_name = "",
                    cube_id = "",
                    cubeLoc = new cubeLoc(0f, 0f, 0f),
                    is_selected = false
                });
                Debug.Log(mData.Model.mCubeItems);
                Debug.Log(mData.Model.mCubeItems.Count());

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
