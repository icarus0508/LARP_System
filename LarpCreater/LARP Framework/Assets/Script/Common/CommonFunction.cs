using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CommonFunction : MonoBehaviour
{

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public static string FetchFileNameByEditor()
    {
        string ResultFilePath = Application.dataPath;

        return ResultFilePath;
    }

    public static Texture2D TextureToTexture2D(Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;
        RenderTexture renderTexture = RenderTexture.GetTemporary(texture.width, texture.height, 32);
        Graphics.Blit(texture, renderTexture);

        RenderTexture.active = renderTexture;
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        RenderTexture.active = currentRT;
        RenderTexture.ReleaseTemporary(renderTexture);
        return texture2D;
    }

    public static bool IsClassAva(string className, string avaclass)
    {
        string[] classes = avaclass.Split('/');
        foreach(var cla in classes)
        {
            if(className == cla)
            {
                return true;
            }
        }
        return false;
    }

    public void SwitchPage(ref GameObject ClosedObj, ref GameObject OpenObj)
    {
        ClosedObj.SetActive(false);
        OpenObj.SetActive(true);
    }

}
