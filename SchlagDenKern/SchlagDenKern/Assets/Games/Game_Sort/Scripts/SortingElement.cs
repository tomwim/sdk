using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SortingElement : MyMonoBehaviour 
{
	public Text text;
	public float gap;
	public float movingDuration;

	public string Text
	{
		get
		{
			return this.Text;
		}
		set
		{
			this.Text = value;
			text.text = value;
		}
	}

	public string Position 
	{
		get;
		set;
	}

	public void MoveUp ()
	{
		StartCoroutine (CoMove (false));
	}

	public void MoveDown ()
	{
		StartCoroutine (CoMove (true));
	}

	/* Move the element either down or up before a new element is inserted */
	public IEnumerator CoMove(bool down)
	{
		float isMovingDownFactor = down ? -1 : 1;
		bool moving = true;
		Vector2 startPosition = text.rectTransform.anchoredPosition;
		Vector2 endPosition = startPosition + (new Vector2(0.0f, text.rectTransform.sizeDelta.y + gap) * isMovingDownFactor);
		float currentLerpTime = 0f;

		while (moving) 
		{
			currentLerpTime += Time.deltaTime;

			if(currentLerpTime > movingDuration)
			{
				currentLerpTime = movingDuration;
			}

			float t = currentLerpTime / movingDuration;
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			Vector2 newPos = Vector2.Lerp (startPosition, endPosition, t);
			text.rectTransform.anchoredPosition = newPos;

			if(text.rectTransform.anchoredPosition == endPosition)
			{
				moving = false;
			}

			yield return null;
		}
	}

}
