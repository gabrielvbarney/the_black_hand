using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void playToFind() {
        SceneManager.LoadScene("FindCreateGame");
    }

    public void toLobby() {
        SceneManager.LoadScene("Lobby");
    }
}
