using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarScript : MonoBehaviour
{

    public GameObject prefabHPSingle = null;

    public GameObject TheCanvas;

    public GameObject PlayerInfoGO = null;
    protected PlayerData PlayerInfo = null;

    List<GameObject> Hp_Bar = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerInfo == null)
            if (PlayerInfoGO)
            {
                PlayerInfo = PlayerInfoGO.GetComponent<PlayerData>();
            }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        
    }

    public void Apply()
    {
        foreach(var i in Hp_Bar)
        {
            GameObject.Destroy(i);
        }
        Hp_Bar.Clear();

        int AddAdditionHPByGroup = 0;
        if(PlayerInfo.PlayerGroupIndex !=GroupDataInfo.GroupIndex.FreeGroup)
        {
            AddAdditionHPByGroup = 1;
        }
        int x = -139, y = -220;
        for (int i = 0; i < PlayerInfo.HP + AddAdditionHPByGroup; i++)
        {
            GameObject tGO =
            Instantiate(prefabHPSingle, new Vector3(x, y, 0), Quaternion.identity);
            tGO.transform.SetParent(TheCanvas.transform, false);
            tGO.GetComponent<RectTransform>().localScale = new Vector3(1.0f / 18f, 1.0f / 18f, 1.0f / 18f);

            if(i==PlayerInfo.HP)
            {
                tGO.GetComponent<Hp_Script>().Apply(false);
            }
            else
            {
                tGO.GetComponent<Hp_Script>().Apply(true);
            }

            Hp_Bar.Add(tGO);
            x += 20;
        }
    }
}
