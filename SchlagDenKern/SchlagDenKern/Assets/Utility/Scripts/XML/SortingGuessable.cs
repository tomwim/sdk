using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public class SortingGuessable : Guessable 
{
	
	[XmlElement("task")]
	public string task 
	{
		get;
		set;
	}
	
	[XmlElement("top_label")]
	public string topLabel 
	{
		get;
		set;
	}
	
	[XmlElement("bottom_label")]
	public string bottomLabel 
	{
		get;
		set;
	}
	
	[XmlArray("elements"), XmlArrayItem("element")]
	public List<Element> elements;
	
	public class Element 
	{
		
		[XmlAttribute("starting_element")]
		public bool startingElement;
		
		[XmlElement("text")]
		public string text 
		{
			get;
			set;
		}
		
		[XmlElement("position")]
		public int position 
		{
			get;
			set;
		}
		
	}
}
