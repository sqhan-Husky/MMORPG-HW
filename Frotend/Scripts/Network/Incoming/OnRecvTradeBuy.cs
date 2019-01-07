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
		private void OnRecvTradeBuy(IChannel channel, Message message)
		{
			STradeBuy msg = message as STradeBuy;
			if (msg.success)
			{
				foreach (var kv in msg.m_items)
				{
					PlayerAttribute.m_item.Add(kv.Key, kv.Value);
					PlayerAttribute.trade_item.Remove(kv.Key);
				}
				PlayerAttribute.sTradecurrentCost = 0;

				PlayerAttribute.s_coin = msg.remains;

				GameObject.FindObjectOfType<TradeCartGridUI>().AfterBuy();
			}
		}
	}
}
