using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class SPlayerAttribute : Message
	{
		public SPlayerAttribute() : base(Command.S_PLAYER_ATTRIBUTE) { }
		public string user;
		public int currentHP;
		public int level;
		public int id;
		public int Intelligence;
		public int Speed;
		public int Attack;
		public int Defense;
		public int count;
		public int equipped_count;
		public IDictionary<int, M_Item> m_item;
		public IDictionary<string, M_Item> market_item;
		public IDictionary<int, M_Item> trade_item;
		public int s_coin;
		public int g_coin;
		//public int dirty_scoin;

	}

}