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
    public float startingConfidence = 0;
    [HideInInspector] public float currentConfidence;
    [NonSerialized] public bool[] VignettesDone;

    [HideInInspector] public Vector3 lastPlayerPos = new(1.5f,4.5f,18f);

    private bool startedOnce;
    public Canvas canvas;

    private AudioManager _audioManager;
    
    public void Start()
    {
        lastPlayerPos = new Vector3(1.5f, 4.5f, 18f);
        VignettesDone = new []{ false, false, false, false, false, false};
        currentConfidence = startingConfidence;
        if (!startedOnce)
        {
            _audioManager = GetComponent<AudioManager>();
            _audioManager.Play("GameMusic");
            Debug.Log("Tocou musica");
            SceneManager.sceneLoaded += OnSceneLoaded;
            startedOnce = true;
        }
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
