using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Scratches : MonoBehaviour
{
    [Header("Info")]
    private Vector3 _startPos;
    private Vector3 _randomPos;
 
    [Header("Settings")]
    public float distance = 0.1f;
    public float delayBetweenShakes = 0f;
 
    private void Awake()
    {
        _startPos = transform.position;
    }

    private void Start()
    {
        Begin();
    }

    private void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(Shake());
    }
 
    private IEnumerator Shake()
    {
 
        while (true)
        {
            _randomPos = _startPos + (Random.insideUnitSphere * distance);
 
            transform.position = _randomPos;
 
            if (delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }
    }
}