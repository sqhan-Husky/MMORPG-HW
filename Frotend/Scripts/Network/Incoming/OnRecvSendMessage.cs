//EXTEND_BEGIN
using Common;
using UnityEngine;
using System;

namespace Gamekit3D.Network
{
    public partial class Incoming
    {
        private void OnRecvSendMessage(IChannel channel, Message message)
        {
            SSendMessage response = message as SSendMessage;
            GameObject.FindObjectOfType<ChatUI>().msgNum = response.msg_num;
        }
    }
}
//EXTEND_END