using UnityEngine;
using System.Collections;

public class ButtonClicks : MonoBehaviour {

	public void clickLevel1()
    {
        Application.LoadLevel("sceneGame");
    }
    public void clickLevel2()
    {
        Application.LoadLevel("sceneGame2");
    }
    public void clickExit()
    {
        Application.Quit();
    }
}
