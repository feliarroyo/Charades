using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    public LocalizedSprite localizedSprite;
    private Image image;
    private Sprite lastValidSprite;

    void Awake()
    {
        image = GetComponent<Image>();
        lastValidSprite = image.sprite;

        localizedSprite.AssetChanged += OnSpriteChanged;
    }

    void OnDestroy()
    {
        localizedSprite.AssetChanged -= OnSpriteChanged;
    }

    void OnSpriteChanged(Sprite newSprite)
    {
        if (newSprite != null)
        {
            image.sprite = newSprite;
            lastValidSprite = newSprite;
        }
        else
        {
            // Prevent black square
            image.sprite = lastValidSprite;
        }
    }
}