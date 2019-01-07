using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CItemApply : Message
	{
		public CItemApply() : base(Command.C_ITEM_APPLY) { }
		public string itemName;
		public string playerName;
		public string apply_or_discharge;
		public int equipped_id;

        public int err_id = 0;
	}
}
