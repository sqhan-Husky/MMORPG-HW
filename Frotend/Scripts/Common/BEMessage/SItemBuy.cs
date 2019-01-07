using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class SItemBuy : Message
	{
		public SItemBuy() : base(Command.S_BUY_ITEMS) { }
		public Dictionary<string, int> m_items;
		public bool success;
		public string gold_or_silver;
		public int remains;
		public int currentID;
	}
}
