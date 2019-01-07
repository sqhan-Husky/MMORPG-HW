//EXTEND_BEGIN
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class SGetFriends : Message
    {
        public SGetFriends() : base(Command.S_GET_FRIENDS) {
            players = new List<int>();
            names = new List<string>();
        }
        public List<int> players;
        public List<string> names;
        public int player_id;
    }
}
//EXTEND_END
