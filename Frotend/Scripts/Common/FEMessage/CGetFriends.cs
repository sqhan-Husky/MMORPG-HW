//EXTEND_BEGIN
using System;

namespace Common
{
    [Serializable]
    public class CGetFriends : Message
    {
        public CGetFriends() : base(Command.C_GET_FRIENDS) { }
    }
}
//EXTEND_END
