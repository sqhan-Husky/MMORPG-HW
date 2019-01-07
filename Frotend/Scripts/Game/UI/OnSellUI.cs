using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using System;
using Common;
using TMPro;

public class OnSellUI : MonoBehaviour
{

	public GameObject OnSellCell;
	public GameObject OnSellGridContent;

	// Use this for initialization

	private void Awake()
	{
		OnSellCell.SetActive(false);
	}

	private void OnEnable()
	{
		int capacity = PlayerMyController.Instance.InventoryCapacity;
		int count = 0;

		foreach (var kv in PlayerAttribute.m_item)
		{
			if (kv.Value.equipped == 2)
			{
				GameObject cloned = GameObject.Instantiate(OnSellCell);
				Button button = cloned.GetComponent<Button>();
				Sprite icon = GetAllIcons.icons[kv.Value.name];
				button.image.sprite = icon;
				cloned.SetActive(true);
				cloned.transform.SetParent(OnSellGridContent.transform, false);
				count++;
			}
		}

		for (int i = 0; i < capacity - count; i++)
		{
			GameObject cloned = GameObject.Instantiate(OnSellCell);
			cloned.SetActive(true);
			cloned.transform.SetParent(OnSellGridContent.transform, false);
		}
		PlayerMyController.Instance.EnabledWindowCount++;
	}


	private void OnDisable()
	{
		int cellCount = OnSellGridContent.transform.childCount;
		foreach (Transform transform in OnSellGridContent.transform)
		{
			Destroy(transform.gameObject);
			var go = GameObject.Find("RoleWindow");
			var role = GameObject.FindObjectOfType<RoleUI>();

		}

		PlayerMyController.Instance.EnabledWindowCount--;
	}

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}


}
