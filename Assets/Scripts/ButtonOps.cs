using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonOps : Photon.PunBehaviour //MonoBehaviour
{

    private PhotonView _photon;

    void Start()
    {
        _photon = GetComponent<PhotonView>();
    }


    public void OnClicked(Button button)
    {

        // PhotonNetwork.player gets person who pressed the button

        Dictionary<string, string> ksjResults = new Dictionary<string, string>();
        //if (Story.nightTime) // killing, saving, justice
        //{
            // https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
            if (button.tag == "Mafia")
            {
                Debug.Log("Mafia");
                Debug.Log(PhotonNetwork.player);
                 
            }
            else if (button.tag == "Doctor")
            {
                Debug.Log("Doctor");
                Debug.Log(PhotonNetwork.player);
        }
            else if (button.tag == "Sheriff")
            {
                Debug.Log("Sheriff");
            }
            else
            {
                Debug.Log("Common");
            }
            
        //} 
        //else if (Story.voteTime)
        //{
            // voting for players
        //}
    }
}
