using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private Block block;
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log ("Hello World");
		
		// create a block
		block = new Block(23);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		// move the character around
		
	}
}
