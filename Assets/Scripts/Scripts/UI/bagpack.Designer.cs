using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	// Generate Id:b95d1e98-9f2d-4dc3-bc1e-39489e06ea99
	public partial class bagpack
	{
		public const string Name = "bagpack";
		
		[SerializeField]
		public Menu Menu;
		[SerializeField]
		public InfoItem InfoItem;
		
		private bagpackData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Menu = null;
			InfoItem = null;
			
			mData = null;
		}
		
		public bagpackData Data
		{
			get
			{
				return mData;
			}
		}
		
		bagpackData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new bagpackData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
