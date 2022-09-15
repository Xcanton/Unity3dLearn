using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	public class emptymapData : UIPanelData
	{
	}
	public partial class emptymap : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as emptymapData ?? new emptymapData();
			// please add init code here
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
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
