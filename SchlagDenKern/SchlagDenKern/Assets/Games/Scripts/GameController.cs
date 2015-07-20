using UnityEngine;
using System.Collections;

public class GameController : Singleton<GameController> 
{

	// Use this for initialization
	void Start () {
		StartMe ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMe ();
	}

	public override void UpdateMe ()
	{
		if(Input.GetKeyUp(KeyCode.S))
		{
			Solve();
		}

		if(Input.GetKeyUp(KeyCode.RightArrow))
		{
			Next();
		}
	}

	public virtual void Solve ()
	{

	}

	public virtual void Next ()
	{

	}

	public void Clear ()
	{
		GameObject.Find ("GameContent");
	}
}
