using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Common;
using Gamekit3D.Network;

public class UndoSellUI : MonoBehaviour
{
    public Button undoSuccess;

    private void Awake()
    {

    }
    private void OnEnable()
    {
        undoSuccess.onClick.AddListener(delegate ()
        {
            Undosell();
        });
    }

	public void Undosell()
	{
		var usv = GameObject.Find("UndoSellView");
		if (usv != null)
		{
			//int price = Convert.ToInt32(GameObject.Find("howmuchInputField").GetComponent<InputField>().text);
			int item_id = PlayerAttribute.nowequipped_id;

			// remove from list & send message
			CTradeUndoSell request = new CTradeUndoSell();

			if (PlayerAttribute.m_item[item_id].equipped == 2)
			{
				PlayerAttribute.m_item[item_id].equipped = 0;
				PlayerAttribute.trade_item.Remove(item_id);
				request.item_id = item_id;
			}
			else if(PlayerAttribute.m_item[item_id].equipped == 1)
			{
				request.err_id = 1;
			}
			else if(PlayerAttribute.m_item[item_id].equipped == 0)
			{
				request.err_id = 2;
			}
			Client.Instance.Send(request);
			GameObject.FindObjectOfType<InventoryUI>().newAttribute();

			usv.SetActive(false);
		}
	}
}
