using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private GroundManager GroundScript;

    // Use this for initialization
    int lives;
    public string state;
    bool lastEnemy = true;
    public int count;
    void Start()
    {
        lives = GameObject.Find("digdug_various_sheet_4").GetComponent<PlayerBehavoir>().playerLives;
        count = 0;
    }
	// Update is called once per frame
	void Update ()
    {
        if(lives != GameObject.Find("digdug_various_sheet_4").GetComponent<PlayerBehavoir>().playerLives)
        {
            lives--;
            state = "restart";
        }
        if(GameObject.FindGameObjectsWithTag("enemy").Length == 1 && lastEnemy)
        {
            lastEnemy = false;
            GameObject[] tempene = GameObject.FindGameObjectsWithTag("enemy");
            
            foreach(GameObject i in tempene)
            {
                if (i.name == "Fygars")
                {
                    i.GetComponent<FBehaviour>().speedH *= 2;
                    i.GetComponent<FBehaviour>().speedV *= 2;
                    i.GetComponent<FBehaviour>().speedMoveTowards *= 2;
                }
                else
                {
                    i.GetComponent<PBehaviour>().speedH *= 2;
                    i.GetComponent<PBehaviour>().speedV *= 2;
                    i.GetComponent<PBehaviour>().speedMoveTowards *= 2;
                }
            }
        }
        //check player lives
        if (GameObject.Find("digdug_various_sheet_4").GetComponent<PlayerBehavoir>().playerLives == 0)
        {
            Application.LoadLevel("StartScene");
        }
        if(GameObject.FindGameObjectsWithTag("enemy").Length == 0)
        {
            if(Application.loadedLevel == 1)
            {
                Application.LoadLevel("sceneGame2");
            }
            else if(Application.loadedLevel == 2)
            {
                Application.LoadLevel("StartScene");
            }
        }
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.LoadLevel("StartScene");
        }
	
	}
}
