using UnityEngine;
using System.Collections;
using VIDE_Data; //Connecting to the VD class to access the node data
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject container_NPC;
    public GameObject NPC;
    public GameObject container_PLAYER;
    public GameObject Text;
    public Text text_NPC;
    public GameObject Player;
    public Text[] text_Choices;
    public AudioSource audioSource;
    bool isLocked = true;
    public bool isActive;
    public bool isExit;
    bool isPlayerTime;

    // Use this for initialization
    void Start()
    {
        var NPC_cont = container_NPC;
        var Player_cont = container_PLAYER;
        NPC_cont.active = false;
        Player_cont.active = false;
        VD.LoadDialogues();
        isPlayerTime = false;
        Player.GetComponentInChildren<FirstPersonLookInDialogue>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isExit)
        {
            End(null);
        }
            if (Input.GetKeyDown(KeyCode.E))
            {
            Text.SetActive(false);
            isActive = true;
            Player.transform.position = new Vector3(161.9839f, 36.3028f, 458.1215f);
            Player.GetComponentInChildren<FirstPersonLook>().enabled = false;
            Player.GetComponentInChildren<FirstPersonLookInDialogue>().enabled = true;
            //Player.GetComponent<PersonControllerScript>().enabled = false;
            //Player.GetComponent<FirstPersonMovement>().enabled = false;
            //Player.GetComponent<Animator>().enabled = false;
            if (Input.GetKeyDown(KeyCode.E) && isLocked == true)
                    SetCursorLock(!isLocked);
                if (!VD.isActive)
                {
                    Begin();
                }
            }
            if (VD.isActive)
            {
                if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return)) && isPlayerTime == false)
                {
                    VD.Next();
                }
            }
        
    }
    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;
    }

    void UpdateUI(VD.NodeData data)
    {
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        if (data.isPlayer)
        {
            container_PLAYER.SetActive(true);
            isPlayerTime = true;

            for (int i = 0; i < text_Choices.Length; i++)
            {
                if (i < data.comments.Length)
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(true);
                    text_Choices[i].text = data.comments[i];
                }
                else
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            container_NPC.SetActive(true);
            isPlayerTime = false;
            text_NPC.text = data.comments[data.commentIndex];
            //Play Audio if any
            /* if (data.audios[data.commentIndex] != null)
                audioSource.clip = data.audios[data.commentIndex];
                
            audioSource.Play();
            */
        }
    }

    void End(VD.NodeData data)
    {
        SetCursorLock(true);
        container_NPC.SetActive(false);
        container_PLAYER.SetActive(false);
        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
        NPC.GetComponent<NPC_triggered>().EndDialog = true;
        //Player.GetComponent<PersonControllerScript>().enabled = true;
        //Player.GetComponent<FirstPersonMovement>().enabled = true;
        Player.GetComponentInChildren<FirstPersonLook>().enabled = true;
        Player.GetComponentInChildren<FirstPersonLookInDialogue>().enabled = false;
        isActive = false;
        //Player.GetComponent<Animator>().enabled = true;
    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Return)) && isPlayerTime == true)
        {
            VD.Next();
        }
    }

    void OnDisable()
    {
        if (container_NPC != null)
            End(null);
    }
}