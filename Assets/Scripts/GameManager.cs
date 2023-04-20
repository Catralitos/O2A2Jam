using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region SingleTon

    /// <summary>
    /// Gets the sole instance.
    /// </summary>
    /// <value>
    /// The instance.
    /// </value>
    public static GameManager Instance { get; private set; }

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
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    public float confidenceIncrease;
    [HideInInspector] public float currentConfidence;
    [NonSerialized] public readonly bool[] VignettesDone = { false, false, false, false, false, false};

    [HideInInspector] public Vector3 lastPlayerPos = new(1.5f,4.5f,18f);

    public Canvas canvas;
    
    private void Start()
    {
        lastPlayerPos = new Vector3(1.5f, 4.5f, 18f);
        //canvas.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y, -0.1f);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        canvas.worldCamera = Camera.main;;
    }

    private void Update()
    {
        canvas.gameObject.SetActive(SceneManager.GetActiveScene().buildIndex is not (1 or 2));
    }

    public void IncreaseConfidence()
    {
        currentConfidence = Mathf.Clamp(currentConfidence + confidenceIncrease, 0.0f,
            currentConfidence + confidenceIncrease * 4);
    }

    public void DecreaseConfidence()
    {
        currentConfidence = Mathf.Clamp(currentConfidence - confidenceIncrease, 0.0f,
            currentConfidence + confidenceIncrease * 4);
    }
}
