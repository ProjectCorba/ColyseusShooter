using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private RectTransform _filledImage;
    [SerializeField] private float _defaultWidth;

    private void OnValidate()
    {
        _defaultWidth = _filledImage.sizeDelta.x;
    }
    public void UpdateHealth(float max, int current)
    {
        float filled = current / max;
        _filledImage.sizeDelta = new Vector2(_defaultWidth * filled, _filledImage.sizeDelta.y);
    }
}
