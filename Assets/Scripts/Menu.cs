using System.Collections;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSound;

    public void StartGame(){
        Debug.Log("Start button");
        StartCoroutine(WaitForSound());
    }

    public void Settings(){
        Debug.Log("Settings button");
        menuSound.Play();
    }

    public void Quit(){
        Debug.Log("Quit pressed");
        Application.Quit();
        /*
        if (Application.isEditor){
            EditorApplication.ExitPlaymode();
        }
        else{
            Application.Quit();
        }*/
    }

    IEnumerator WaitForSound(){
		menuSound.Play();
		yield return new WaitForSeconds((float)1.5);
		SceneManager.LoadSceneAsync(1);
	}
}
