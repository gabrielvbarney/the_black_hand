using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Text;
using Random = System.Random;


public class PlayerFunctions : Photon.MonoBehaviour
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

    private List<int> playerRoleList = new List<int>();
    private string playerRoleString;

    public Text testText1;
    public Text testText2;

    //private string tempString;
    int[] roleArray = new int[10];
    //int[] tempArray = new int[10];

    // Start is called before the first frame update

    [PunRPC]
    public void RPC_setPlayerWindows(string photonPlayerString, int i) 
    {
        
        // playerText = photonPlayerString;
        // Debug.Log(playerText);
        playerTextList[i].text = photonPlayerString;
        // Debug.Log(playerText);
         
        // instantiate?
    }

    void Start()
    {
        _photon = GetComponent<PhotonView>();
        roleAnnouncement.gameObject.SetActive(false);
        PhotonNetwork.automaticallySyncScene = true;

        Dictionary<int, string> players = new Dictionary<int, string>();
        
        for (int i = 0; i < PhotonNetwork.playerList.Length; i++) 
        {
            players.Add(PhotonNetwork.playerList[i].ID, PhotonNetwork.playerList[i].name);
        }

        foreach (KeyValuePair<int, string> player in players)
        {
            _photon.RPC("RPC_setPlayerWindows", PhotonTargets.All, player.Value, player.Key - 1);
        }

        for (int i = players.Keys.Max(); i < 9; i++)
        {
            playerTextList[i].gameObject.SetActive(false);
        }

        assignRoles(players);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    
   /* public void voting()
    {

    }*/


    public void assignRoles(Dictionary<int, string> players)
    {
        int i = 0;
        foreach (KeyValuePair<int, string> player in players) // only touches active players
        {
           
            // Key - 1 because keys are not 0 indexed
            if (player.Key - 1 == 0)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Mafia";
                playerTextList[i].transform.gameObject.tag = "Mafia";
            }
            else if (player.Key - 1 == 1)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Doctor";
                playerTextList[i].transform.gameObject.tag = "Doctor";
            }
            else if (player.Key - 1 == 2)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Sheriff";
                playerTextList[i].transform.gameObject.tag = "Sheriff";
            }
            else
            {
                playerTextList[i].transform.parent.gameObject.tag = "Common";
                playerTextList[i].transform.gameObject.tag = "Common";
            }
            i++;

        }
        //sendRoleAnnouncement();
    }


    [PunRPC]
    public void sendRoleAnnouncement()
    {

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            if (playerTextList[i].gameObject.tag == "Mafia")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are a member of the Mafia.";
            }
            else if (playerTextList[i].gameObject.tag == "Doctor")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are the Doctor.";
            }
            else if (playerTextList[i].gameObject.tag == "Sheriff")
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are the Sheriff.";
            }
            else
            {
                roleAnnouncement.gameObject.SetActive(true);
                roleAnnouncement.text = "You are hiding from the Mafia.";
            }
            roleAnnouncement.gameObject.SetActive(true);

        }

    }








    /* REAL ROLE ASSIGNMENT, DO LATER, PROBLEMS WITH RANDOMIZING, DO GENERAL RN FOR DEMO 
      
    [PunRPC]
    public void RPC_receiveRoles(int[] _arr)
    {

        roleArray = _arr;
    }
    [PunRPC]
    public void assignRoles()
    {

        List<int> roleIndicators = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        roleIndicators = roleIndicators.Take(PhotonNetwork.playerList.Length).ToList();
        roleIndicators = roleIndicators.OrderBy(item => rng.Next()).ToList();

        for (int i = 0; i < roleIndicators.Count; i++)
        {
            _photon.RPC("RPC_receiveRoles", PhotonTargets.All, i, roleIndicators)
        }
        
        List<char> hostRoles = new List<char>();
        List<char> roleIndicatorsChar = new List<char>();

        // 0 = mafia, 1 = doctor, 2 = sheriff, 3-8 = commoners

        // LATER CHANGE MIN PLAYERS TO 4
        
        for (int i = 0; i < roleIndicators.Count; i++)
        {
           tempArray[i] = (int)roleIndicators[i];
        } 
             

        _photon.RPC("RPC_receiveRoles", PhotonTargets.All, tempArray);

        testText1.text = (PhotonNetwork.playerList[0].name);
        testText2.text = (PhotonNetwork.playerList[1].name);

        // do new function here maybe?\
        // also, need to check for 4 or more players, and mafia, sheriff, dr all to be assigned

        for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            // GOING TO RENAME THE LIST USED HERE
            // Debug.Log(roleIndicators[0]);
            if (roleArray[i] == 0)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Mafia";
                playerTextList[i].transform.gameObject.tag = "Mafia";
            } else if (roleArray[i] == 1)
            {
                playerTextList[i].transform.parent.gameObject.tag = "Doctor";
                playerTextList[i].transform.gameObject.tag = "Doctor";
            } else if (roleArray[i] == 2)
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
    */



}
