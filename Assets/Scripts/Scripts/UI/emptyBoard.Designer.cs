using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.CubeUI
{
	// Generate Id:42e8bacf-049f-4a5b-87a5-898039312858
	public partial class emptyBoard
	{
		public const string Name = "emptyBoard";
		
		[SerializeField]
		public UnityEngine.UI.Toggle generateToggle;
		[SerializeField]
		public UnityEngine.UI.Toggle selectToggle;
		[SerializeField]
		public UnityEngine.UI.Toggle moveToggle;
		
		private emptyBoardData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			generateToggle = null;
			selectToggle = null;
			moveToggle = null;
			
			mData = null;
		}
		
		public emptyBoardData Data
		{
			get
			{
				return mData;
			}
		}
		
		emptyBoardData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new emptyBoardData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
