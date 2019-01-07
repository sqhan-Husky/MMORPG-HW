//EXTEND_BEGIN
using Common;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvGetFriends(IChannel channel, Message message)
        {
            SGetFriends msg = message as SGetFriends;
			ChatMessage.Instance.friend_list = new Dictionary<int, string>();
			int i = 0;
            for (i=0;i<msg.players.Count;++i)
            {
                ChatMessage.Instance.friend_list.Add(msg.players[i],msg.names[i]);
            }
            ChatMessage.Instance.user_id = msg.player_id;
            Debug.Log(msg.player_id);
            GameObject.FindObjectOfType<FriendUI>().ShowFriendList();
        }
    }
}
//EXTEND_END