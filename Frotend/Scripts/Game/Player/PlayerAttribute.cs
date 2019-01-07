using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gamekit3D.Network;
using Common;

public class PlayerAttribute
	{
		public static string user;

		public static int level;

		public static int currentHP;

		public static int id;

		public static int Intelligence;

		public static int Speed;

		public static int Attack;

		public static int Defense;

		public static int count;

		public static int equipped_count;

		public static IDictionary<int, M_Item> m_item;

		public static string nowequipped;

		public static int nowequipped_id;

		public static IDictionary<string, M_Item> market_item;

        public static IDictionary<int, M_Item> trade_item;//new

        public static int s_coin;

		public static int g_coin;

		public static int gcurrentCost;

		public static int scurrentCost;

        public static int sTradecurrentCost;//new

}

