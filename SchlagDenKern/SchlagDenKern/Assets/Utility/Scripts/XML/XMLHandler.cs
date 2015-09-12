using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLHandler 
{
	/*
	 * Opens the XMLFile of a given game and returns the list of all guessables
	 */ 
	public static Guessables<T> GetGuessableList<T>(GameLoadingController.Game game)
	{
		string filePath = Application.dataPath.Remove(Application.dataPath.IndexOf("SchlagDenKern/Assets"));
		Debug.Log (filePath);
		
		switch(game)
		{
		case GameLoadingController.Game.NUMBERWORDS:
			filePath += "Resources/Games/NumberWords/numberWords.xml";
			break;
		case GameLoadingController.Game.SORT:
			filePath += "Resources/Games/Sort/sort.xml";
			break;
		default:
			filePath += "";
			break;
		}

		XmlSerializer serializer = new XmlSerializer(typeof(Guessables<T>));
		Stream stream = new FileStream(filePath, FileMode.Open);
		Guessables<T> guessables = serializer.Deserialize(stream) as Guessables<T>;
		stream.Close ();
		
		return guessables;
	}

	/*
	 * Shuffle a list randomly
	 */ 
	public static List<T> Shuffle<T>(List<T> list)  
	{  
		System.Random rng = new System.Random();  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rng.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}
		return list;
	}

	/*
	 * Sort a list of Guessables by their order number
	 */ 
	public static List<T> SortByOrder<T>(List<T> guessables) where T : Guessable
	{
		guessables.Sort(delegate(Guessable ga1, Guessable ga2) {
			if(ga1.order > ga2.order) {
				if(ga2.order < 0)
					return -1;
				return 1;
			}
			if(ga2.order > ga1.order) {
				if(ga1.order < 0)
					return 1;
				return -1;
			}
			return 0;
		});
		
		return guessables;
	}
}
