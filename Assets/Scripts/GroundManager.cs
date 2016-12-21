using UnityEngine;
using System.Collections.Generic;
public class GroundManager : MonoBehaviour {

	public int row = 2;
	public int column = 2;
	public GameObject[] groundType;

	private Transform GroundHolder;
	private List<Vector3> gridPostions = new List<Vector3>();

	//first we must initials the list
	//to ganerate the ground tiles
	void ListInit()
	{
		//clearing list
		gridPostions.Clear ();
		//loop and add the number of colurmns and rows
		for (float i=0.0f; i<row -1; i+=.138f) 
		{
			for (float j=0.0f; j<column -1; j+=.138f) 
			{
				gridPostions.Add(new Vector3(i, j, 0.0f));
			}
		}
	}
	//set up the ground tiles
	void SetGround()
	{
		//get the ground object and a reference to its transform
		GroundHolder =  GameObject.Find ("Ground").transform;
		Debug.Log (GroundHolder);
		for (float i=0.0f; i< row; i+=.138f) 
		{
			for(float j=0.0f; j< column; j+=.138f)
			{
				//Select a game object to be instantiated
				Debug.Log (j);
				int select = 0;
				if( j > 3.5){
					select = 2;
				}
				else if( j > 5.25){
					select = 1;
				}
				else if( j > 7){
					select = 0;
				}
				GameObject toBeInst = groundType[select];

				GameObject thisGroundTile = (GameObject)Instantiate(toBeInst, 
				                                 new Vector3(i, j, 0.0f),
				                                 Quaternion.identity);
				thisGroundTile.transform.SetParent(GroundHolder);
			}
		}
	}

	//finally setup the scene
	public void SetupScene()
	{
		SetGround ();
		ListInit ();

	}

}
