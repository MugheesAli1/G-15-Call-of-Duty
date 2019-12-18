using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;

public class G15_L2_AmmadMachine : MonoBehaviour
{//work jiii ////
    public Button HomeButton;
    public AudioSource SpaceButtonMusic; public AudioSource AcceptedMusic; public AudioSource RegectedMusic;
    //public GameObject UserInputString;
    public GameObject TapeHead;
    public Image HideAcceptedImageWork;
    public Image HideRejectedImageWork;
    // public AudioClip headMovementSound;
    // /public AudioSource headAudioSource;
    //public AudioSource CubeRotationSoundSource;
    //public AudioClip CubeRotationSound;
    //public AudioClip AcceptedSound;
    //public AudioSource AcceptedSoundSource;
    //public AudioSource RejectedSoundSource;
    //public AudioClip RejectedSound;
    //public Button ButtonPlay;
    public GameObject HeadDisplay;
    public TextMesh CurrentStateText;

    private GameObject cube;
    private float positionCube;
    private string CurrentState;
    private List<char> Word = new List<char>();
    private int HeadPosition = 1;
    private char symbol;
    private string inputText;
    private int MovementSpeed;
    private GameObject CurrentCube;
    //bool isAnimationStoped = true;
    //    bool IsMovementStoped = true;
    
    void Start()
    {

        positionCube = 0;
        MovementSpeed = 8;
        symbol = '$';
        HeadPosition = 1;
        CurrentState = "q1";
        // headAudioSource.clip = headMovementSound;
        // CubeRotationSoundSource.clip = CubeRotationSound;
        // AcceptedSoundSource.clip = AcceptedSound;
        // RejectedSoundSource.clip = RejectedSound;

        //ButtonPlay.onClick.AddListener(ResetMachine);
        //ButtonPlay.onClick.AddListener(GetInput);

        //GetInput();
        InitializeMachine();
        HomeButton.onClick.AddListener(homefunction);

    }
    private void homefunction()
    {
        SceneManager.LoadScene("G15_MainMenu");

    }
    private void OnEnable()
    {
        inputText = PlayerPrefs.GetString("Input");
    }

    void GetInput()
    {

        //validation textbox

       // inputText = UserInputString.GetComponent<Text>().text;// get text from input field
        //print("value:" + inputText);


        //ButtonPlay.gameObject.SetActive(false);
        //hide input and button
        InitializeMachine();
    }

    private void InitializeMachine()
    {
        //UserInputString.gameObject.SetActive(false);


        string input = symbol.ToString() + inputText + symbol.ToString() + symbol.ToString() + symbol.ToString() + symbol.ToString() + symbol.ToString();
        Word = ConvertToCharList(input);
        CreateMachine(Word);

        CurrentStateText.GetComponent<TextMesh>().text = "" + CurrentState;
    }

    public void ResetMachine()
    {
        SceneManager.LoadScene("InputScene");
    }

    private List<char> ConvertToCharList(string input)
    {
        List<char> list = new List<char>();
        foreach (char c in input)
        {
            list.Add(c);
        }

        return list;
    }

    void CreateMachine(List<char> list)
    {
        foreach (char c in list)
        {
            AddBlock(c.ToString());
        }
        GameObject.FindGameObjectWithTag("cube").SetActive(false);
    }

    void AddBlock(string text)
    {
        CreateCube(text, positionCube);
        positionCube += 300;
    }

    void CreateCube(string _cubeText, float position_X)
    {
        cube = Instantiate(GameObject.FindGameObjectWithTag("cube")) as GameObject;
        cube.transform.position = new Vector3(position_X, 0f, 0f);
        cube.transform.localScale = new Vector3(100f, 100f, 10f);
        cube.name = "CubeMachine" + _cubeText;
        cube.tag = "cubeMachine";
        TextMesh cubeText = cube.GetComponentInChildren<TextMesh>();
        cubeText.text = _cubeText;
    }

    bool isHalt = false;
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space) && !isHalt)
        {
            SpaceButtonMusic.Play();
            //IsMovementStoped = false;
            //headAudioSource.Play();
            System.Threading.Thread.Sleep(200);
            isHalt = GetNextState();
            CurrentStateText.GetComponent<TextMesh>().text = "" + CurrentState;
        }
        //try
       // {
        ///    isAnimationStoped = CurrentCube.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle");
        //}
        //catch
       // {

//        }
  //      if (isAnimationStoped)
            write();
    }


    private void UpdateHeadDisplay(Color screenColor, String displayText)
    {
        HeadDisplay.GetComponent<Renderer>().material.SetColor("_Color", screenColor);
        CurrentStateText.GetComponent<TextMesh>().text = displayText;
        //ButtonPlay.gameObject.SetActive(true);
    }

    public void write()
    {

        int currentPosition = Convert.ToInt32(TapeHead.transform.position.x);
        int newPosition = Convert.ToInt32(HeadPosition * 300);
        if (currentPosition < newPosition)
        {
            currentPosition += MovementSpeed;
            TapeHead.transform.position = new Vector3(currentPosition, 100, 0);

            if (currentPosition + MovementSpeed >= newPosition)
            {
                TapeHead.transform.position = new Vector3(newPosition, 100, 0);
            }

        }
        else if (currentPosition > newPosition)
        {

            currentPosition -= MovementSpeed;
            TapeHead.transform.position = new Vector3(currentPosition, 100, 0);
            if (newPosition >= currentPosition - MovementSpeed)
            {
                TapeHead.transform.position = new Vector3(newPosition, 100, 0);
            }
        }
        else
        {
            print("hhhh");
    //        headAudioSource.Stop();
     //       IsMovementStoped = true;
        }



    }

    private char getCurrentChar()
    {


        return Word[HeadPosition];
    }

    private void addExtraCube(int position_X, string text)
    {
        cube = Instantiate(GameObject.FindGameObjectWithTag("cubeMachine")) as GameObject;
        cube.transform.position = new Vector3(position_X, 0f, 0f);
        cube.transform.localScale = new Vector3(100f, 100f, 10f);
        cube.name = "CubeMachine" + text;
        cube.tag = "cubeMachine";
        TextMesh cubeText = cube.GetComponentInChildren<TextMesh>();
        cubeText.text = text;
    }


    public bool GetNextState()
    {
        switch (CurrentState)
        {
            case "q1":
                switch (getCurrentChar())
                {
                    case 'x':
                        PerformTransaction('a', Movement.R, "q2");
                        break;

                    case 'a':
                        PerformTransaction('a', Movement.R, "q2");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.R, "q6");
                        break;



                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        // RejectedSoundSource.Play();
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        return true;
                }

                break;

            case "q2":
                switch (getCurrentChar())
                {
                    case 'y':
                        PerformTransaction('b', Movement.R, "q3");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.R, "q2");
                        break;

                    case 'x':
                        PerformTransaction('x', Movement.R, "q2");
                        break;





                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q3":
                switch (getCurrentChar())
                {
                    case 'x':
                        PerformTransaction('c', Movement.R, "q4");
                        break;

                    case 'y':
                        PerformTransaction('y', Movement.R, "q3");
                        break;

                    case 'c':
                        PerformTransaction('c', Movement.R, "q3");
                        break;



                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q4":

                switch (getCurrentChar())
                {
                    case 'x':
                        PerformTransaction('c', Movement.L, "q5");
                        break;


                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q5":
                switch (getCurrentChar())
                {
                    case 'a':
                        PerformTransaction('a', Movement.R, "q1");
                        break;

                    case 'c':
                        PerformTransaction('c', Movement.L, "q5");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.L, "q5");
                        break;

                    case 'y':
                        PerformTransaction('y', Movement.L, "q5");
                        break;

                    case 'x':
                        PerformTransaction('x', Movement.L, "q5");
                        break;



                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q6":
                switch (getCurrentChar())
                {
                    case 'c':
                        PerformTransaction('c', Movement.R, "q7");
                        break;

                    case 'b':
                        PerformTransaction('b', Movement.R, "q6");
                        break;



                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q7":
                switch (getCurrentChar())
                {
                    case 'c':
                        PerformTransaction('c', Movement.R, "q7");
                        break;

                    case '$':
                        PerformTransaction('$', Movement.H, "q8");
                        break;

                    default:
                        HideRejectedImageWork.gameObject.SetActive(true);
                        UpdateHeadDisplay(Color.red, "Rejected");
                        SpaceButtonMusic.Stop();
                        RegectedMusic.Play();

                        //RejectedSoundSource.Play();
                        return true;
                }
                break;

            case "q8":
                HideAcceptedImageWork.gameObject.SetActive(true);
                getCurrentChar();
                UpdateHeadDisplay(Color.green, " Accepted");
                SpaceButtonMusic.Stop();
                AcceptedMusic.Play();

                //AcceptedSoundSource.Play();
                return true;






        }

        return false;
    }

    private void PerformTransaction(char replaceChar, Movement movement, string NextState)
    {
        char tempChar = Word[HeadPosition];
        Word[HeadPosition] = replaceChar;

        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y - 2, transform.position.z), Vector3.forward); //trying to single select
        RaycastHit hit;
 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            CurrentCube = hit.collider.gameObject;

           // if (Word[HeadPosition] != tempChar)
           // {
             //   CurrentCube.GetComponent<Animator>().Play("WriteCube");
                //CubeRotationSoundSource.Play();
            //}
            CurrentCube.GetComponentInChildren<TextMesh>().text = replaceChar.ToString();

        }

        HeadPosition += (int)movement;
        if (HeadPosition <= 0)
        {
            addExtraCube(HeadPosition * 300 - 300, symbol.ToString());
            Word.Insert(0, symbol);
            addExtraCube(HeadPosition * 300 - 300, symbol.ToString());
        }
        else if (HeadPosition >= Word.Count - 2)
        {
            addExtraCube(HeadPosition * 300 + 300, symbol.ToString());
            Word.Add(symbol);
        }
        CurrentState = NextState;
    }

    enum Movement
    {
        R = 1,
        L = -1,
        H = 0
    }

}
