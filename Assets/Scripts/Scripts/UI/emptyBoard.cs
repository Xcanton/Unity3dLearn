using UnityEngine;
using UnityEngine.UI;
using QFramework;
using Meta;

namespace QFramework.CubeUI
{
	public class emptyBoardData : UIPanelData
	{
	}
	public partial class emptyBoard : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as emptyBoardData ?? new emptyBoardData();
			// please add init code here

			GameObject.Find("Factory").GetComponent<factory>().events.dropdownChange.AddListener((int par) => FactoryChangeMode(par));

			generateToggle.onValueChanged.AddListener((bool value) => { if (value) { GameObject.Find("Factory").GetComponent<factory>().dropdownchange((int)Status_enum.generate); } }) ;
            selectToggle.onValueChanged.AddListener((bool value) => { if (value) { GameObject.Find("Factory").GetComponent<factory>().dropdownchange((int)Status_enum.select); } });
            moveToggle.onValueChanged.AddListener((bool value) => { if (value) { GameObject.Find("Factory").GetComponent<factory>().dropdownchange((int)Status_enum.move); } });
        }
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}

		private void FactoryChangeMode(int factory_index)
		{
			Debug.Log("接收到工厂事件");
			Debug.Log((Status_enum)int.Parse(factory_index.ToString()));

			switch ((Status_enum)int.Parse(factory_index.ToString()))
			{
				case Status_enum.generate:
					generateToggle.isOn = true;
					break;
                case Status_enum.select:
					selectToggle.isOn = true;
                    break;
                case Status_enum.move:
					moveToggle.isOn = true;
					break;
            }
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
