using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gamekit3D;
using Gamekit3D.Network;
using System;
using Common;
using TMPro;

public class InventoryUI : MonoBehaviour
{
	public GameObject InventoryCell;
	public GameObject InventoryGridContent;
	public GameObject ValueButton;
	public GameObject NameButton;
	public GameObject AttributeButton;
	public GameObject StatusButton;

	public TextMeshProUGUI InteligenceValue;
	public TextMeshProUGUI SpeedValue;
	public TextMeshProUGUI AttackValue;
	public TextMeshProUGUI DefenseValue;
	public Text EquippedCount;

	public int item_clicked_id;

	// Use this for initialization

	private void Awake()
	{
		InventoryCell.SetActive(false);
	}

	private void OnEnable()
	{
		//PlayerMyController.Instance.EnabledWindowCount++;

		int capacity = PlayerMyController.Instance.InventoryCapacity;
		int count = PlayerAttribute.count;

		foreach (var kv in PlayerAttribute.m_item)
		{
			Console.WriteLine(kv.Value.name);
			GameObject cloned = GameObject.Instantiate(InventoryCell);
			Button button = cloned.GetComponent<Button>();
			Sprite icon = GetAllIcons.icons[kv.Value.name];
			button.image.sprite = icon;
			button.onClick.AddListener(delegate ()
			{
				SetAttr(kv, icon);
			});
			cloned.SetActive(true);
			cloned.transform.SetParent(InventoryGridContent.transform, false);
		}

		for (int i = 0; i < capacity - count; i++)
		{
			GameObject cloned = GameObject.Instantiate(InventoryCell);
			cloned.SetActive(true);
			cloned.transform.SetParent(InventoryGridContent.transform, false);
		}
		PlayerMyController.Instance.EnabledWindowCount++;
	}

	public void SetAttr(KeyValuePair<int, M_Item> kv, Sprite icon)
	{
		PlayerAttribute.nowequipped = kv.Value.name;
		PlayerAttribute.nowequipped_id = kv.Value.pi_id;


		GameObject.Find("ItemImage").GetComponent<Image>().sprite = icon;
		if (kv.Value.mtype == M_ItemType.attack) { ValueButton.GetComponentInChildren<Text>().text = string.Format("攻击加成： {0}", kv.Value.value); }
		else if (kv.Value.mtype == M_ItemType.intelligence) { ValueButton.GetComponentInChildren<Text>().text = string.Format("智慧加成： {0}", kv.Value.value); }
		else if (kv.Value.mtype == M_ItemType.speed) { ValueButton.GetComponentInChildren<Text>().text = string.Format("速度加成： {0}", kv.Value.value); }
		else if (kv.Value.mtype == M_ItemType.defense) { ValueButton.GetComponentInChildren<Text>().text = string.Format("防守加成： {0}", kv.Value.value); }

		NameButton.GetComponentInChildren<Text>().text = string.Format("名称： {0}", kv.Value.name);
		if (kv.Value.equipped == 1) { StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 已装备"); }
		else if (kv.Value.equipped == 0) { StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 未装备"); }
		else if (kv.Value.equipped == 2) { StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 待售中"); }

		AttributeButton.GetComponentInChildren<Text>().text = string.Format("种类： {0}", kv.Value.atype);

		Debug.Log("Gift value op");
		var gift = GameObject.FindObjectOfType<GiftUI>();
		if (gift != null)
		{
			gift.NameButton.GetComponentInChildren<Text>().text = NameButton.GetComponentInChildren<Text>().text;
			gift.AttributeButton.GetComponentInChildren<Text>().text = AttributeButton.GetComponentInChildren<Text>().text;
			gift.ValueButton.GetComponentInChildren<Text>().text = ValueButton.GetComponentInChildren<Text>().text;
			gift.StatusButton.GetComponentInChildren<Text>().text = StatusButton.GetComponentInChildren<Text>().text;
			item_clicked_id = kv.Value.pi_id;
		}
		else
		{
			Debug.Log("Gift null");
		}


		Debug.Log("Gift value ed");
	}

	private void OnDisable()
	{
		int cellCount = InventoryGridContent.transform.childCount;
		foreach (Transform transform in InventoryGridContent.transform)
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

	void ExtendBagCapacity(int n)
	{
		int cellCount = InventoryGridContent.transform.childCount;
		for (int i = 0; i < n - cellCount; i++)
		{
			GameObject cloned = GameObject.Instantiate(InventoryCell);
			cloned.SetActive(true);
			cloned.transform.SetParent(InventoryGridContent.transform, false);
		}
	}

	public void OnApplyButtonClicked()
	{
		CItemApply msg = new CItemApply();
		msg.itemName = PlayerAttribute.nowequipped;
		msg.playerName = PlayerAttribute.user;
		msg.apply_or_discharge = "apply";
		msg.equipped_id = PlayerAttribute.nowequipped_id;

		Client.Instance.Send(msg);

		/*
        CItemApply msg = new CItemApply();
        msg.itemName = PlayerAttribute.nowequipped;
        msg.playerName = PlayerAttribute.user;
        msg.apply_or_discharge = "apply";
        msg.equipped_id = PlayerAttribute.nowequipped_id;
        if (PlayerAttribute.equipped_count == 6)
        {
            msg.err_id = 1;
        }
        else if(PlayerAttribute.m_item[item_clicked_id].equipped == 1)
        {
            msg.err_id = 2;
        }
        else 
        {
            foreach (var kv in PlayerAttribute.m_item)
            {
                if (kv.Value.pi_id != msg.equipped_id)
                {
                    if (kv.Value.equipped == 1 && kv.Value.atype == PlayerAttribute.m_item[msg.equipped_id].atype)
                    {
                        msg.err_id = 3;
                    }
                }
            }
        }

        if(msg.err_id == 0)
        {
            PlayerAttribute.m_item[item_clicked_id].equipped = 1;
            PlayerAttribute.equipped_count++;

            M_Item item = PlayerAttribute.m_item[item_clicked_id];
            if (item.mtype == M_ItemType.attack) { PlayerAttribute.Attack += item.value; }
            else if (item.mtype == M_ItemType.intelligence) { PlayerAttribute.Intelligence += item.value; }
            else if (item.mtype == M_ItemType.speed) { PlayerAttribute.Speed += item.value; }
            else if (item.mtype == M_ItemType.defense) { PlayerAttribute.Defense += item.value; }
        }
        Client.Instance.Send(msg);
        */
	}

	//
	public void OnGiveButtonClicked()
	{
		int status = PlayerAttribute.m_item[item_clicked_id].equipped;
		CGiveGift gift_msg = new CGiveGift();
		if (status.Equals(0))
		{
			gift_msg.sender_id = ChatMessage.Instance.user_id;
			gift_msg.receiver_id = ChatMessage.Instance.friend_id;
			string sender_name = ChatMessage.Instance.friend_list[gift_msg.sender_id];
			Debug.Log("GIFT OP");

			foreach (var kvp in PlayerAttribute.m_item)
			{
				if (kvp.Value.pi_id.Equals(item_clicked_id))
				{
					gift_msg.item = kvp.Value;
					PlayerAttribute.m_item.Remove(kvp.Key);
					break;
				}
			}

			gift_msg.msg = "Receive item " + gift_msg.item.name + " from " + sender_name;
			Debug.Log(gift_msg.msg);
			Debug.Log("GIFT ED");
		}
		else if (status.Equals(1))
		{
			gift_msg.err_id = 1;
		}
		else if (status.Equals(2))
		{
			gift_msg.err_id = 2;
		}
		Client.Instance.Send(gift_msg);
	}
	//

	public void OnDisChargeButtonClicked()
	{
		CItemApply msg = new CItemApply();
		msg.itemName = PlayerAttribute.nowequipped;
		msg.playerName = PlayerAttribute.user;
		msg.apply_or_discharge = "discharge";
		msg.equipped_id = PlayerAttribute.nowequipped_id;
		Client.Instance.Send(msg);
	}

	public void newAttribute()
	{
		foreach (var kv in PlayerAttribute.m_item)
		{
			if (kv.Value.pi_id == PlayerAttribute.nowequipped_id && kv.Value.equipped == 1)
			{
				StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 已装备");
				InteligenceValue.SetText(PlayerAttribute.Intelligence.ToString(), true);
				AttackValue.SetText(PlayerAttribute.Attack.ToString(), true);
				DefenseValue.SetText(PlayerAttribute.Defense.ToString(), true);
				SpeedValue.SetText(PlayerAttribute.Speed.ToString(), true);
				RoleUI role = GameObject.FindObjectOfType<RoleUI>();
				role.GetComponent<RoleUI>().EquippedCount.text = string.Format("装备数量：{0}/6", PlayerAttribute.equipped_count);
				return;
			}
			else if (kv.Value.pi_id == PlayerAttribute.nowequipped_id && kv.Value.equipped == 0)
			{
				StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 未装备");
				InteligenceValue.SetText(PlayerAttribute.Intelligence.ToString(), true);
				AttackValue.SetText(PlayerAttribute.Attack.ToString(), true);
				DefenseValue.SetText(PlayerAttribute.Defense.ToString(), true);
				SpeedValue.SetText(PlayerAttribute.Speed.ToString(), true);
				RoleUI role = GameObject.FindObjectOfType<RoleUI>();
				role.GetComponent<RoleUI>().EquippedCount.text = string.Format("装备数量：{0}/6", PlayerAttribute.equipped_count);
				return;
			}
			else if (kv.Value.pi_id == PlayerAttribute.nowequipped_id && kv.Value.equipped == 2)
			{
				StatusButton.GetComponentInChildren<Text>().text = string.Format("状态： 待售中");
				InteligenceValue.SetText(PlayerAttribute.Intelligence.ToString(), true);
				AttackValue.SetText(PlayerAttribute.Attack.ToString(), true);
				DefenseValue.SetText(PlayerAttribute.Defense.ToString(), true);
				SpeedValue.SetText(PlayerAttribute.Speed.ToString(), true);
				RoleUI role = GameObject.FindObjectOfType<RoleUI>();
				role.GetComponent<RoleUI>().EquippedCount.text = string.Format("装备数量：{0}/6", PlayerAttribute.equipped_count);
				return;
			}

		}
	}

}
