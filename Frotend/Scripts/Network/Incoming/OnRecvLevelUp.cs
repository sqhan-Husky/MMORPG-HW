using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common;

namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvLevelUp(IChannel channel, Message message)
		{
			SLevelUp msg = message as SLevelUp;
			PlayerAttribute.level++;
		}
	}
}
