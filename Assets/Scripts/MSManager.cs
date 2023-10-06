using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MSManager : MonoBehaviour
{

    public float colThickness = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;
    public AudioSource _audioSource;
    public AudioClip _tankMove;
    public bool moving;


    public Text p1ScoreText;
    public Text p2ScoreText;
    private int p1Score;
    private int p2Score;
    // Start is called before the first frame update
    public void PrintWorking()
    {
        print("working");
    }
    void Start()
    {
        p1Score = p2Score = 0;
        GenerateCollidersAcrossScreen();
        _audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("P1Score"))
        {
            p1Score = PlayerPrefs.GetInt("P1Score");
            p1ScoreText.text = "Player 1 Score: " + p1Score.ToString();
        }
        if (PlayerPrefs.HasKey("P2Score"))
        {
            p2Score = PlayerPrefs.GetInt("P2Score");
            p2ScoreText.text = "Player 2 Score: " + p2Score.ToString();
        }
    }

    
    /*
     * adapted from https://forum.unity.com/threads/collision-with-sides-of-screen.228865/
     */
    void GenerateCollidersAcrossScreen()
    {
        //Create a Dictionary to contain all our Objects/Transforms
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>
        {
            { "Top", new GameObject().transform },
            { "Bottom", new GameObject().transform },
            { "Right", new GameObject().transform },
            { "Left", new GameObject().transform }
        };

        //Generate world space point information for position and scale calculations
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;
        //For each Transform/Object in our Dictionary
        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>(); //Add our colliders. Remove the "2D", if you would like 3D colliders.
            valPair.Value.name = valPair.Key + "Collider"; //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
            valPair.Value.parent = transform; //Make the object a child of whatever object this script is on (preferably the camera)

            if (valPair.Key == "Left" || valPair.Key == "Right") //Scale the object to the width and height of the screen, using the world-space values calculated earlier
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            else
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        }
        //Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier, and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
        colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);

    }

    public bool ManageWin(string player)
    {
        if (player.Equals("P1"))
        {
            p2Score++;
            p2ScoreText.text = "Player 2 Score: " + p2Score.ToString();
            PlayerPrefs.SetInt("P2Score", p2Score);

        }
        else
        {
            p1Score++;
            p1ScoreText.text = "Player 1 Score: ";// + p1Score.ToString();
            PlayerPrefs.SetInt("P1Score", p1Score);
        }
        if (p1Score < 5 && p2Score < 5)
        {
            return true;
        }
        PlayerPrefs.SetInt("P1Score", 0);
        PlayerPrefs.SetInt("P2Score", 0);
        return false;
    }
}


