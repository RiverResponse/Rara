using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Resizes a UI element with a RectTransform to respect the safe areas of the current device.
/// This is particularly useful on an iPhone X, where we have to avoid the notch and the screen
/// corners.
/// 
/// The easiest way to use it is to create a root Canvas object, attach this script to a game object called "SafeAreaContainer"
/// that is the child of the root canvas, and then layout the UI elements within the SafeAreaContainer, which
/// will adjust size appropriately for the current device./// </summary>
///
/// Based on https://gist.github.com/SeanMcTex/c28f6e56b803cdda8ed7acb1b0db6f82
public class PinToSafeArea : MonoBehaviour
{
    private RectTransform parentRectTransform;

    private void Start()
    {
        ApplySafeArea();
    }

    [Button]
    private void ApplySafeArea()
    {
        parentRectTransform = GetComponentInParent<RectTransform>();
        
        Rect safeAreaRect = Screen.safeArea;

        float scaleRatio = parentRectTransform.rect.width / Screen.width;

        var left = safeAreaRect.xMin * scaleRatio;
        var right = -(Screen.width - safeAreaRect.xMax) * scaleRatio;
        var bottom = safeAreaRect.yMin * scaleRatio;
        var top = (Screen.height - safeAreaRect.yMax) * scaleRatio;

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.offsetMin = new Vector2(left, bottom);
        rectTransform.offsetMax = new Vector2(right, top);
    }
}