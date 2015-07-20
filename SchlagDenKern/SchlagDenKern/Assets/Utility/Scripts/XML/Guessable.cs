using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class Guessable 
{

	// solution of the guessable
	[XmlElement("result")]
	public string result 
	{
		get;
		set;
	}

	// order number - if not randomly chosen
	[XmlElement("order")]
	public int order 
	{
		get;
		set;
	}

	// extra information given - not necesseraly needed
	[XmlElement("info")]
	public int info 
	{
		get;
		set;
	}
	
}
