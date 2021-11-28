using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : Photon.PunBehaviour
{

    List<string> announcements = new List<string>()
    {
        "> Welcome to the game. You will be assigned roles come nightfall. Best of luck to you all.",
        "> Each of you now know who you are. Stay safe.",
        "> Mafia, choose your victim for the night. Doctor, choose who you wish to insure safety. Sheriff, choose who you wish to seek justice upon. The rest of you, hope for the best.",
        "> Day is approaching, finish making your decisions."
   
    };

    // str manipulation ex: ("{0}", name)
    List<string> stories = new List<string>()
    {
        "> Story: As {0} enjoyed a cigar and whiskey before bed, the mafia was already converging on their room. The ice melted within their drink, and the cigar slowly became unlit. Il lupo perde il pelo ma non il vizio."


    };

    public string mafiaAlternate = "> The mafia did not choose to kill anyone tonight. Is this a dream?";
    public string doctorAlternate = "> The doctor did not choose to save anyone tonight. An instance of malpractice?";
    public string sheriffAlternate = "> The sheriff did not choose to seek justice tonight. Crooked cop? Or no suspicious activity detected?";

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
    public Text nightIndicator;

    public PlayerFunctions pf;

    public static bool nightTime = false;
    public static bool voteTime = false;

    // Start is called before the first frame update
    void Start()
    {
      
        _photon = GetComponent<PhotonView>(); 
        StartCoroutine(DelayCoroutineNightZero()); // need to call next coroutines from prior coroutine
       
       
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

        yield return new WaitForSeconds(1);
        countDown.gameObject.SetActive(false);

        pf.sendRoleAnnouncement(); // may need to import class?

        // now counting until night fall
        yield return new WaitForSeconds(3);
        SendAnnouncement();
        yield return new WaitForSeconds(3);

        countDown.text = "5";
        countDown.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);
        countDown.text = "4";

        yield return new WaitForSeconds(1);
        countDown.text = "3";

        yield return new WaitForSeconds(1);
        countDown.text = "2";

        yield return new WaitForSeconds(1);
        countDown.text = "1";

        yield return new WaitForSeconds(1);
        countDown.gameObject.SetActive(false);
        nightIndicator.text = "Night.";
        nightIndicator.gameObject.SetActive(true);

        // night 1
        nightTime = true; // used for voting funcs
        yield return new WaitForSeconds(3);
        SendAnnouncement(); // Tell mafia and dr and sheriff to do stuff
        StartCoroutine(DelayCoroutine20Seconds());
        yield return new WaitForSeconds(20);
        nightTime = false;
        nightIndicator.gameObject.SetActive(false);
        countDown.gameObject.SetActive(false);




    }

    IEnumerator DelayCoroutine20Seconds()
    {
        // call voting function here (while Story.nightTime)
        countDown.text = "20";
        countDown.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        countDown.text = "19";
        yield return new WaitForSeconds(1);
        countDown.text = "18";
        yield return new WaitForSeconds(1);
        countDown.text = "17";
        yield return new WaitForSeconds(1);
        countDown.text = "16";
        yield return new WaitForSeconds(1);
        countDown.text = "15";
        yield return new WaitForSeconds(1);
        countDown.text = "14";
        yield return new WaitForSeconds(1);
        countDown.text = "13";
        yield return new WaitForSeconds(1);
        countDown.text = "12";
        yield return new WaitForSeconds(1);
        countDown.text = "11";
        yield return new WaitForSeconds(1);
        countDown.text = "10";
        yield return new WaitForSeconds(1);
        SendAnnouncement();
        countDown.text = "9";
        yield return new WaitForSeconds(1);
        countDown.text = "8";
        yield return new WaitForSeconds(1);
        countDown.text = "7";
        yield return new WaitForSeconds(1);
        countDown.text = "6";
        yield return new WaitForSeconds(1);
        countDown.text = "5";
        yield return new WaitForSeconds(1);
        countDown.text = "4";
        yield return new WaitForSeconds(1);
        countDown.text = "3";
        yield return new WaitForSeconds(1);
        countDown.text = "2";
        yield return new WaitForSeconds(1);
        countDown.text = "1";
        yield return new WaitForSeconds(1);
    }


    IEnumerator Delay1Second()
    {
        inCoroutine = true;
        yield return new WaitForSecondsRealtime(1);
        inCoroutine = false;
    }

    
}
