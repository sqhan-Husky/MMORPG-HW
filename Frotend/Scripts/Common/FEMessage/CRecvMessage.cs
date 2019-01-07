//EXTEND_BEGIN
using System;

namespace Common
{
    [Serializable]
    public class CRecvMessage : Message
    {
        public CRecvMessage() : base(Command.C_RECV_MESSAGE) {
            
        }
        public int sender_id;
        public int receiver_id;
        public int msg_num;
    }
}
//EXTEND_END
