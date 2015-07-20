using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Word : MyMonoBehaviour 
{
	// delay between two turning letters
	public float timeBetweenLetters;
	public Letter letterPrefab;

	private Letter[] letters;

	// creates all letters
	public void CreateWord (string word)
	{
		word = word.ToUpper();
		letters = new Letter[word.Length];

		float xSize = letterPrefab.GetComponent<RectTransform>().sizeDelta.x * word.Length;
		float ySize = letterPrefab.GetComponent<RectTransform>().sizeDelta.y;
		transform.GetComponent<RectTransform>().sizeDelta = new Vector2(xSize, ySize);

		int i = 0;
		foreach (char c in word)
		{
			Letter letter = GameObject.Instantiate (letterPrefab);
			letter.transform.SetParent(transform, false);
			letter.SetLetter(c);
			letter.SetPosition(i);
			letters[i] = letter;
			i++;
		}
	}

	// initiates solving of the word
	public void Solve ()
	{
		StartCoroutine (SolveWord());
	}

	// solve the word by turning all letters
	private IEnumerator SolveWord ()
	{
		foreach (Letter letter in letters)
		{
			letter.Turn ();
			yield return new WaitForSeconds (timeBetweenLetters);
		}
	}
}
