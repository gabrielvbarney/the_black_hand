using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class incrementPlayerCount : Photon.PunBehaviour
{
    public Text playerCount;
    public int Value = 0;
    public PhotonView photonView;

    private void Awake()
    {
        
        ++Value;
        playerCount.text = Value.ToString();

    }

    private void Update()
    {
        Value = PhotonNetwork.playerList.Length;
        playerCount.text = Value.ToString();
    }

    private void OnJoinedRoom()
    {
        ++Value;
        playerCount.text = Value.ToString();
    }

}
