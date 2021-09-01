using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setUsernames : Photon.MonoBehaviour
{
    public Text Username;
    public PhotonView photonView;
    
    // Start is called before the first frame update
   // void Start()
    private void Awake()
    {
      if (photonView.isMine) {
          Username.text = PhotonNetwork.playerName;
      }
      else
      {
          Username.text = photonView.owner.name;
      }
      
    }

    
}
