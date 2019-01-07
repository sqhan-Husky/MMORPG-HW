//EXTEND_BEGIN
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class SGiveGift : Message
    {
        public SGiveGift() : base(Command.S_GIVE_GIFT) {
        }
        public M_Item item;
		public string friend_name;
    }
}
//EXTEND_END
