using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
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

    public Text StoryContent;

    public bool isReady; // commented out = true
    public bool inCoroutine = false;

    public int nightCount = 0;

    private int pointInAnnouncement = 0; // used for incrementing announcement order
    private int storyGen = 0; // used for randomizing story

    private List<string> _announcements = new List<string>(); // actual items displayed on-screen

    private PhotonView _photon; 
    private float _buildDelay = 0f;
    private int _maximumAnnouncements = 10; // arbitrary

    // Start is called before the first frame update
    void Start()
    {
        _photon = GetComponent<PhotonView>(); // maybe used for role assignment?
        isReady = true;

    }

    [PunRPC]
    void RPC_AddNewAnnouncement(string announcement) // pass in incremented storypoint
    {
        _announcements.Add(announcement); // post-increment in c# ?
        // pointInAnnouncement++; //
    }

    public void SendAnnouncement()
    {
        string announcement = announcements[pointInAnnouncement];
        StartCoroutine(DelayCoroutine());
        // _photon.RPC("RPC_AddNewAnnouncement", PhotonTargets.All, announcement);
        RPC_AddNewAnnouncement(announcement);
    }

    // SEND STORY METHOD

    // public void SubmitAnnouncement() {} // may have to do some timing in here...

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
              
                StartCoroutine(DelayCoroutine());
                BuildAnnouncementContents();
                _buildDelay = Time.time + 0.25f;
            }
        }
        else if (_announcements.Count > 0)
        {
            _announcements.Clear();
            StoryContent.text = "";
        }

        if (nightCount == 0 && isReady) // probs need to change this later
        {
            isReady = false;
            SendAnnouncement();
            
        }

    }

    IEnumerator DelayCoroutine()
    {
        inCoroutine = true;
        yield return new WaitForSeconds(5);
        inCoroutine = false;
    }
}
