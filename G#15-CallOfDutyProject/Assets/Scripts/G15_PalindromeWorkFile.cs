using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEditor;
public class G15_PalindromeWorkFile : MonoBehaviour
{/// <summary>
 /// 
 /// </summary>
    public Button HomeButton;
    public AudioSource SpaceButtonMusic; public AudioSource AcceptedMusic; public AudioSource RegectedMusic;
    public Image HideAcceptedImageWork;
    public Image HideRejectImageWork;
    public Text UserInputString;
    private string UserStringSave;
    private int FirstInputValue;
    private int LastInputValue;
    private int InputStringLenght;
    private int StateVariable;
    public int NoOfCubes;
    public float PositionOfCube;
    string InputString;
    public float CamPositon;
    public string RayCastTxt;
    public Text LabelValue;
    private GameObject CreateCubes;
    private float InitialPosition;
    public Camera cameraa;
    private float CameraMovePosition;
    public bool Flag;
    private int MoveNextVariable;
    private int MoveBackVariable;
    public GameObject ButtonOff;
    public GameObject InputOff;
    public GameObject TxtMeshTxt;
    public GameObject CubeMeshTxt;
    void Start()
    {
        Flag = true;
        InitialPosition = -1.5f;
        CameraMovePosition = 1.5f;
        MoveNextVariable = 1;
        MoveBackVariable = 1;
        CamPositon = 1.5f;
        StateVariable = 1;

        HomeButton.onClick.AddListener(homefunction);
    }
    private void homefunction()
    {
        SceneManager.LoadScene("G15_MainMenu");

    }
void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpaceButtonMusic.Play();
            Ray myray = cameraa.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
            RaycastHit hit;
            if (Physics.Raycast(myray, out hit, 100))
            {
                TxtMeshTxt = hit.transform.GetChild(0).gameObject;
                RayCastTxt = TxtMeshTxt.GetComponent<TextMeshPro>().text;
                Debug.DrawLine(myray.origin, hit.point, Color.red);

                NoOfCubes = InputStringLenght / 2;
                PositionOfCube = NoOfCubes * 1.5f;


                if (!RayCastTxt.Equals("Δ"))
                {
                    if (Flag == true)
                    {
                        if (MoveNextVariable == 1)
                        {
                            StateVariable += 1;

                            FirstInputValue = int.Parse(RayCastTxt);
                            MoveBackVariable = 1;
                            TxtMeshTxt.GetComponent<TextMeshPro>().text = "Δ";

                        }
                        CamPositon += 1.5f;
                        State();
                        Forward();
                        MoveNextVariable += 1;

                    }
                    else if (Flag == false)
                    {
                        if (MoveBackVariable == 1)
                        {


                            LastInputValue = int.Parse(RayCastTxt);
                            if (FirstInputValue == LastInputValue)
                            {
                                StateVariable += 1;
                                TxtMeshTxt.GetComponent<TextMeshPro>().text = "Δ";
                            }

                            else
                            {/////
                                //State();
                                LabelValue.text = "Rejected";
                                HideRejectImageWork.gameObject.SetActive(true);
                                TxtMeshTxt.GetComponent<TextMeshPro>().color = Color.red;
                                SpaceButtonMusic.Stop();
                                RegectedMusic.Play();
                                enabled = false;
                            }
                            MoveNextVariable = 1;

                        }
                        CamPositon -= 1.5f;
                        State();
                        Backward();
                        MoveBackVariable += 1;
                    }
                }
                else
                {

                    if (Flag == true)
                    {
                        CamPositon -= 1.5f;
                        StateVariable += 1;
                        State();
                        Backward();
                        Flag = false;
                        if (MoveNextVariable == 1 && RayCastTxt.Equals("Δ") && StateVariable == 2)
                        {
                            LabelValue.text = "Accepted";
                            HideAcceptedImageWork.gameObject.SetActive(true);
                            LabelValue.color = Color.green;
                            //TxtMeshTxt.GetComponent<TextMeshPro>().color = Color.green;
                            SpaceButtonMusic.Stop();
                            AcceptedMusic.Play();

                            enabled = false;

                        }
                    }
                    else
                    {
                        CamPositon += 1.5f;
                        StateVariable = 1;
                        State();
                        Forward();
                        Flag = true;
                        if (MoveNextVariable == 2 && RayCastTxt.Equals("Δ"))
                        {

                            LabelValue.text = "Accepted";
                            HideAcceptedImageWork.gameObject.SetActive(true);
                            LabelValue.color = Color.green;
                            //TxtMeshTxt.GetComponent<TextMeshPro>().color = Color.green;
                            SpaceButtonMusic.Stop();
                            AcceptedMusic.Play();
                            enabled = false;

                        }
                    }
                }
            }
        }
    }
    public void Forward()
    {
        CameraMovePosition += 1.5f;
        cameraa.transform.position = new Vector3(CameraMovePosition, 0.68f, -1.6f);
    }
    public void Backward()
    {

        CameraMovePosition -= 1.5f;
        cameraa.transform.position = new Vector3(CameraMovePosition, 0.68f, -1.6f);
    }


    public void Clickbutton()
    {
        InputString = UserInputString.text.Trim();
        Regex regex = new Regex("^[0-1]*$");
        if (!regex.IsMatch(InputString))
        {
            UserInputString.text = " ";
            EditorUtility.DisplayDialog("Invalid", "Enter Only 0 or 1", "Thanks");

            return;
        }
        else
            ////
            ButtonOff.SetActive(false);
        InputOff.SetActive(false);


        UserStringSave = "Δ" + UserInputString.GetComponent<Text>().text + "Δ";

        InputStringLenght = UserStringSave.Length;

        int aa = 0;

        char[] CHarray = UserStringSave.ToCharArray();

        for (int i = 0; i < CHarray.Length; i++)
        {
            InitialPosition += 1.5f;
            //print(CHarray[i]);

            CreateCubes = GameObject.CreatePrimitive(PrimitiveType.Cube);
            CreateCubes.transform.position = new Vector3(InitialPosition, 0.5f, 0.0f);

            CreateCubes.transform.localScale = new Vector3(0.8f, 0.8f, 0.1f);
            CreateCubes.GetComponent<MeshRenderer>().material.color = Color.clear;
            GameObject go = new GameObject();
            go.transform.parent = CreateCubes.transform;
            CubeMeshTxt = CreateCubes.transform.GetChild(0).gameObject;
            CubeMeshTxt.AddComponent<TextMeshPro>();
            CubeMeshTxt.GetComponent<TextMeshPro>().fontSize = 5;
            CubeMeshTxt.GetComponent<TextMeshPro>().alignment = TextAlignmentOptions.Center;
            CubeMeshTxt.GetComponent<TextMeshPro>().text = CHarray[i].ToString();
            CubeMeshTxt.GetComponent<TextMeshPro>().color = Color.cyan;
            CubeMeshTxt.transform.Rotate(0, 0, 0);
            Vector3 pos = CubeMeshTxt.transform.localPosition;
            pos.x = 0;
            pos.y = 0;
            pos.z = -0.51f;
            CubeMeshTxt.transform.localPosition = pos;
            aa = aa + 2;
        }
        State();
    }
    public void State()
    {

        LabelValue.text = "Status: Q" + StateVariable;

    }


}
