using UnityEngine;
using UnityEngine.UI;

public class AltMc : Character
{
    public GameObject scratches;
    
    public void StartScratches()
    {
        scratches.SetActive(true);
    }

    public void StopScratches()
    {
        scratches.SetActive(false);
    }
}