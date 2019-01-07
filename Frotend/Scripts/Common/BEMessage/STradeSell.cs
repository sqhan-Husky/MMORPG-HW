using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class STradeSell : Message
	{
		public STradeSell() : base(Command.S_TRADE_SELL) { }
		public Dictionary<int, M_Item> m_items;
		public bool success;
		public int remains;
		public int currentID;
	}
}
