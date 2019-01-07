using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

public class ChatMessage : Singleton<ChatMessage>
{
    public int user_id;
    public int friend_id;
    public int recv_msg_num;

    private ChatMessage()
    {
        user_id = 0;
        friend_id = 0;
        recv_msg_num = 0;
    }
    public Dictionary<int, string> friend_list = new Dictionary<int, string>();
}