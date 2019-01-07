using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class STradeBuy : Message
	{
		public STradeBuy() : base(Command.S_TRADE_BUY) { }
		public Dictionary<int, M_Item> m_items;
		public bool success;
		public int remains;
	}
}
