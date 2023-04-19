using System;
using UnityEngine;

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

    [HideInInspector] public Vector3 lastPlayerPos = new(1.5f,4.5f,20f);

    private void Start()
    {
        lastPlayerPos = new Vector3(1.5f, 4.5f, 20f);
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
