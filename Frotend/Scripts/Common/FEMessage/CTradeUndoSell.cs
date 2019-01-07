using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CTradeUndoSell: Message
	{
		public CTradeUndoSell() : base(Command.C_TRADE_UNDO_SELL) { }
        //public M_Item item;
        public int item_id;
        public int err_id=0; 
	}
}
