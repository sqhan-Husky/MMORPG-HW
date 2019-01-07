//EXTEND_BEGIN
using System;

namespace Common
{
    [Serializable]
    public class CGiveGift : Message
    {
        public CGiveGift() : base(Command.C_GIVE_GIFT) {

        }
        public int sender_id;
        public int receiver_id;
        public M_Item item;
        public string msg;

        public int err_id = 0;
    }
}
//EXTEND_END
