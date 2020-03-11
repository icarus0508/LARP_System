using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPageScript : BasePageScript
{

    public GameObject StartSystemButton = null;
    public GameObject LoadSystemButton = null;

    public GameObject PlayerInfoGO = null;

    public GameObject fileBrowserPrefab;
    public string[] FileExtensions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckDataIsReady())
        {
            StartSystemButton.SetActive(true);
            LoadSystemButton.SetActive(true);
        }
    }

   private  bool CheckDataIsReady()
    {
        if (!gameObject.GetComponentInChildren<Skill_Info_Manager>(true).DataIsReady) return false;
        if (!gameObject.GetComponentInChildren<Classes_Info_Manager>(true).DataIsReady) return false;

        return true;
    }

    public void StartGame()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }

    public void LoadCharacterInfo()
    {
        OpenFileBrowser();

       
    }
    private void OpenFileBrowser()
    {
        GameObject fileBrowserObject = Instantiate(fileBrowserPrefab, transform);
        fileBrowserObject.name = "FileBrowser";
        GracesGames.SimpleFileBrowser.Scripts.FileBrowser fileBrowserScript =
            fileBrowserObject.GetComponent<GracesGames.SimpleFileBrowser.Scripts.FileBrowser>();

        fileBrowserScript.SetupFileBrowser(GracesGames.SimpleFileBrowser.Scripts.ViewMode.Portrait);

        fileBrowserScript.OpenFilePanel(FileExtensions);
        fileBrowserScript.OnFileSelect += LoadPlayerInfoUsingPath;
    }
    private void LoadPlayerInfoUsingPath(string path)
    {
        Player_Save_Info tPSI =
        CommonFunction.LoadPlayerSaveInfoJason(path);

        Player_Info tPI = PlayerInfoGO.GetComponent<Player_Info>();
        tPI.LoadFromPlayerSaveInfo(tPSI);

        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).NextPage();
    }
    private void OnEnable()
    {
        TransmitPage_GO.GetComponentInChildren<TransmitPageScript>(true).ForceSetNextPageBtnActive(false);
    }
}
