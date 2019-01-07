using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;


namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvItemBuy(IChannel channel, Message message)
		{
			SItemBuy msg = message as SItemBuy;
			int currentMaxID = msg.currentID;

			if (msg.success)
			{
				foreach (var kv in msg.m_items)
				{
					for (int i = 0; i < kv.Value; i++)
					{
						currentMaxID ++;
						PlayerAttribute.m_item.Add(currentMaxID, PlayerAttribute.market_item[kv.Key]);
						PlayerAttribute.m_item[currentMaxID].pi_id = currentMaxID;
						PlayerAttribute.m_item[currentMaxID].owner_name = PlayerAttribute.user;
					}
				}
				PlayerAttribute.gcurrentCost = 0;
				PlayerAttribute.scurrentCost = 0;
				PlayerAttribute.count ++;

				if (Equals(msg.gold_or_silver, "silver"))
				{
					PlayerAttribute.s_coin = msg.remains;
				}
				else
				{
					PlayerAttribute.g_coin = msg.remains;
				}

				GameObject.FindObjectOfType<CartGridUI>().AfterBuy();
			}







		}
	}
}
