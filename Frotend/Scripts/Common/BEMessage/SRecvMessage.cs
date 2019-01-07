//EXTEND_BEGIN
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class SRecvMessage : Message
    {
        public SRecvMessage() : base(Command.S_RECV_MESSAGE) {

        }
        public int sender_id;
        public int receiver_id;
        public int msg_num;
        public string msg;
    }
}
//EXTEND_END
