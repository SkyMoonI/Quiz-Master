using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
	int correctAnswer;
	int questionsSeen;

	public int GetCorrectAnswers()
	{
		return correctAnswer;
	}
	public int GetQuestionsSeen()
	{
		return questionsSeen;
	}
	public int CalculateScore()
	{
		return Mathf.RoundToInt(correctAnswer / (float)questionsSeen * 100f);
	}
	public void IncrementCorrectAnswers()
	{
		correctAnswer++;
	}
	public void IncrementQuestionsSeen()
	{
		questionsSeen++;
	}
}
