using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEditor;
public class G15_MainMenuWork : MonoBehaviour
{//time .
    // Start is called before the first frame update
    public Button inputBtnPalindrome;
    public Button inputBtnMughees;
    public Button inputBtnSajid;
    public Button inputBtnAmmad;
    public Button inputExit;
    void Start()
    {
        inputBtnPalindrome.onClick.AddListener(PalidromeFunction);
        inputBtnMughees.onClick.AddListener(MugheesFunction);
        inputBtnSajid.onClick.AddListener(SajidFunction);
        inputBtnAmmad.onClick.AddListener(AmmadFunction);
        inputExit.onClick.AddListener(exitFunction);
    }

    private void exitFunction()
    {
     Debug.Log("Game is Exiting");
      Application.Quit();
    }

    private void PalidromeFunction()
    {
        SceneManager.LoadScene("G15_Palindromescene");
    }
    private void MugheesFunction()
    {
        SceneManager.LoadScene("G15_L1_MugheesMain");
    }
    private void SajidFunction()
    {
        SceneManager.LoadScene("G15_L3_SajidMain");
    }
    private void AmmadFunction()
    {
        SceneManager.LoadScene("G15_L2_AmmadMain");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
