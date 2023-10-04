using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class EndScreenManagerScript : MonoBehaviour
{
    public Text winnerText;
    // Start is called before the first frame update
    void Start()
    {
        winnerText.text = PlayerWinController.winner + " wins the game!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
