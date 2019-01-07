using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CTradeSell: Message
	{
		public CTradeSell() : base(Command.C_TRADE_SELL) { }
		public M_Item item;
		public int price;

        public int err_id=0; 
	}
}
