using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //The refference to the map
    Map map;
    //The reference to the main camera
    Camera cam;
    //Set up the gui layer mask to check for mouse click manualky
    public LayerMask guiLayerMask;
    //Check if the controlpanel is active
    bool controlPanelActive;
    //The cursor prefab
    public GameObject cursorPrefab;
    //Get a reference to the GameManager script
    GameManager gameManager;

    //The world coordinates of the mouse in the last frame
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    //The reference to the control panel
    public GameObject controlPanel;

    //The original position of the mouse when it was clicked becuase if we click a secon time we overide the click position
    Vector2 buildMousePos;

    // Use this for initialization
    void Start()
    {
        //Get a reference to the map script on the game manager object this script is also comnneted to
        map = GetComponent<Map>();
        //Get the reference to teh MainCamera
        cam = GameObject.FindGameObjectWithTag("64Cam").GetComponent<Camera>();
        //Instantiate the cursor object
        cursorPrefab = Instantiate(cursorPrefab);
        //Set the cursor invisible
        Cursor.visible = false;
        //Set the refference to the game manager
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currFramePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();

        lastFramePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

        //Keeps the control panel in the same position of hte screen we want it to be
        controlPanel.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 3f, controlPanel.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {

            if (gameManager.win)
            {
                Application.Quit();
            }
            if (gameManager.lose)
            {
                Application.Quit();
            }

            if (gameManager.startScreenActive)
            {
                //Get where the mouse was cliked on the screen
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //Shoot a ray on mouse click to check if a button was presed on the start screen
                RaycastHit2D mouseHit = Physics2D.Linecast(mousePos, mousePos);

                if (mouseHit.collider.name == "Start")
                {
                    gameManager.StartGame();
                }

                if (mouseHit.collider.name == "Exit")
                {
                    Application.Quit();
                }

            }
            else
            {

                //Get the position of the mouse
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));

                //Get the tree at the tile if it exishts
                Tree treeInfo = null;
                //Get the tile to work with
                Tile tileInfo = map.GetTileAtPos(mousePos);
                //Get the tree game object
                GameObject tree_go = null;



                if (!controlPanel.activeSelf && tileInfo.CanBuild)
                {
                
                    //We set the originalClickPosition here becuase it wil only be set one as the control panel is not active
                    buildMousePos = mousePos;
                    //Check if the control panel is active and if it is not make it active
                    controlPanel.SetActive(true);
//Display the food panel when the mouse is clicked
                    if(tileInfo.CanBuild) gameManager.TogleFoodDisplay();
                    //Get teh tile and the tile game object ot modify and check for the tile status
                    //tileInfo = map.GetTileAtPos(mousePos);

                    if (tileInfo != null)
                    {
                        //If te tree has a tree
                        if (!tileInfo.HasTree)
                        {
                            controlPanel.transform.GetChild(1).gameObject.SetActive(true);
                        }
                        else
                        {
                            //Get the tree at the tile if it exishts
                            treeInfo = map.GetTreeScript(mousePos);

                            if (!treeInfo.HasFarm)
                            {
                                controlPanel.transform.GetChild(2).gameObject.SetActive(true);
                            }
                            if (!treeInfo.HasTower)
                            {
                                controlPanel.transform.GetChild(0).gameObject.SetActive(true);
                            }
                        }
                    }
                }
                else 
                {                    
                    
                    //Get teh tile and the tile game object ot modify and check for the tile status
                    tileInfo = map.GetTileAtPos(buildMousePos);

                    //Shoot a ray on mouse click to check if a button was presed
                    RaycastHit2D mouseHit = Physics2D.Linecast(mousePos, mousePos, guiLayerMask);

                    if (mouseHit.collider != null)
                    {
                        //If the tile has a tree then we reference the tree script here
                        if (tileInfo.HasTree)
                        {
                            //Get the tree at the tile if it exishts
                            treeInfo = map.GetTreeScript(buildMousePos);
                            tree_go = map.GetTreeGO(buildMousePos);
                        }


                        if (mouseHit.collider.name == "TreeGOButton")
                        {
                            //Add the tree if there is enough food
                            if (gameManager.food >= 4)
                            {
                                gameManager.food -= 4;
                                map.AddTree(buildMousePos, false);
                            }

                            controlPanel.transform.GetChild(1).gameObject.SetActive(false);
                            controlPanel.SetActive(false);
                            //Display the food panel when the mouse is clicked
                            gameManager.TogleFoodDisplay();

                        }
                        else if (mouseHit.collider.name == "TowerGOButton")
                        {
                            if (gameManager.food >= 3)
                            {
                                gameManager.food -= 4;
                                treeInfo.AddTower();
                                //Set the sprite fot the new tree
                                if (!treeInfo.HasFarm)
                                {
                                    tree_go.GetComponent<SpriteRenderer>().sprite = map.treeTowerSprites[treeInfo.treeSprite];
                                }
                                else
                                {
                                    tree_go.GetComponent<SpriteRenderer>().sprite = map.treeComboSprites[treeInfo.treeSprite];
                                }
                            }

                            controlPanel.transform.GetChild(0).gameObject.SetActive(false);
                            controlPanel.SetActive(false);
                            //Display the food panel when the mouse is clicked
                            gameManager.TogleFoodDisplay();
                        }
                        else if (mouseHit.collider.name == "FarmGOButton")
                        {
                            if (gameManager.food >= 2)
                            {
                                gameManager.food -= 4;
                                treeInfo.AddFarm();
                                if (!treeInfo.HasTower)
                                {
                                    tree_go.GetComponent<SpriteRenderer>().sprite = map.treeFarmSprites[treeInfo.treeSprite];
                                }
                                else
                                {
                                    tree_go.GetComponent<SpriteRenderer>().sprite = map.treeComboSprites[treeInfo.treeSprite];
                                }
                            }
                            controlPanel.transform.GetChild(2).gameObject.SetActive(false);
                            controlPanel.SetActive(false);
                            //Display the food panel when the mouse is clicked
                            gameManager.TogleFoodDisplay();
                        }
                        else
                        {
                            Debug.Log("No valid button selected");
                        }
                    }
                    else
                    {
                        //If the panel active but the press was not on the control panel then we disable the control panel again
                        controlPanel.SetActive(false);
                        //Display the food panel when the mouse is clicked
                        gameManager.TogleFoodDisplay();
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        cursorPrefab.transform.position = GetMousePosition2D();
    }

    Vector2 GetMousePosition2D()
    {
        if (controlPanel.activeSelf || gameManager.startScreenActive)
        {
            return new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }
        else
        {
            return new Vector2(Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y));
        }

    }

    void UpdateCameraMovement()
    {
        //Update screen dragging
        if (Input.GetMouseButton(1))
        {
            //TODO: Tweak the movement capture on the mouse a bit more but the rounding to int is working beautifuly
            Vector3 diff = lastFramePosition - currFramePosition;
            //Round the tempdiff value to ints so that it jumps ints not just move smoothly
            diff.x = Mathf.RoundToInt(diff.x);
            diff.y = Mathf.RoundToInt(diff.y);

            //Lock the camera to the map so it does not go out of bounds
            if ((cam.transform.position.x + diff.x) < (map.Width - 4) && (cam.transform.position.x + diff.x) > (0 + 3))
            {
                cam.transform.Translate(new Vector2(diff.x, 0));
                Camera.main.transform.Translate(new Vector2(diff.x, 0));
            }

            if ((cam.transform.position.y + diff.y) < (map.Height - 4) && (cam.transform.position.y + diff.y) > (0 + 3))
            {
                cam.transform.Translate(new Vector2(0, diff.y));
                Camera.main.transform.Translate(new Vector2(0, diff.y));
            }

        }
    }
}
