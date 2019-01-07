//EXTEND_BEGIN
using Common;
using UnityEngine;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvRecvMessage(IChannel channel, Message message)
        {
            SRecvMessage response = message as SRecvMessage;
            GameObject.FindObjectOfType<ChatUI>().msgNum = response.msg_num;
            Debug.Log("msgNum:" + GameObject.FindObjectOfType<ChatUI>().msgNum + ",user_id:" + ChatMessage.Instance.user_id + ",friend_id:" + ChatMessage.Instance.friend_id);
            if (response.sender_id.Equals(ChatMessage.Instance.friend_id) && response.receiver_id.Equals(ChatMessage.Instance.user_id))
            {
                GameObject.FindObjectOfType<ChatUI>().ReceiveFriendMessage(response.msg);
            }
            if (response.sender_id.Equals(ChatMessage.Instance.user_id) && response.receiver_id.Equals(ChatMessage.Instance.friend_id))
            {
                GameObject.FindObjectOfType<ChatUI>().SendMyMessage(response.msg);
            }
        }
    }
}
//EXTEND_END