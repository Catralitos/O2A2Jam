using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
        
    private DialogueParser parser;

    [HideInInspector] public string dialogue, characterName;
    [HideInInspector] public int lineNum;
    private int pose;
    private string[] options;
    [HideInInspector] public bool playerTalking;
    private readonly List<ChoiceButton> buttons = new List<ChoiceButton> ();
    public List<Character> characters = new List<Character>();
    
    public TextMeshProUGUI dialogueBox;
    public TextMeshProUGUI nameBox;
    public GameObject choiceBoxHolder;
    public GameObject choiceBoxPrefab;
    public GameObject dialogueBoxParent;
    public GameObject nameBoxParent;
    
    private void Start () {
        parser = DialogueParser.Instance;
        parser.GetFile();
        dialogue = "";
        characterName = "";
        pose = 0;
        playerTalking = false;
        parser = DialogueParser.Instance;
        lineNum = 0;
        
        ShowDialogue();
        lineNum++;
        UpdateUI ();
    }

    private void Update () {
        if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown(KeyCode.Space))&& playerTalking == false) {
            ShowDialogue();

            lineNum++;
        }
        UpdateUI ();
    }

    public void ShowDialogue() {
        ParseLine ();
    }

    private void ParseLine() {
        if (parser.GetName(lineNum) == "Next Scene")
        {
            pose = parser.GetPose(lineNum);
            SceneManager.LoadScene(pose);
        }
        else if (parser.GetName(lineNum) == "Narrator")
        {
            playerTalking = false;
            characterName = "";
            dialogue = parser.GetContent (lineNum);
            pose = parser.GetPose (lineNum);
            DisplayImages();
        }
        else if (parser.GetName (lineNum) == "Choice") {
            playerTalking = true;
            characterName = "";
            dialogue = "";
            pose = 0;
            options = parser.GetOptions(lineNum);
            CreateButtons();
        } 
        else {
            playerTalking = false;
            characterName = parser.GetName (lineNum);
            dialogue = parser.GetContent (lineNum);
            pose = parser.GetPose (lineNum);
            DisplayImages();
        }
    }

    private void DisplayImages() {
        if (characterName != "?????") {
            
            Character character = characters.Find(c => c.characterName == "Ava");

            Image currSprite = character.gameObject.GetComponent<Image>();
            currSprite.sprite = character.characterPoses[pose];
        }
        else {
            
            AltMc altMc = (AltMc)characters.Find(c => c.characterName == "?????");
            Image currSprite1 = altMc.gameObject.GetComponent<Image>();
            currSprite1.sprite = altMc.characterPoses[pose];
            
            if (pose != altMc.characterPoses.Length - 1)
            {
                altMc.StartScratches();
            }
            else
            {
                altMc.StopScratches();
            }
            
            Character character = characters.Find(c => c.characterName == "Ava");

            Image currSprite2 = character.gameObject.GetComponent<Image>();
            currSprite2.sprite = character.characterPoses[pose];
        }
    }

    private void CreateButtons()
    {
        dialogueBoxParent.SetActive(false);
        foreach (string t in options)
        {
            GameObject button = Instantiate(choiceBoxPrefab, choiceBoxPrefab.transform.position, Quaternion.identity, choiceBoxHolder.transform);
            ChoiceButton cb = button.GetComponent<ChoiceButton>();
            cb.SetText(t.Split(':')[0]);
            cb.option = t.Split(':')[1];
            cb.box = this;
            buttons.Add (cb);
        }
    }

    private void UpdateUI() {
        if (!playerTalking) {
            dialogueBoxParent.SetActive(true);
            ClearButtons();
        }
        dialogueBox.text = dialogue;
        nameBox.text = characterName.ToUpper();
        nameBoxParent.SetActive(characterName != "");
    }

    private void ClearButtons() {
        for (int i = 0; i < buttons.Count; i++) {
            print ("Clearing buttons");
            ChoiceButton b = buttons[i];
            buttons.Remove(b);
            Destroy(b.gameObject);
        }
    }
}