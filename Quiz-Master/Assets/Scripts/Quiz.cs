using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI questionText;
    [SerializeField]
    QuestionSO question;
    [SerializeField]
    GameObject[] answerButtons;

    void Start()
    {
        questionText.text = question.GetQuestion();
        ChangeButtonText();

    }

    void ChangeButtonText()
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.GetAnswer(i);
        }
    }
}
