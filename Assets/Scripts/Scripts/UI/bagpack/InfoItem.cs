/****************************************************************************
 * 2022.9 DESKTOP-Q8U2JSO
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using static QFramework.CubeUI.bagpack;
using Meta;

namespace QFramework.CubeUI
{
	public partial class InfoItem : UIElement
	{
		private void Awake()
		{
			delete_button.transform.GetComponentInChildren<Text>().text = "É¾³ý";

        }

		protected override void OnBeforeDestroy()
		{
		}

		public cubeInfo info_item;

        public void Init(cubeInfo iItem)
		{
			info_item = iItem;

			cube_name.text = info_item.cube_name;
			cube_id.text = info_item.cube_id;
			cube_location.text = String.Concat(info_item.cubeLoc.x, ",", info_item.cubeLoc.y, ",", info_item.cubeLoc.z);

			delete_button.onClick.AddListener(() =>
			{
                GameObject.Find("Factory").GetComponent<factory>().itemDelete(info_item.cube_id);

                SendMsg(new DeleteCubeMsg(info_item));
				Destroy(gameObject);
            });

			background_click.onClick.AddListener(() =>
			{
				GameObject.Find("Factory").GetComponent<factory>().ViewPointItemClicked(info_item.cube_id);

            });

        }


	}
}