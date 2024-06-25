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
	[SerializeField] QuestionSO question;

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

	void Start()
	{
		timer = FindObjectOfType<Timer>();
		GetNextQuestion();
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
			answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswer(i);
		}
	}
	void DisplayQuestion()
	{
		questionText.text = question.GetQuestion();
		ChangeButtonText();
	}
	void GetNextQuestion()
	{
		SetButtonState(true);
		SetDefaultButtonSprite();
		DisplayQuestion();
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
	}

	private void DisplayAnswer(int index)
	{
		Image buttonImage;
		if (index == question.GetCorrectAnswerIndex())
		{
			questionText.text = "Correct";
			buttonImage = answerButtons[index].GetComponent<Image>();
			buttonImage.sprite = correctAnswerButtonSprite;
		}
		else
		{
			correctAnswerIndex = question.GetCorrectAnswerIndex();
			string correctAnswer = question.GetAnswer(correctAnswerIndex);
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
