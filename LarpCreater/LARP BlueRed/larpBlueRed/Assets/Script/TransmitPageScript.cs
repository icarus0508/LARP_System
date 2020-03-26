using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitPageScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        DoChangeButtonAppearance();
    }

    public GameObject NextPageButton=null;
    public GameObject PrePageButton = null;

    public GameObject CurrentPage = null;
 
    private static Stack<GameObject> PreivewPageStack = new Stack<GameObject>();

    private bool NextButtonVisible = false;

    public static void Reset()
    {
        PreivewPageStack.Clear();
    }

    public static GameObject GoToNextPage(ref GameObject InCurrentPage, ref GameObject NextPage)
    {
        PreivewPageStack.Push(InCurrentPage);
        InCurrentPage.SetActive(false);
        NextPage.SetActive(true);

        return NextPage;

    }
    
    public static  GameObject GoToLastPage(ref GameObject InCurrentPage)
    {
        
       if(PreivewPageStack.Count !=0)
        {
            InCurrentPage.SetActive(false);
            GameObject temPrePage= PreivewPageStack.Pop();
            temPrePage.SetActive(true);
            return temPrePage;
        }
        return null;
    }


    public void NextPage()
    {
        CurrentPage = CurrentPage.GetComponentInChildren<BasePageScript>(true).ToNextPage();
        DoChangeButtonAppearance();
    }

    public void LastPage()
    {
        CurrentPage= GoToLastPage(ref CurrentPage);
        DoChangeButtonAppearance();
    }

    public void ForceSetNextPageBtnActive(bool active)
    {
        NextButtonVisible = active;
    }

    private void ForcePostSetNextPageBtnStatus()
    {
        NextPageButton.SetActive(NextButtonVisible);
    }
    private void DoChangeButtonAppearance()
    {
        if (CurrentPage.GetComponentInChildren<BasePageScript>(true) == null) return;

      if (CurrentPage.GetComponentInChildren<BasePageScript>(true).isNextPageAva())
        {
            NextPageButton.SetActive(true);
        }
      else
        {
            NextPageButton.SetActive(false);
        }

      if(PreivewPageStack.Count ==0)
        {
            PrePageButton.SetActive(false);
        }
      else
        {
            PrePageButton.SetActive(true);
        }

        ForcePostSetNextPageBtnStatus();
    }
}
