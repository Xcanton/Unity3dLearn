/****************************************************************************
 * 2022.9 DESKTOP-Q8U2JSO
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	public partial class Menu
	{
		[SerializeField] public UnityEngine.UI.Button Create_cube;
		[SerializeField] public UnityEngine.UI.ScrollRect ScrollView;
		[SerializeField] public RectTransform Content;

		public void Clear()
		{
			Create_cube = null;
			ScrollView = null;
			Content = null;
		}

		public override string ComponentName
		{
			get { return "Menu";}
		}
	}
}
