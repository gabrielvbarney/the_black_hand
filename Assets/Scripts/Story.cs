using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : Photon.PunBehaviour
{

    List<string> announcements = new List<string>()
    {
        "> Welcome to the game. You will be assigned roles come nightfall. Best of luck to you all."
    };

    // str manipulation ex: ("{0}", name)
    List<string> stories = new List<string>()
    {
        "Story: As {0} enjoyed a cigar and whiskey before bed, the mafia was already converging on their room. The ice melted within their drink, and the cigar slowly became unlit. Il lupo perde il pelo ma non il vizio."


    };

    // public PlayerFunctions pf = new PlayerFunctions();

    // PlayerFunctions pf = new PlayerFunctions();
    

    public Text StoryContent;
    private bool gameOver = false; // may need to do separate method for game flow: while gameOver == false :

    public bool isReady; // commented out = true
    public bool inCoroutine = false;

    public int nightCount = 0;

    private int pointInAnnouncement = 0; // used for incrementing announcement order
    private int storyGen = 0; // used for randomizing story

    private List<string> _announcements = new List<string>(); // actual items displayed on-screen

    private PhotonView _photon;
    private float _buildDelay = 0f;
    private int _maximumAnnouncements = 10; // arbitrary

    public Text countDown;

    // Start is called before the first frame update
    void Start()
    {
        // will need to instantiate player stuff in here
        
        _photon = GetComponent<PhotonView>(); // maybe used for role assignment?
        StartCoroutine(DelayCoroutineNightZero());
       
    }

    [PunRPC]
    void RPC_AddNewAnnouncement(string announcement) // pass in incremented storypoint
    {
        _announcements.Add(announcement); // post-increment in c# ?
        pointInAnnouncement++; 
        isReady = true;
    }

    public void SendAnnouncement()
    {
        string announcement = announcements[pointInAnnouncement];
        RPC_AddNewAnnouncement(announcement);
    }

    // SEND STORY METHOD

    void BuildAnnouncementContents() 
    {
        string NewContents = "";
        foreach (string s in _announcements)
        {
            NewContents += s + "\n";
        }
        StoryContent.text = NewContents;
    
    } // may be unneeded due to pregenerated announcements/stories


    // Update is called once per frame
    void Update()
    {
        // Also probably used for story stuff
        // night counter int for keeping track? int night_count = 0;
        // can use story updates to increment and then time everything

        // cuento notes: scheduler, different classes for each night?
        // mine: have to organize really well due to potential of lots of different cases
        // check out cuentos game on github: coup d'tat
        if (PhotonNetwork.inRoom)
        {

            if (_announcements.Count > _maximumAnnouncements)
            {
                _announcements.RemoveAt(0);
            }
            if (_buildDelay < Time.time && !inCoroutine)
            {
              
                // StartCoroutine(DelayCoroutine5()); //maybe dont need?
                BuildAnnouncementContents();
                _buildDelay = Time.time + 0.25f;
            }
        }
        else if (_announcements.Count > 0)
        {
            _announcements.Clear();
            StoryContent.text = "";
        }
       // Debug.Log(pointInAnnouncement);
    }

    IEnumerator DelayCoroutineNightZero()
    {
        
        yield return new WaitForSeconds(5);
        SendAnnouncement();
        
        yield return new WaitForSeconds(5);
        countDown.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.text = "4";

        yield return new WaitForSeconds(1);
        countDown.text = "3";

        yield return new WaitForSeconds(1);
        countDown.text = "2";

        yield return new WaitForSeconds(1);
        countDown.text = "1";
       // pf.assignRoles(); // may need to import class?

    }

    IEnumerator Delay1Second()
    {
        inCoroutine = true;
        yield return new WaitForSecondsRealtime(1);
        inCoroutine = false;
    }

    
}
