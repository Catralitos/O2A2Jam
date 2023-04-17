using System;

public class DialogueLine  
{
    public readonly string Name;
    public readonly string Content;
    public readonly int Pose;
    public readonly string Position;
    public string[] Options;

    public DialogueLine(string Name, string Content, int Pose, string Position) {
        this.Name = Name;
        this.Content = Content;
        this.Pose = Pose;
        this.Position = Position;
        Options = Array.Empty<string>();
    }
}