using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

[Serializable]
public class ColorEvent : UnityEvent<Color> { }

public class ColorPicker : MonoBehaviour
{
    RectTransform rect;
    Texture2D colorTexture;
    public ColorEvent OnColorPreview;
    public ColorEvent OnColorSelect;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out delta);

        float width = rect.rect.width;
        float height = rect.rect.height;

        delta += new Vector2(width * 0.5f, height * 0.5f);

        float x = Mathf.Clamp(delta.x / width, 0f, 1f);
        float y = Mathf.Clamp(delta.y / height, 0f, 1f);

        int textureX = Mathf.RoundToInt(x * colorTexture.width);
        int textureY = Mathf.RoundToInt(y * colorTexture.height);

        Color color = colorTexture.GetPixel(textureX, textureY);

        OnColorPreview?.Invoke(color);

        if (Input.GetMouseButtonDown(0))
        {
            OnColorSelect?.Invoke(color);
            this.gameObject.SetActive(false);
        }
    }
}
