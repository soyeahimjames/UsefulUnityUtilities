using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Screenshot : EditorWindow
{
	//[Shortcut("J-Scripts/Screenshot")]
    [MenuItem("J-Scripts/Screenshot")]
    public static void Screengrab()
    {
        string path = EditorUtility.SaveFilePanelInProject("Screenshot Current Gameview", "Screenshot", "png", "Save?");

        if (path.Length != 0)
        {
            ScreenCapture.CaptureScreenshot(path);
        }
    }
}
