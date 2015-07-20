using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordNumbersController : GameController {

	public Word wordPrefab;
	public GameObject gameCanvas;
	public float switchDuration;

	private List<Word> words;
	private int currentWordIndex;

	public override void StartMe ()
	{
		base.StartMe ();
		currentWordIndex = 0;
		Init ();
	}

	public override void Solve ()
	{
		words[currentWordIndex].Solve();
	}

	public override void Next ()
	{
		currentWordIndex++;
		MoveWordsUp ();
	}

	private void MoveWordsUp ()
	{
		int i = 0;
		foreach(Word word in words) 
		{
			StartCoroutine(MoveWordUp(word, i));
			i++;
		}
	}

	private IEnumerator MoveWordUp (Word word, int position)
	{
		bool moving = true;
		
		RectTransform rectTrans = word.GetComponent<RectTransform>();
		Vector2 startPosition = rectTrans.anchoredPosition;
		Vector2 targetPosition = new Vector2(0, (position - currentWordIndex) * -((WordNumbersController) GameController.Instance).gameCanvas.GetComponent<RectTransform>().sizeDelta.y);
		float currentLerpTime = 0f;
		
		Debug.Log (startPosition + " " + targetPosition);
		while (moving) 
		{
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > switchDuration) {
				currentLerpTime = switchDuration;
			}
			
			float t = currentLerpTime / switchDuration;
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			Debug.Log (startPosition + " " + targetPosition + " " + t);
			Vector2 newPos = Vector2.Lerp (startPosition, targetPosition, t);
			//Debug.Log (newPos);
			rectTrans.anchoredPosition = newPos;
			
			if(t == 1.0f) {
				moving = false;
			}
			
			yield return null;
		}
	}

	/*
	 * Get all words from the XML and create them as Word-Objects
	 */ 
	private void Init ()
	{
		Guessables<Guessable> guessables = XMLHandler.GetGuessableList<Guessable>(GameLoadingController.Game.NUMBERWORDS);
		words = new List<Word> ();

		if(guessables.ordered)
		{
			guessables.guessablesList = XMLHandler.SortByOrder(guessables.guessablesList);
		}
		else 
		{
			guessables.guessablesList = XMLHandler.Shuffle(guessables.guessablesList);
		}

		int i = 0;
		foreach (Guessable w in guessables.guessablesList)
		{
			Word word = GameObject.Instantiate (wordPrefab) as Word;
			word.transform.SetParent(gameCanvas.transform, false);
			words.Add(word);
			word.CreateWord(w.result);
			word.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f , i * -((WordNumbersController) GameController.Instance).gameCanvas.GetComponent<RectTransform>().sizeDelta.y);

			i++;
		}
	}
}
