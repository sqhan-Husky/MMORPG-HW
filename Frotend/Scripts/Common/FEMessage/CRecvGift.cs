//EXTEND_BEGIN
using System;

namespace Common
{
    [Serializable]
    public class CRecvGift : Message
    {
        public CRecvGift(): base(Command.C_RECV_GIFT) {

        }
        public string msg;
        //public int err_id = 0;
    }
}
//EXTEND_END
