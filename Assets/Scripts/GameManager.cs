using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool win = false, winTriggered = false;
    public GameObject winScreen;

    [HideInInspector]
    public bool lose = false, loseTriggered = false;
    public GameObject loseScreen;

    

    //The start screen variables
    [HideInInspector]
    public bool startScreenActive = true;

    public GameObject startScreen;

    //The fade screen variables
    [HideInInspector]
    public bool fadeScreenActive = false;

    public GameObject fadeScreen;

    int fadeScreenTimer = 1;
    float timer = 0;

    //The refference to the map
    Map map;
    //The reference to the main camera
    Camera cam;
    //Get the wave script
    Waves wave;

    //Used for building
    public float food;

    //The components needed for the food display on manual
    //Number Sprites
    public Sprite[] numbers = new Sprite[10];
    public SpriteRenderer singleDigits, doubleDigits;
public GameObject foodDisplay;

    // Use this for initialization
    void Start()
    {
        startScreen.SetActive(true);

        //Get a reference to the map script on the game manager object this script is also comnneted to
        map = GetComponent<Map>();
        //Get the reference to teh MainCamera
        cam = GameObject.FindGameObjectWithTag("64Cam").GetComponent<Camera>();
        //Set the waves script pointer here
        wave = GetComponent<Waves>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateFoodDisplay();

        if (win && !winTriggered)
        {
            winTriggered = true;
            Win();
        }

        if (lose && !loseTriggered)
        {
            loseTriggered = true;
            Lose();
        }
        if (!startScreenActive)
        {
            for (int i = 0; i < map.Trees.Count; i++)
            {

                if ((Time.time - map.Trees[i].GetComponent<Tree>().lastFoodTime) > map.Trees[i].GetComponent<Tree>().interval)
                {
                    map.Trees[i].GetComponent<Tree>().lastFoodTime = Time.time;
                    food += 0.2f * map.Trees[i].GetComponent<Tree>().NoOfFarms;
                }
            }
        }
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        startScreenActive = false;
        map.NewMap();
        wave.CountDown();

    }

    void Win()
    {

        winScreen.SetActive(true);
    }

    void Lose()
    {

        loseScreen.SetActive(true);
    }

    public void TogleFoodDisplay()
    {
        foodDisplay.SetActive(!foodDisplay.activeSelf);
    }

    void UpdateFoodDisplay()
    {
        //Keeps the fod display panel where the camera is at the moment
        foodDisplay.transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);

        if (Mathf.FloorToInt(food / 10) == 1) doubleDigits.sprite = numbers[1];
        if (Mathf.FloorToInt(food / 10) == 2) doubleDigits.sprite = numbers[2];
        if (Mathf.FloorToInt(food / 10) == 3) doubleDigits.sprite = numbers[3];
        if (Mathf.FloorToInt(food / 10) == 4) doubleDigits.sprite = numbers[4];
        if (Mathf.FloorToInt(food / 10) == 5) doubleDigits.sprite = numbers[5];
        if (Mathf.FloorToInt(food / 10) == 6) doubleDigits.sprite = numbers[6];
        if (Mathf.FloorToInt(food / 10) == 7) doubleDigits.sprite = numbers[7];
        if (Mathf.FloorToInt(food / 10) == 8) doubleDigits.sprite = numbers[8];
        if (Mathf.FloorToInt(food / 10) == 9) doubleDigits.sprite = numbers[9];
        if (Mathf.FloorToInt(food / 10) == 0) doubleDigits.sprite = numbers[0];

        int doubleDigit = Mathf.FloorToInt(food / 10);
        int singleDigit = Mathf.FloorToInt(food - doubleDigit * 10);
        if (singleDigit == 1) singleDigits.sprite = numbers[1];
        if (singleDigit == 2) singleDigits.sprite = numbers[2];
        if (singleDigit == 3) singleDigits.sprite = numbers[3];
        if (singleDigit == 4) singleDigits.sprite = numbers[4];
        if (singleDigit == 5) singleDigits.sprite = numbers[5];
        if (singleDigit == 6) singleDigits.sprite = numbers[6];
        if (singleDigit == 7) singleDigits.sprite = numbers[7];
        if (singleDigit == 8) singleDigits.sprite = numbers[8];
        if (singleDigit == 9) singleDigits.sprite = numbers[9];
        if (singleDigit == 0) singleDigits.sprite = numbers[0];

    }
}
