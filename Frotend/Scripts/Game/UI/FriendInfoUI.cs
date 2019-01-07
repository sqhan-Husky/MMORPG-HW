//EXTEND_BEGIN
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit3D;

using System;
using Common;
using Gamekit3D.Network;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class FriendInfoUI : MonoBehaviour
{
    private void Awake()
    {

    }
    // Use this for initialization

    void Start()
    {

    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
    }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetFriendName()
    {
        string friend_name = gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text;
        foreach (KeyValuePair<int, string> kvp in ChatMessage.Instance.friend_list)
        {
            if (friend_name.Equals(kvp.Value))
            {
                ChatMessage.Instance.friend_id = kvp.Key;
                break;
            }
        }
    }
}
//EXTEND_END
