using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEditor;

public class G15_L2_AmmadMain : MonoBehaviour
{
    public Button HomeButton;
    // Start is called before the first frame update
    public Button inputBtn;
    public Text inputText;
    string InputString;
    void Start()
    {
        HomeButton.onClick.AddListener(homefunction);
        inputBtn.onClick.AddListener(NextScene);
    }
    private void homefunction()
    {
        SceneManager.LoadScene("G15_MainMenu");

    }
    private void NextScene()
    {
        InputString = inputText.text.Trim();
        Regex regex = new Regex("^[x-y]*$");
        if (!regex.IsMatch(InputString))
        {
            inputText.text = String.Empty;
            EditorUtility.DisplayDialog("Invalid","Enter Only x or y","Thanks");
            
            return;
        }
        else
        SceneManager.LoadScene("G15_L2_AmmadMachine");
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("Input", inputText.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
