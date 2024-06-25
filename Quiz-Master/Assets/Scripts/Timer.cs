using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
	[SerializeField] float timeToCompleteQuestion = 10f;
	[SerializeField] float timeToShowCorrectAnswer = 3f;

	public bool loadNextQuestion = false;
	public float fillFraction;
	public bool isAnsweringQuestion = false;

	float timerValue;


	void Update()
	{
		UpdateTimer();
	}
	public void CancelTimer()
	{
		timerValue = 0;
	}
	void UpdateTimer()
	{
		timerValue -= Time.deltaTime;
		if (isAnsweringQuestion)
		{
			if (timerValue > 0)
			{
				fillFraction = timerValue / timeToCompleteQuestion; // will be between 0 and 1. 5/10 = 0.5
			}
			else
			{
				isAnsweringQuestion = false;
				timerValue = timeToShowCorrectAnswer;
			}
		}
		else
		{
			if (timerValue > 0)
			{
				fillFraction = timerValue / timeToShowCorrectAnswer; // will be between 0 and 1. 5/10 = 0.5
			}
			else
			{
				isAnsweringQuestion = true;
				timerValue = timeToCompleteQuestion;
				loadNextQuestion = true;
			}
		}
		Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
	}
}
