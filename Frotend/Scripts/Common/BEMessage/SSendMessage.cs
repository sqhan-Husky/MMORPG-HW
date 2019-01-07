//EXTEND_BEGIN
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class SSendMessage : Message
    {
        public SSendMessage() : base(Command.S_SEND_MESSAGE) {
            msg_num = 0;
        }
        public int msg_num;
    }
}
//EXTEND_END
