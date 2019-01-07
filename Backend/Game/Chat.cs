//EXTEND_BEGIN
using UnityEngine;
using System;
using Common;
using System.Collections.Generic;

public class Chat : Singleton<Chat>
{
	public struct CommunicationRecord
	{
		public int sender_id;
		public int receiver_id;
		public string msg;
	}

	private Chat()
	{

	}

	public Dictionary<int, string> friend_list = new Dictionary<int, string>();
	public Dictionary<int, List<CommunicationRecord>> chatting_records = new Dictionary<int, List<CommunicationRecord>>();//Key=player1_id*1000+player2_id
	public Dictionary<int, int> records_visited = new Dictionary<int, int>();//Key=player1_id*1000+player2_id

	public Dictionary<int, M_Item> all_items = new Dictionary<int, M_Item>();
}
//EXTEND_BEGIN