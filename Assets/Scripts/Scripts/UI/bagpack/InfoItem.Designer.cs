/****************************************************************************
 * 2022.9 DESKTOP-Q8U2JSO
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	public partial class InfoItem
	{
		[SerializeField] public UnityEngine.UI.Button background_click;
		[SerializeField] public UnityEngine.UI.Text cube_name;
		[SerializeField] public UnityEngine.UI.Text cube_location;
		[SerializeField] public UnityEngine.UI.Text cube_id;
		[SerializeField] public UnityEngine.UI.Button delete_button;

		public void Clear()
		{
			background_click = null;
			cube_name = null;
			cube_location = null;
			cube_id = null;
			delete_button = null;
		}

		public override string ComponentName
		{
			get { return "InfoItem";}
		}
	}
}
