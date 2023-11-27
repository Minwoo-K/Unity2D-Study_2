using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;

    public Sprite ScreenshotToSprite()
    {
        int width = Screen.width;
        int height = Screen.height;

        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        mainCamera.targetTexture = renderTexture;

        mainCamera.Render();
        RenderTexture.active = renderTexture;

        Texture2D screenshot = new Texture2D(width, width, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, (height - width) * 0.5f, width, width), 0, 0);
        screenshot.Apply();

        mainCamera.targetTexture = null;

        Rect rect = new Rect(0, 0, screenshot.width, screenshot.height);
        Sprite sprite = Sprite.Create(screenshot, rect, Vector2.one * 0.5f);

        return sprite;
    }
}
