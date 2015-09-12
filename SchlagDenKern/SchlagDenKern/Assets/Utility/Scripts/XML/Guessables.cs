using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("guessablesCollection")]
public class Guessables<T> {

	/*
	 * List of all guessables
	 */ 
	[XmlArray("guessables"), XmlArrayItem("guessable")]
	public List<T> guessablesList;

	/*
	 * Determines if the list is ordered or in a random order
	 */ 
	[XmlElement("ordered")]
	public bool ordered;

	public Guessables() 
	{
		guessablesList = new List<T>();
	}
}