using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEditor;

public class G15_L1_MugheesMain : MonoBehaviour
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
        Regex regex = new Regex("^[a-b]*$");
        if (!regex.IsMatch(InputString))
        {
            inputText.text = " ";
            EditorUtility.DisplayDialog("Invalid","Enter Only a or b","Thanks");
            
            return;
        }
        else
        SceneManager.LoadScene("G15_L1_MugheesMachine");
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
