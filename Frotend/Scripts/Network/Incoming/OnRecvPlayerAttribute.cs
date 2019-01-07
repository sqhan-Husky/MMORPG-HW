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
		private void OnRecvPlayerAttribute(IChannel channel, Message message)
		{

			SPlayerAttribute msg = message as SPlayerAttribute;

			PlayerAttribute.user = msg.user;
			PlayerAttribute.level = msg.level;
			PlayerAttribute.Attack = msg.Attack;
			PlayerAttribute.currentHP= msg.currentHP;
			PlayerAttribute.Defense = msg.Defense;
			PlayerAttribute.Intelligence = msg.Intelligence;
			PlayerAttribute.Speed = msg.Speed;
			PlayerAttribute.id = msg.id;
			PlayerAttribute.m_item = new Dictionary<int, M_Item>();
			PlayerAttribute.market_item = new Dictionary<string, M_Item>();
			PlayerAttribute.trade_item = new Dictionary<int, M_Item>();
			PlayerAttribute.trade_item = msg.trade_item;//new
			PlayerAttribute.m_item = msg.m_item;
			PlayerAttribute.market_item = msg.market_item;
			PlayerAttribute.count = msg.count;
			PlayerAttribute.equipped_count = msg.equipped_count;

			//PlayerMyController.Instance.m_item = msg.m_item;

			PlayerAttribute.s_coin = msg.s_coin;
			PlayerAttribute.g_coin = msg.g_coin;
			Debug.Log(msg.g_coin);

			PlayerAttribute.gcurrentCost = 0;
			PlayerAttribute.scurrentCost = 0;
			PlayerAttribute.sTradecurrentCost = 0;

		}
	}
}
