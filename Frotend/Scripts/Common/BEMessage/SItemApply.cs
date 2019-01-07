using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class SItemApply : Message
	{
		public SItemApply() : base(Command.S_ITEM_APPLY) { }
		public string apply_item;
		public bool success;
		public string apply_or_discharge;
		public int equipped_id;
	}

}