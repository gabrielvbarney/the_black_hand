using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;



public class SceneSwitcher : Photon.PunBehaviour // MonoBehaviour
{
    public void playToFind() {
        SceneManager.LoadScene("HostJoinScreen");
    }

    public void toLobby() {
        SceneManager.LoadScene("Lobby");
    }

    public void toGame() {
        // SceneManager.LoadScene("GameView");
        PhotonNetwork.LoadLevel(2); // game view
    }
}
