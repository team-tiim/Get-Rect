using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour {

	public void LoadScene(string scene)
	{
		Debug.Log("Starting " + scene);
		SceneManager.LoadScene(scene);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
