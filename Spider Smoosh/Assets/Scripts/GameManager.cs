using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
	// GameManager instance Stuff.
	public static GameManager Instance { get; set; }

	//	The HighScoreValue GameObject in the Main Menus Canvas>HighScore
	private GameObject highScoreText;
	//	The Pannel that is show after watching a rewarded video.
	public GameObject rewardPanel;

	void Awake()
	{   // GameManager instance Stuff.
		//Check if instance already exists
		if (Instance == null)
		{
			//if not, set instance to this
			Instance = this;
		}
		//If instance already exists and it's not this:
		else if (Instance != this)
		{

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);

		}
		DontDestroyOnLoad(gameObject);
		// GameManager instance Stuff End.
	}

	private void Start()
	{
		//	Disable the reward panel at runtime.
		rewardPanel.SetActive(false);

		//	If the player hasnt played before
		if(PlayerPrefs.HasKey("HasPlayedBefore") == false)
		{
			//	Run the FirstTimePlaying method.
			FirstTimePlaying();
		}
	}

	public void Update()
	{
		//	Display the high score on the main menu scene
		Scene currentScene = SceneManager.GetActiveScene();
		//	Get the current scenes name.
		string sceneName = currentScene.name;
		//	If we are on the main menu, update the High Score.
		if(sceneName == "MainMenu")
		{
			UpdateHighScore();
		}
	}

	private void FirstTimePlaying()
	{
		//	Set the players spray count to 5 if its the first time playing.
		PlayerPrefs.SetInt("SprayCount", 5);
		//	Create a PlayerPrefs key for the "HasPlayedBefore" string
		PlayerPrefs.SetString("HasPlayedBefore", "Yes");
	}

	

	private void UpdateHighScore()
	{
		//	Get access to the high score text componant.
		TextMeshProUGUI highScoreText = GameObject.Find("HighScoreValue").GetComponent<TextMeshProUGUI>();
		//	Store the high score value inside a temp float "highScore".
		float highScore = PlayerPrefs.GetFloat("HighScore", 0);
		//	Make the time show in minutes and seconds.
		string minutes = ((int)highScore / 60).ToString("00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		string seconds = (highScore % 60).ToString("00.00"); // Used to have the timer show in seconds and minutes rather that just seconds.
		//	Change the high score text componant to our high score.
		highScoreText.text = minutes + ":" + seconds;
	}

	#region Reward Video Stuff
	public void RewardPlayer()
	{
		int sprayCount = PlayerPrefs.GetInt("SprayCount", 0);
		//Debug.Log("Spray count was " + sprayCount);
		sprayCount++;
		//Debug.Log("Spray clount now is " + sprayCount);
		PlayerPrefs.SetInt("SprayCount", sprayCount);
		//Debug.Log("Spray count from player prefs is " + PlayerPrefs.GetInt("SprayCount", 0));
		rewardPanel.SetActive(true);
	}

	public void ShowLeaderboard()
	{
		SoundManager.Instance.ButtonSFX();
		//GooglePlayManager.ShowLeaderboardUI();
	}

	public void CloseRewardPanel()
	{
		rewardPanel.SetActive(false);
	}
	#endregion

	public void LoadMainMenu()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Load the main menu scene.	
		SceneManager.LoadScene(0);
	}

	public void LoadLevel()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Load the level scene.
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	Quit the game
		Application.Quit();
	}

	public void PrivacyPolicy()
	{
		//	Play the button SFX
		SoundManager.Instance.ButtonSFX();
		//	The privacy policy link.
		Application.OpenURL("https://burninghairstudios.wordpress.com/");
	}
}
