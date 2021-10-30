using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Random = System.Random;


public class PlayerFunctions : Photon.PunBehaviour
{
    public Text p1;
    public Text p2;
    public Text p3;
    public Text p4;
    public Text p5;
    public Text p6;
    public Text p7;
    public Text p8;
    public Text p9;

    /// TODO: BUG WITH HOW PLAYERS ARE DISPLAYED IN HOUSE, BUG TO FIX LATER

    public static Random rng = new Random();

    public List<Text> playerTextList = new List<Text> { }; // populated in unity

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.automaticallySyncScene = true;

        int p = 0;
       
        for (int i = 0; i < 9; i++)
        {
            if (i < (PhotonNetwork.playerList.Length))
            {
                playerTextList[i].text = PhotonNetwork.playerList[p++].name;
            } else
            {
                // playerTextList[i] = null;
                playerTextList[i].gameObject.SetActive(false);
            }
        }

        // add cover to all buttons that have the text of " "

        assignRoles();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // added static
    public void assignRoles()
    {
        
        List<int> roleIndicators = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        
        // 0 = mafia, 1 = doctor, 2 = sheriff, 3-8 = commoners
        // LATER CHANGE MIN PLAYERS TO 4

        
        roleIndicators = roleIndicators.Take(PhotonNetwork.playerList.Length).ToList();
        roleIndicators = roleIndicators.OrderBy(item => rng.Next()).ToList();

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            Debug.Log(roleIndicators[0]);
            if (roleIndicators[i] == 0)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Mafia";
            } else if (roleIndicators[i] == 1)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Doctor";
            } else if (roleIndicators[i] == 2)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Sheriff";
            } else
            {
                playerTextList[i].transform.parent.gameObject.tag = "Common";
            }
        }
       

      
        

        // Debug.Log(roleIndicators.Take(4).ToList().Count);
    }

    
    // slice for as many characters that are in the game GOOD
    // randomize list GOOD
    // cross reference this list and player names list and assign roles as indicated above
    // give tag to user instance
    // send announcement to screen saying what their role is.
}
