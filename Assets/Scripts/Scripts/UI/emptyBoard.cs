using UnityEngine;
using UnityEngine.UI;
using QFramework;

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
