using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ChatManager : MonoBehaviour
{
    //https ://www.youtube.com/watch?v=xDWpOtvE9q0

    public InputField ChatInput;
    public Text ChatContent;
    private PhotonView _photon;
    private List<string> _messages = new List<string>();
    private float _buildDelay = 0f;
    private int _maximumMessages = 14;


    void Start()
    {
        _photon = GetComponent<PhotonView>();
    }

    [PunRPC]
    void RPC_AddNewMessage(string msg)
    {
        _messages.Add(msg);
    }

    public void SendChat(string msg)
    {
        string NewMessage = PhotonNetwork.player + ":" + msg;
        _photon.RPC("RPC_AddNewMessage", PhotonTargets.All, NewMessage);
    }

    public void SubmitChat()
    {
        string blankCheck = ChatInput.text;
        blankCheck = Regex.Replace(blankCheck, @"\s", "");
        if (blankCheck == "")
        {
            ChatInput.ActivateInputField();
            ChatInput.text = "";
            return;
        }

        SendChat(ChatInput.text);
        ChatInput.ActivateInputField();
        ChatInput.text = "";

    }

    void BuildChatContents()
    {
        string NewContents = "";
        foreach (string s in _messages)
        {
            NewContents += s + "\n";
        }
        ChatContent.text = NewContents;
    }

    void Update()
    {
        if (PhotonNetwork.inRoom)
        {
          //  ChatContent.maxVisibleLines = _maximumMessages;

            if (_messages.Count > _maximumMessages)
            {
                _messages.RemoveAt(0);
            }
            if (_buildDelay < Time.time)
            {
                BuildChatContents();
                _buildDelay = Time.time + 0.25f;
            }
        } 
        else if (_messages.Count > 0)
        {
            _messages.Clear();
            ChatContent.text = "";
        }
        
    }
}
