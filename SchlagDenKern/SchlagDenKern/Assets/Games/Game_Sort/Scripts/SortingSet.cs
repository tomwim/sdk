using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SortingSet : MyMonoBehaviour 
{
	public Text topLabel;
	public Text bottomLabel;

	/* label at the top of the list */
	public string TopLabelText 
	{
		get
		{
			return this.TopLabelText;
		}
		set 
		{
			this.TopLabelText = value;
			topLabel.text = value;
		}
	}

	/* label at the bottom of the list */
	public string BottomLabelText 
	{
		get
		{
			return this.BottomLabelText;
		}
		set 
		{
			this.BottomLabelText = value;
			bottomLabel.text = value;
		}
	}

	private string[] elements;

	public override void StartMe ()
	{
		base.StartMe ();
		elements = new string[9];
	}

}
