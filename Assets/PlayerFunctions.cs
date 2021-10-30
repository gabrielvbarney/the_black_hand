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

    public Text roleAnnouncement;

    /// TODO: BUG: WITH HOW PLAYERS ARE DISPLAYED IN HOUSE, BUG TO FIX LATER

    public static Random rng = new Random();

    public List<Text> playerTextList = new List<Text> { }; // populated in unity
    private PhotonView _photon;

    // Start is called before the first frame update
    [PunRPC]
    void Start()
    {
        _photon = GetComponent<PhotonView>();
        roleAnnouncement.gameObject.SetActive(false);
        PhotonNetwork.automaticallySyncScene = true;

        int p = 0;

        //if (PhotonNetwork.isMasterClient)
        //{
            for (int i = 0; i < 9; i++)
            {
                if (i < (PhotonNetwork.playerList.Length))
                {
                    playerTextList[i].text = PhotonNetwork.playerList[p++].name;
                }
                else
                {
                    // playerTextList[i] = null;
                    playerTextList[i].gameObject.SetActive(false);
                }
            }


            // add cover to all buttons that have the text of " "

            assignRoles();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    [PunRPC]
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
                playerTextList[i].transform.gameObject.tag = "Mafia";
            } else if (roleIndicators[i] == 1)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Doctor";
                playerTextList[i].transform.gameObject.tag = "Doctor";
            } else if (roleIndicators[i] == 2)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Sheriff";
                playerTextList[i].transform.gameObject.tag = "Sheriff";
            } else
            {
                playerTextList[i].transform.parent.gameObject.tag = "Common";
                playerTextList[i].transform.gameObject.tag = "Common";
            }
            
        }

 
    }

    // next one may have to do with photon view
    
    [PunRPC]
    public void sendRoleAnnouncement()
    {
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (playerTextList[i].gameObject.tag == "Mafia")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are a member of the Mafia.";
            } else if (playerTextList[i].gameObject.tag == "Doctor")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are the Doctor.";
            }
            else if (playerTextList[i].gameObject.tag == "Sheriff")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are the Sheriff.";
            } else
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are hiding from the Mafia.";
            }
            roleAnnouncement.gameObject.SetActive(true);

        }
        // roleAnnouncement.gameObject.SetActive(true);

    }
    

    
    // slice for as many characters that are in the game GOOD
    // randomize list GOOD
    // cross reference this list and player names list and assign roles as indicated above GGOD
    // give tag to user instance GOOD
    // send announcement to screen saying what their role is.
}
