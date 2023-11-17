using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    [SerializeField]
    private Camera screenshotCamera;

    public Sprite ScreenshotToSprit()
    {
        // [Screen] == Game Screen
        int width = Screen.width;
        int height = Screen.height;

        // Create a Texture that Screenshot goes into with the given width and height
        RenderTexture screenshotContainer = new RenderTexture(width, height, 24);
        // Register the screenshotTexture into the Camera's targetTexture variable where the Camera renders
        screenshotCamera.targetTexture = screenshotContainer;

        // Render the Camera
        screenshotCamera.Render();
        RenderTexture.active = screenshotContainer;

        // Read the Pixels information from the active RenderTexture
        // Save the Texture2D into the [screenshot]
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, (height - width) * 0.5f, width, width), 0, 0);
        screenshot.Apply();

        // Empty the targetTexture as Screenshotting is done
        screenshotCamera.targetTexture = null;

        Rect rect = new Rect(0, 0, screenshot.width, screenshot.height);
        Sprite sprite = Sprite.Create(screenshot, rect, Vector2.one * 0.5f);

        return sprite;
    }
}
