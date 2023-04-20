using UnityEngine;
using UnityEngine.UI;

public class ScrollBackground : MonoBehaviour
{
    private RawImage _rawImage;
    public float scrollSpeed;
    public float maxX;
    public float minX;
    public bool scrollingLeft;

    private void Start()
    {
        _rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_rawImage.uvRect.position.x > maxX) scrollingLeft = true;
        if (_rawImage.uvRect.position.x < minX) scrollingLeft = false;
        
        if (scrollingLeft)
        {
            _rawImage.uvRect =
                new Rect(
                    _rawImage.uvRect.position + new Vector2(-scrollSpeed * Time.deltaTime, _rawImage.uvRect.position.y),
                    _rawImage.uvRect.size);  
        }
        else
        {
            _rawImage.uvRect =
                new Rect(
                    _rawImage.uvRect.position + new Vector2(scrollSpeed * Time.deltaTime, _rawImage.uvRect.position.y),
                    _rawImage.uvRect.size);  
        }
    }
}
