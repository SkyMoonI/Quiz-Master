using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	Quiz quiz;
	EndScreen endScreen;

	void Awake()
	{
		quiz = FindObjectOfType<Quiz>();
		endScreen = FindObjectOfType<EndScreen>();
	}
	void Start()
	{
		quiz.gameObject.GetComponent<Canvas>().enabled = true;
		endScreen.gameObject.GetComponent<Canvas>().enabled = false;

	}

	// Update is called once per frame
	void Update()
	{
		if (quiz.isComplete)
		{
			quiz.gameObject.GetComponent<Canvas>().enabled = false;
			endScreen.gameObject.GetComponent<Canvas>().enabled = true;
			endScreen.ShowFinalScore();
		}
	}

	public void OnReplayLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
