using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HostOfLobby : MonoBehaviour
{
    public GameObject button;

    private PhotonView _photon;

    void Start ()
    {
        _photon = GetComponent<PhotonView>();
        Debug.Log(_photon);
        if (PhotonNetwork.isMasterClient)
        {
            button.SetActive(true);
        } else
        {
            button.SetActive(false);
        }
    }

}
