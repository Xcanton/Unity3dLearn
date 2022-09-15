using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	// Generate Id:027130a3-9ad7-481f-971c-ffa2496a98af
	public partial class emptymap
	{
		public const string Name = "emptymap";
		
		[SerializeField]
		public UnityEngine.UI.RawImage map;
		
		private emptymapData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			map = null;
			
			mData = null;
		}
		
		public emptymapData Data
		{
			get
			{
				return mData;
			}
		}
		
		emptymapData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new emptymapData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
