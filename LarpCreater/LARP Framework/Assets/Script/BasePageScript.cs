using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePageScript : MonoBehaviour
{
    public GameObject CurrentPage = null;
    public GameObject NextPage = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject ToNextPage()
    {
      return  TransmitPageScript.GoToNextPage(ref CurrentPage, ref NextPage);
    }

    public bool isNextPageAva()
    {
        if (NextPage == null) return false;
        return true;
    }

}
