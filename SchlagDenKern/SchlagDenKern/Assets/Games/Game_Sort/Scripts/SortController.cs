using UnityEngine;
using System.Collections;

public class SortController : GameController
{

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
