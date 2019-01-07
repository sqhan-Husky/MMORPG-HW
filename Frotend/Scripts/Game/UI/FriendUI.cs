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

public class FriendUI : MonoBehaviour
{
    public GameObject FriendInfo;

    private void Awake()
    {
        FriendInfo.GetComponentInChildren<UnityEngine.UI.Text>().text=" ";
        FriendInfo.SetActive(false);
    }
    // Use this for initialization

    void Start()
    {

    }

    private void OnEnable()
    {
        GetFriends();
        PlayerMyController.Instance.EnabledWindowCount++;
    }

    private void OnDisable()
    {
        /*GameObject FriendInfoCloned = GameObject.Instantiate(FriendInfo);
        foreach (Transform tf in this.transform)
        {
            Destroy(tf.gameObject);
        }*/
        /*
        foreach (Transform tf in this.transform)
        {
            if(!tf.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text.Equals(" "))
                Destroy(tf.gameObject);
        }*/
       //FriendInfo = GameObject.Instantiate(FriendInfoCloned);
       PlayerMyController.Instance.EnabledWindowCount--;
    }

    // Update is called once per frame
    void Update()
    {
        //GetFriends();
    }

    //EXTEND_BEGIN
    public void GetFriends()
    {
        // TODO read from last savepoint
        // read from database
        //Debug.Log("GetFriends");
        CGetFriends friendlist = new CGetFriends();
        Client.Instance.Send(friendlist);
    }

    public void ShowFriendList()
    {
		int frnum = 0;
        foreach (KeyValuePair<int, string> kvp in ChatMessage.Instance.friend_list)
        {
			if (!PlayerAttribute.user.Equals(kvp.Value))
			{
				++frnum;
				NewFriend(kvp.Value);
			}
        }
		var nbv = GameObject.Find("NobodyView");

		if (nbv != null)
		{ 
			if (frnum != 0)
			{
				nbv.SetActive(false);
			}
			else
			{
				nbv.SetActive(true);
			}
		}
	}

    void NewFriend(string name)
    {
        GameObject cloned = GameObject.Instantiate(FriendInfo);
        cloned.transform.SetParent(transform, false);
        cloned.SetActive(true);
        var new_friend = cloned.GetComponentInChildren<UnityEngine.UI.Text>();
        new_friend.text = name;
    }

    void Close()
    {

    }
    //EXTEND_END

    /*
    void Test()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject cloned = GameObject.Instantiate(FriendInfo);
            cloned.transform.SetParent(transform, false);
            cloned.SetActive(true);
        }
    }
    */
}
