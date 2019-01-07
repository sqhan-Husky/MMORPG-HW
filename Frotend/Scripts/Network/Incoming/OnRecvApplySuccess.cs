using System;
using System.Collections.Generic;
using Common;
using UnityEngine;


namespace Gamekit3D.Network
{
	public partial class Incoming
	{
		private void OnRecvApplySuccess(IChannel channel, Message message)
		{
			SItemApply msg = message as SItemApply;
			if (Equals(msg.apply_or_discharge,"apply") && msg.success)
			{
				foreach (var kv in PlayerAttribute.m_item)
				{
					if (kv.Value.pi_id == msg.equipped_id)
					{
							kv.Value.equipped = 1;
							PlayerAttribute.equipped_count++;
							if (kv.Value.mtype == M_ItemType.attack) { PlayerAttribute.Attack += kv.Value.value; }
							else if (kv.Value.mtype == M_ItemType.intelligence) { PlayerAttribute.Intelligence += kv.Value.value; }
							else if (kv.Value.mtype == M_ItemType.speed) { PlayerAttribute.Speed += kv.Value.value; }
							else if (kv.Value.mtype == M_ItemType.defense) { PlayerAttribute.Defense += kv.Value.value; }
					}
				}
			}

			if (Equals(msg.apply_or_discharge, "discharge") && msg.success )
			{
				foreach (var kv in PlayerAttribute.m_item)
				{
					if (kv.Value.pi_id == msg.equipped_id)
					{
						kv.Value.equipped = 0;
						PlayerAttribute.equipped_count--;
						if (kv.Value.mtype == M_ItemType.attack) { PlayerAttribute.Attack -= kv.Value.value; }
						else if (kv.Value.mtype == M_ItemType.intelligence) { PlayerAttribute.Intelligence -= kv.Value.value; }
						else if (kv.Value.mtype == M_ItemType.speed) { PlayerAttribute.Speed -= kv.Value.value; }
						else if (kv.Value.mtype == M_ItemType.defense) { PlayerAttribute.Defense -= kv.Value.value; }
					}
				}
			}

			GameObject.FindObjectOfType<InventoryUI>().newAttribute();
		}
	}
}