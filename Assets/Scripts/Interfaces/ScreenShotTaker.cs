using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotTaker : MonoBehaviour
{
    private int i;
    private void Update()
    {
        // Check for a specific input (e.g., 'S' key) to trigger the screenshot
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureScreenshot();
        }
    }

    private void CaptureScreenshot()
    {
        if (i == null) i = 0;
        // Capture the screenshot and specify the file name (e.g., "screenshot.png")
        ScreenCapture.CaptureScreenshot("screenshot"+i+".png");

        // You can also specify a file path like this:
        // ScreenCapture.CaptureScreenshot("/path/to/save/screenshot.png");

        Debug.Log("Screenshot captured!");
    }
}
