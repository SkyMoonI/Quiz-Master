using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
	[Header("Questions")]
	[SerializeField] TextMeshProUGUI questionText;
	[SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
	QuestionSO currentQuestion;

	[Header("Answers")]
	[SerializeField] GameObject[] answerButtons;
	int correctAnswerIndex;
	bool hasAnsweredEarly;

	[Header("Button Sprites")]
	[SerializeField] Sprite defaultButtonSprite;
	[SerializeField] Sprite correctAnswerButtonSprite;

	[Header("Timer")]
	[SerializeField] Image timerImage;
	Timer timer;

	[Header("Scoring")]
	[SerializeField] TextMeshProUGUI scoreText;
	ScoreKeeper scoreKeeper;

	void Start()
	{
		timer = FindObjectOfType<Timer>();
		scoreKeeper = FindObjectOfType<ScoreKeeper>();
	}

	void Update()
	{
		timerImage.fillAmount = timer.fillFraction;
		if (timer.loadNextQuestion)
		{
			hasAnsweredEarly = false;
			GetNextQuestion();
			timer.loadNextQuestion = false;
		}
		else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
		{
			DisplayAnswer(-1);
			SetButtonState(false);
		}
	}
	void ChangeButtonText()
	{
		for (int i = 0; i < answerButtons.Length; i++)
		{
			answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = currentQuestion.GetAnswer(i);
		}
	}
	void DisplayQuestion()
	{
		questionText.text = currentQuestion.GetQuestion();
		ChangeButtonText();
	}
	void GetNextQuestion()
	{
		if (questions.Count > 0)
		{
			SetButtonState(true);
			SetDefaultButtonSprite();
			GetRandomQuestion();
			DisplayQuestion();
			scoreKeeper.IncrementQuestionsSeen();
		}

	}
	void GetRandomQuestion()
	{
		int index = UnityEngine.Random.Range(0, questions.Count);
		currentQuestion = questions[index];
		questions.RemoveAt(index);
	}

	void SetDefaultButtonSprite()
	{
		Image buttonImage;
		for (int i = 0; i < answerButtons.Length; i++)
		{
			buttonImage = answerButtons[i].GetComponent<Image>();
			buttonImage.sprite = defaultButtonSprite;
		}
	}

	public void OnAnswerSelected(int index)
	{
		hasAnsweredEarly = true;
		DisplayAnswer(index);
		SetButtonState(false);
		timer.CancelTimer();
		scoreText.text = "Score: " + scoreKeeper.CalculateScore().ToString() + "%";
	}

	private void DisplayAnswer(int index)
	{
		Image buttonImage;
		if (index == currentQuestion.GetCorrectAnswerIndex())
		{
			questionText.text = "Correct";
			buttonImage = answerButtons[index].GetComponent<Image>();
			buttonImage.sprite = correctAnswerButtonSprite;
			scoreKeeper.IncrementCorrectAnswers();
		}
		else
		{
			correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
			string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
			questionText.text = "Correct answer was:\n" + correctAnswer;
			buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
			buttonImage.sprite = correctAnswerButtonSprite;
		}
	}

	void SetButtonState(bool state)
	{
		for (int i = 0; i < answerButtons.Length; i++)
		{
			Button button = answerButtons[i].GetComponent<Button>();
			button.interactable = state;
		}

	}
}
