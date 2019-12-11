using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Info : MonoBehaviour
{
    public string Name;
    public string Photo;
    public string Rank = "C";
    public string Clasz = "W";
    public string Sided = "BI";
    public List<int> SkillIndexes = new List<int>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayerInfoFromFile()
    {

    }

}
