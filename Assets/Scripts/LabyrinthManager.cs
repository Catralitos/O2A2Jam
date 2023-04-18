using System.Collections.Generic;
using UnityEngine;

public class LabyrinthManager : MonoBehaviour
{
    public List<GameObject> vignetteStarters;

    private void Start()
    {
        bool[] vignettesDone = GameManager.Instance.VignettesDone;
        int index = 0;
        for (int i = 0; i < vignettesDone.Length; i++)
        {
            if (!vignettesDone[i])
            {
                index = i;
                break;
            }
        }

        for (int j = 0; j < vignettesDone.Length; j++)
        {
            vignetteStarters[j].SetActive(j == index);
        }

        Player.Instance.transform.position = GameManager.Instance.lastPlayerPos;
    }
}
