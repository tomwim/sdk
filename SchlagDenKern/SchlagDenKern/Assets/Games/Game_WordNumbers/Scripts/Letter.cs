using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Letter : MyMonoBehaviour {

	public float turningDuration;
	private Image letter;
	private Image number;
	private int num;
	private RectTransform rectTrans;

	public void SetLetter (char c)
	{
		letter = transform.GetChild(0).GetComponent<Image>() as Image;
		number = transform.GetChild(1).GetComponent<Image>() as Image;

		num = (int) c - 'A' + 1;
		number.transform.FindChild("Text").GetComponent<Text>().text = num.ToString();
		letter.transform.FindChild("Text").GetComponent<Text>().text = c.ToString();
	}

	public void SetPosition (int position)
	{
		rectTrans = GetComponent<RectTransform>();
		rectTrans.anchoredPosition = new Vector2 (position * rectTrans.sizeDelta.x + (rectTrans.sizeDelta.x / 2), 0);
	}

	public void Turn ()
	{
		StartCoroutine (CoTurn());
	}

	private IEnumerator CoTurn ()
	{
		bool turning = true;
		bool changed = false;
		Quaternion startRotation = transform.rotation;
		Quaternion targetRotation = Quaternion.Euler (transform.eulerAngles + new Vector3(0f, -180f, 0f));
		float currentLerpTime = 0f;
		while (turning) 
		{
			currentLerpTime += Time.deltaTime;
			
			if (currentLerpTime > turningDuration) {
				currentLerpTime = turningDuration;
			}
			
			float t = currentLerpTime / turningDuration;
			t = t * t * t * (t * (6f * t - 15f) + 10f);

			Quaternion newRot = Quaternion.Lerp (startRotation, targetRotation, t);
			rectTrans.rotation = newRot;

			if(t >= 0.5f && !changed)
			{
				letter.transform.SetAsLastSibling();
				changed = true;
			}

			if(t == 1.0f) {
				turning = false;
			}

			yield return null;
		}
	}
}
