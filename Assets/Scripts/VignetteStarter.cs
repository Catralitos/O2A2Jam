using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VignetteStarter : MonoBehaviour
{
    [Range(1, 6)] public int vignetteNumber;  
    public int sceneIndex;
    public LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (playerLayer.HasLayer(col.gameObject.layer))
        {
            GameManager.Instance.VignettesDone[vignetteNumber - 1] = true;
            GameManager.Instance.lastPlayerPos = Player.Instance.transform.position;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
