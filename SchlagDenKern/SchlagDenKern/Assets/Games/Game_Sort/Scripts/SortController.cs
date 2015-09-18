using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortController : GameController
{

	private int currentSetIndex;
	private List<SortingSet> sortingSets; 

	public override void StartMe ()
	{
		base.StartMe ();
		currentSetIndex = 0;
		Init ();
	}

	public override void Solve ()
	{

	}

	public override void Next ()
	{
		if (currentSetIndex < sortingSets.Count) 
		{
			currentSetIndex++;
	
		}
	}

	public override void Previous ()
	{
		if (currentSetIndex > 0) 
		{
			currentSetIndex--;
		}
	}

	/*
	 * Get all sorting sets 
	 */
	private void Init ()
	{
		Guessables<SortingGuessable> guessables = XMLHandler.GetGuessableList<SortingGuessable>(GameLoadingController.Game.SORT);

		if(guessables.ordered)
		{
			guessables.guessablesList = XMLHandler.SortByOrder(guessables.guessablesList);
		}
		else 
		{
			guessables.guessablesList = XMLHandler.Shuffle(guessables.guessablesList);
		}


	}
	
}
