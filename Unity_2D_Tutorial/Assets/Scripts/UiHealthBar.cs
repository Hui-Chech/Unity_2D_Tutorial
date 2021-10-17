using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealthBar : MonoBehaviour
{
    public static UiHealthBar instance { get; private set;}
    public Image Mask;
    float OriginalSize;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        OriginalSize = Mask.rectTransform.rect.width;
    }
    public void SetValue(float value)
    {
        Mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, OriginalSize * value);
    }

}
