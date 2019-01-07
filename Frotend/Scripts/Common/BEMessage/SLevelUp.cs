using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
	[Serializable]
	public class SLevelUp : Message
	{
		public SLevelUp() : base(Command.S_LEVEL_UP) { }
		public string user_name;
	}
}
