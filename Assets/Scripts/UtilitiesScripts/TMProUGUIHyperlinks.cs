using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class handles basic link color behavior, supports also underline
/// Does not support strike-through, but can be easily implemented in the same way as the underline
/// </summary>
[DisallowMultipleComponent()]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TMProUGUIHyperlinks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private TextMeshProUGUI textMeshPro;
    private int hoveredLinkIndex = -1;
    private int pressedLinkIndex = -1;
    private Camera mainCamera;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();

        mainCamera = Camera.main;
        if (textMeshPro.canvas.renderMode == RenderMode.ScreenSpaceOverlay) mainCamera = null;
        else if (textMeshPro.canvas.worldCamera != null) mainCamera = textMeshPro.canvas.worldCamera;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        int linkIndex = GetLinkIndex();
        if (linkIndex != -1) // Was pointer intersecting a link?
        {
            pressedLinkIndex = linkIndex;
            hoveredLinkIndex = pressedLinkIndex; // Changes flow in LateUpdate
        }
        else pressedLinkIndex = -1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        int linkIndex = GetLinkIndex();
        if (linkIndex != -1 && linkIndex == pressedLinkIndex) // Was pointer intersecting the same link as OnPointerDown?
        {
            TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];
            Application.OpenURL(linkInfo.GetLinkID());
        }
        pressedLinkIndex = -1;
    }

    private void LateUpdate()
    {
        int linkIndex = GetLinkIndex();
        if (linkIndex != -1) // Was pointer intersecting a link?
        {
            if (linkIndex != hoveredLinkIndex) // We started hovering above link (hover can be set from OnPointerDown!)
            {
                hoveredLinkIndex = linkIndex;
            }
        }
        else if (hoveredLinkIndex != -1) // If we hovered above other link before
        {
            hoveredLinkIndex = -1;
        }
    }

    private int GetLinkIndex()
    {
        return TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, mainCamera);
    }
}