using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceButton : MonoBehaviour {

    [HideInInspector] public string option;
    [HideInInspector] public DialogueManager box;
    
    public void SetText(string newText) {
        GetComponentInChildren<TextMeshProUGUI> ().text = newText;
    }

    public void SetOption(string newOption) {
        option = newOption;
    }

    public void ParseOption() {
        string command = option.Split (',') [0];
        string commandModifier = option.Split (',') [1];
        box.playerTalking = false;
        switch (command)
        {
            case "line":
                box.lineNum = int.Parse(commandModifier);
                box.ShowDialogue();
                break;
            case "scene":
                SceneManager.LoadScene("Scene" + commandModifier);
                break;
        }
    }
}