using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Common;


public class EquippedUI : MonoBehaviour
{
	public Sprite myImg;
	private void OnEnable()
	{
		PlayerMyController.Instance.EnabledWindowCount++;

		GameObject.Find("helmetItem").GetComponent<Image>().sprite = myImg;
		GameObject.Find("ArmourItem").GetComponent<Image>().sprite = myImg;
		GameObject.Find("LeftItem").GetComponent<Image>().sprite = myImg;
		GameObject.Find("RightItem").GetComponent<Image>().sprite = myImg;
		GameObject.Find("AccessoryItem").GetComponent<Image>().sprite = myImg;
		GameObject.Find("medicineItem").GetComponent<Image>().sprite = myImg;

		foreach (var kv in PlayerAttribute.m_item)
		{
			if( kv.Value.equipped == 1)
			{
				if (kv.Value.atype == A_ItemType.helmet )
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("helmetItem").GetComponent<Image>().sprite = icon;
				}
				if (kv.Value.atype == A_ItemType.armour)
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("ArmourItem").GetComponent<Image>().sprite = icon;
				}
				if (kv.Value.atype == A_ItemType.left_weapon)
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("LeftItem").GetComponent<Image>().sprite = icon;
				}
				if (kv.Value.atype == A_ItemType.right_weapon)
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("RightItem").GetComponent<Image>().sprite = icon;
				}
				if (kv.Value.atype == A_ItemType.accessory)
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("AccessoryItem").GetComponent<Image>().sprite = icon;
				}
				if (kv.Value.atype == A_ItemType.medicine)
				{
					Sprite icon = GetAllIcons.icons[kv.Value.name];
					GameObject.Find("medicineItem").GetComponent<Image>().sprite = icon;
				}
			}
		}
	}

	private void OnDisable()
	{
		PlayerMyController.Instance.EnabledWindowCount--;
	}

}

