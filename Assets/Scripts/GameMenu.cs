using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseUI;
    public GameObject startUI;
    //public GameObject victoryUI;
    public GameObject endUI;
    [SerializeField] private TMP_Text totalTime;
    public GameObject deathUI;

    void Awake(){
        //subscribe to event 
        GameManager.OnGameStateChange += GameManagerOnGameStateChanged;
    }

    private void GameManagerOnGameStateChanged(GameState gameState)
    {
        //shows UI on start game
        if (gameState == GameState.start){
            PauseStart();
        } 
        else if (gameState == GameState.victory){
            //Victory();
            Debug.Log("victory");
        }
        else if (gameState == GameState.death){
            Death();
        }
        else if (gameState == GameState.end){
            EndGame();
        }
    }

    void OnDestroy(){
        //unsubscribe from event
        GameManager.OnGameStateChange -= GameManagerOnGameStateChanged;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameIsPaused){
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    //------------------------------Start Menu------------------------------

    void PauseStart(){
        startUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("pauseStart");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //------------------------------Victory Menu------------------------------

    /*
    private void Victory()
    {
        victoryUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("victoryUI open");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to next level
    public void NextLevel(){
        Debug.Log("New Level");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }*/

    //------------------------------End Menu------------------------------

    private void EndGame()
    {
        endUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("endUI open");

        float t = TimeScoreManager.Instance.GetTime();
		int mins = (int)( t / 60 );
		int rest = (int)(t % 60);
        totalTime.text = string.Format("Total Time - {0:D2}:{1:D2}", mins, rest);

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to main menu
    public void GoMainMenu(){
        Debug.Log("Main Menu");
        SceneManager.LoadSceneAsync(0);
    }

    //------------------------------Death Menu------------------------------

    private void Death()
    {
        deathUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("DeathUI open");

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }

    //method goes to restart level
    public void RestartLevel(){
        Debug.Log("Restart Level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //------------------------------Pause Menu------------------------------

    public void Resume (){
        pauseUI.SetActive(false);
        startUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
    }

    void Pause(){
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
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

}
