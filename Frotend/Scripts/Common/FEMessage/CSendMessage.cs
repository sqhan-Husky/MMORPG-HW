//EXTEND_BEGIN
using System;

namespace Common
{
    [Serializable]
    public class CSendMessage : Message
    {
        public CSendMessage() : base(Command.C_SEND_MESSAGE) {

        }
        public int sender_id;
        public int receiver_id;
        public string msg;
    }
}
//EXTEND_END
