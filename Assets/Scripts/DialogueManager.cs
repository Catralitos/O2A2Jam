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

    public int nextSceneIndex;
    
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
        if (Input.GetMouseButtonDown (0) && playerTalking == false) {
            ShowDialogue();

            lineNum++;
        }
        UpdateUI ();
    }

    public void ShowDialogue() {
        ParseLine ();
    }

    private void ParseLine() {
        if (parser.GetName(lineNum) == "")
        {
            SceneManager.LoadScene(nextSceneIndex);
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
        if (characterName != "") {
            
            Character character = characters.Find(c => c.characterName == characterName);
            
            Image currSprite = character.gameObject.GetComponent<Image>();
            currSprite.sprite = character.characterPoses[pose];
        }
    }

    private void CreateButtons()
    {
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
            ClearButtons();
        }
        dialogueBox.text = dialogue;
        nameBox.text = characterName;
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