using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class CAddCoin : Message
	{
		public CAddCoin() : base(Command.C_ADD_COIN) { }
		public string gold_or_silver;
		public int num;
	}
}
