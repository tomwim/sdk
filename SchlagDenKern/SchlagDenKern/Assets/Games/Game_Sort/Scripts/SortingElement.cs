using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SortingElement : MyMonoBehaviour 
{
	public RectTransform rectTransform;
	public Text text;
	public float gap;
	public float movingDuration;
	public float showingDuration;
	public float waitingTimeBeforeShowing = 0.1f;

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

	public int Position 
	{
		get;
		set;
	}

	public int InsertedPosition 
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

	public void Insert (SortingElement referenceElement)
	{
		/* if reference element is above this element add position */
		float belowFactor = (referenceElement.InsertedPosition < InsertedPosition) ? 1f : -1f;

		Vector2 insertPosition = referenceElement.rectTransform.anchoredPosition + (new Vector2(0f, rectTransform.sizeDelta.y + gap) * belowFactor);

		StartCoroutine (CoInsert (insertPosition));
	}

	/* insert the element at a certain position */
	public IEnumerator CoInsert (Vector2 insertPosition)
	{
		bool inserting = true;
		bool showing = false;
		bool hiding = true;

		Vector2 startSize = rectTransform.sizeDelta;
		Vector2 endSize = Vector2.zero;

		float currentLerpTime = 0f;

		while (inserting) 
		{
			/* first hide element */
			while (hiding) {
				currentLerpTime += Time.deltaTime;

				if (currentLerpTime > showingDuration)
				{
					currentLerpTime = showingDuration;
				}

				float t = currentLerpTime / showingDuration;
				t = t * t * t * (t * (6f * t - 15f) + 10f);

				Vector2 newSize = Vector2.Lerp (startSize, endSize, t);
				rectTransform.sizeDelta = newSize;

				if (rectTransform.sizeDelta == endSize)
				{
					hiding = false;
					showing = true;
					currentLerpTime = 0;
				}
				yield return null;
			}

			/* then move hidden element to appropriate position and wait a short period of time */
			rectTransform.anchoredPosition = insertPosition;
			yield return new WaitForSeconds (waitingTimeBeforeShowing);

			/* then show hidden element again */
			while (showing)
			{
				currentLerpTime += Time.deltaTime;

				if(currentLerpTime > showingDuration)
				{
					currentLerpTime = showingDuration;
				}

				float t = currentLerpTime / showingDuration;
				t = t * t * t * (t * (6f * t - 15f) + 10f);

				Vector2 newSize = Vector2.Lerp (endSize, startSize, t);
				rectTransform.sizeDelta = newSize;

				if (rectTransform.sizeDelta == startSize)
				{
					showing = false;
					inserting = false;
				}

				yield return null;
			}
		}
	}

	/* Move the element either down or up before a new element is inserted */
	public IEnumerator CoMove(bool down)
	{
		float isMovingDownFactor = down ? -1f : 1f;
		bool moving = true;

		Vector2 startPosition = rectTransform.anchoredPosition;
		Vector2 endPosition = startPosition + (new Vector2(0.0f, rectTransform.sizeDelta.y + gap) * isMovingDownFactor);

		float currentLerpTime = 0f;

		while (moving) 
		{
			currentLerpTime += Time.deltaTime;

			if (currentLerpTime > movingDuration)
			{
				currentLerpTime = movingDuration;
			}

			float t = currentLerpTime / movingDuration;
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			Vector2 newPos = Vector2.Lerp (startPosition, endPosition, t);
			rectTransform.anchoredPosition = newPos;

			if(rectTransform.anchoredPosition == endPosition)
			{
				moving = false;
			}

			yield return null;
		}
	}

}
