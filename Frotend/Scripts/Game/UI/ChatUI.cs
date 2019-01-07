using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gamekit3D;

using System.Net;
using System.Net.Sockets;
using Common;
using Gamekit3D.Network;
using System;


public class ChatUI : MonoBehaviour
{
    public GameObject messageView;
    public GameObject myMessage;
    public GameObject friendMessage;

    public int msgNum=0;

    // my message info content layout | ----------------- message text | image |
    // friend's info content layout   | image | message text ----------------- |


    private void Awake()
    {
        myMessage.SetActive(false);
        friendMessage.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        ReceiveMessageFromFriend();
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
        //EXTEND_BEGIN
        ReceiveMessageFromFriend();
        //EXTEND_END
    }

    //EXTEND_BEGIN
    private void ReceiveMessageFromFriend()
    {
        CRecvMessage recv_msg = new CRecvMessage();
        recv_msg.sender_id = ChatMessage.Instance.friend_id;
        recv_msg.receiver_id = ChatMessage.Instance.user_id;
		//Debug.Log(recv_msg.sender_id);
		//Debug.Log(recv_msg.receiver_id);
		recv_msg.msg_num = msgNum;
        Client.Instance.Send(recv_msg);
    }

    private void SendMessageToFriend(string msg)
    {
        CSendMessage send_msg = new CSendMessage();
        send_msg.sender_id = ChatMessage.Instance.user_id;
        send_msg.receiver_id = ChatMessage.Instance.friend_id;
        send_msg.msg = msg;
        Client.Instance.Send(send_msg);
    }
    //EXTEND_END

    public void ReceiveFriendMessage(string text)
    {
        if (friendMessage == null)
            return;

        GameObject cloned = GameObject.Instantiate(friendMessage);
        if (cloned == null)
            return;
        cloned.SetActive(true);
        AddElement(cloned, text);
    }

    public void SendMyMessage(string text)
    {
        if (myMessage == null)
            return;

        GameObject cloned = GameObject.Instantiate(myMessage);
        if (cloned == null)
            return;
        cloned.SetActive(true);
        AddElement(cloned, text);
    }

    public void OnSendButtonClick(GameObject inputField)
    {
        InputField input = inputField.GetComponent<InputField>();
        if (input == null)
            return;

        if (input.text.Trim().Length == 0)
            return;

        //EXTEND_BEGIN
        SendMessageToFriend(input.text);
        //EXTEND_END

        input.text = "";
    }

    void AddElement(GameObject element, string text)
    {
        TextMeshProUGUI textMesh = element.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
            return;
        //float width = textMesh.GetPreferredValues().x; // get preferred width before assign text
        textMesh.text = text;
        RectTransform rectTransform = element.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;

        RectTransform parentRect = this.GetComponent<RectTransform>();
        if (parentRect == null)
            return;

        if (textMesh.preferredWidth < parentRect.rect.width)
        {
            ContentSizeFitter filter = textMesh.GetComponent<ContentSizeFitter>();
            if (filter != null)
            {
                filter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                textMesh.rectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            }
        }

        element.transform.SetParent(this.transform, false);

        ScrollRect scrollRect = messageView.GetComponent<ScrollRect>();
        if (scrollRect == null)
            return;

        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    /*
    void Test()
    {
        //AddNewMessage(true, "my message send");
        //AddNewMessage(false, "friend message receive");

        SendMyMessage("hello");
        ReceiveFriendMessage("hello");
    }

    void AddNewMessage(bool mine, string message)
    {
        GameObject newContent = GameObject.Instantiate(content);
        if (newContent == null)
            return;
        GameObject newImage = GameObject.Instantiate(image);
        if (newImage == null)
            return;
        GameObject newText = GameObject.Instantiate(text);
        if (newText == null)
            return;

        HorizontalLayoutGroup layout = newContent.GetComponent<HorizontalLayoutGroup>();
        if (mine)
            layout.childAlignment = TextAnchor.UpperRight;
        else
            layout.childAlignment = TextAnchor.UpperLeft;

        TextMeshProUGUI textMesh = text.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
            return;

        //float width = textMesh.GetPreferredValues().x; // get preferred width before assign text
        textMesh.text = message;
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;

        RectTransform viewRect = messageContent.GetComponent<RectTransform>();
        if (viewRect == null)
            return;

        if (textMesh.preferredWidth < viewRect.rect.width)
        {
            ContentSizeFitter filter = textMesh.GetComponent<ContentSizeFitter>();
            if (filter != null)
            {
                filter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                rectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            }
        }

        newImage.transform.SetParent(newContent.transform, false);
        newText.transform.SetParent(newContent.transform, false);
        newContent.transform.SetParent(messageContent.transform, false);
    }
    */
}
