//EXTEND_BEGIN
using Common;
using UnityEngine;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvGiveGift(IChannel channel, Message message)
        {
            SGiveGift response = message as SGiveGift;
			CRecvGift request = new CRecvGift();
            M_Item item = response.item;
            PlayerAttribute.m_item.Add(item.pi_id,item);

			request.msg="You have received a(an) "+item.name+" from your friend " + response.friend_name;
			Client.Instance.Send(request);
            Debug.Log("Get Gift");
        }
    }
}
//EXTEND_END