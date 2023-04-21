using System.Collections.Generic;
using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{
    public List<GameObject> vignetteStarters;

    private void Start()
    {
        bool[] vignettesDone = GameManager.Instance.VignettesDone;
        
        for (int j = 0; j < vignettesDone.Length; j++)
        {
            vignetteStarters[j].SetActive(!vignettesDone[j]);
        }

        Player.Instance.gameObject.transform.position = GameManager.Instance.lastPlayerPos;
    }
}
