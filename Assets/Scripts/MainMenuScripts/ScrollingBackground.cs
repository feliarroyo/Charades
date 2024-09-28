using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private float x, y;

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(x,y) * Time.deltaTime, img.uvRect.size);
    }
}
