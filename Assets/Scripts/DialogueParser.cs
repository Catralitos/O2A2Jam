using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class DialogueParser : MonoBehaviour
{
    #region SingleTon

    /// <summary>
    /// Gets the sole instance.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static DialogueParser Instance { get; private set; }

    /// <summary>
    /// Awakes this instance (if none already exists).
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    
    
    private List<DialogueLine> lines;
    private void Start () {
        GetFile();
    }

    public void GetFile()
    {
        string file = "Dialogue";
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        file += sceneNum;
        //file += ".txt";
        
        lines = new List<DialogueLine>();
        TextAsset ta = (TextAsset) Resources.Load(file, typeof(TextAsset));
        LoadDialogue (TextAssetToList(ta));
    }

    private static List<string> TextAssetToList(TextAsset ta)
    {
        string[] arrayString = ta.text.Split('\n');
        return arrayString.ToList();
    }

    
    private void LoadDialogue(List<string> fileContents) {
        if (lines.Count > 0) lines.Clear();

        foreach(string line in fileContents){
            if (line != null) {
                string[] lineData = line.Split(';');
                if (lineData[0] == "Choice") {
                    DialogueLine lineEntry = new DialogueLine(lineData[0], "", 0);
                    lineEntry.Options = new string[lineData.Length-1];
                    for (int i = 1; i < lineData.Length; i++) {
                        lineEntry.Options[i-1] = lineData[i];
                    }
                    lines.Add(lineEntry);
                } else {
                    DialogueLine lineEntry = new DialogueLine(lineData[0], lineData[1], int.Parse(lineData[2]));
                    lines.Add(lineEntry);
                }
            }
        }
    }
    
    public string GetName(int lineNumber)
    {
        return lineNumber < lines.Count ? lines[lineNumber].Name : "";
    }

    public string GetContent(int lineNumber)
    {
        return lineNumber < lines.Count ? lines[lineNumber].Content : "";
    }

    public int GetPose(int lineNumber)
    {
        return lineNumber < lines.Count ? lines[lineNumber].Pose : 0;
    }

    public string[] GetOptions(int lineNumber)
    {
        return lineNumber < lines.Count ? lines[lineNumber].Options : Array.Empty<string>();
    }
}