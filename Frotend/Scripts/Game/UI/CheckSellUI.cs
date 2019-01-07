using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class CheckSellUI : MonoBehaviour
{
    public InputField Num;
    public Button checkSuccess;


    private void Awake()
    {

    }

    private void OnEnable()
    {
        checkSuccess.onClick.AddListener(delegate ()
        {
            Tosell();
        });
    }

    public void Tosell()
    {
		var input = GameObject.Find("howmuchInputField");
		if (input != null)
		{
			int price = Convert.ToInt32(input.GetComponent<InputField>().text);
			Debug.Log(price);
			int item_id = PlayerAttribute.nowequipped_id;

			// add to list & send message
			CTradeSell request = new CTradeSell();

			if (PlayerAttribute.m_item[item_id].equipped == 0)
			{
				PlayerAttribute.m_item[item_id].equipped = 2;
				PlayerAttribute.trade_item.Add(item_id, PlayerAttribute.m_item[item_id]);
				request.item = PlayerAttribute.m_item[item_id];
				request.price = price;
				PlayerAttribute.trade_item[item_id].s_coin = price;
				PlayerAttribute.trade_item[item_id].g_coin = 0;
			}
			else if (PlayerAttribute.m_item[item_id].equipped == 1)
			{
				request.err_id = 1;
			}
			else if (PlayerAttribute.m_item[item_id].equipped == 2)
			{
				request.err_id = 2;
			}

			Client.Instance.Send(request);
			GameObject.FindObjectOfType<InventoryUI>().newAttribute();
			GameObject.Find("CheckToSellView").SetActive(false);
		}
    }
}
