using System;

public class DialogueLine  
{
    public readonly string Name;
    public readonly string Content;
    public readonly int Pose;
    public string[] Options;

    public DialogueLine(string Name, string Content, int Pose) {
        this.Name = Name;
        this.Content = Content;
        this.Pose = Pose;
        Options = Array.Empty<string>();
    }
}