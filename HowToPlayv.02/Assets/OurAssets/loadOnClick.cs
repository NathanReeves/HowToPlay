using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadOnClick : MonoBehaviour {

	public void LoadByIndex (int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}

	public void QuitGame(){
		//source: http://answers.unity3d.com/questions/899037/applicationquit-not-working-1.html
		// save any game data here
		#if UNITY_EDITOR
		// Application.Quit() does not work in the editor so
		// UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

}
