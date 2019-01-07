using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CBuyItems : Message
	{
		public CBuyItems() : base(Command.C_BUY_ITEMS) { }
		public Dictionary<string, int> m_items;
		public string gold_or_silver;
		public int sum_cost;

	}
}
