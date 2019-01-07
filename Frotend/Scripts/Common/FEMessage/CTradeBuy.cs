using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CTradeBuy: Message
	{
		public CTradeBuy() : base(Command.C_TRADE_BUY) { }
		public Dictionary<int, int> m_items;//key:物品id，Value：价格
		public int sum_cost;

	}
}
