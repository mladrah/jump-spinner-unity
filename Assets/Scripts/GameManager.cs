using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject mainScreen;
    public GameObject gameOverScreen;
    public GameObject leaderboard;
    public GameObject shop;
    public GameObject inputFieldText;
    public GameObject inputView;
    public GameObject scoreTop;
    public GameObject fadeOut;
    public GameObject justTap;
    public Animator animator;

    [Header("UI Buttons")]
    public Button submitButtonGameOver;
    public Button[] mainButtons;
    public Button[] backButtons;
    public Button submitOKButton;

    [Header("Game Mechanics")]
    public GameObject player;
    public GameObject cam;
    public GameObject obstacleSpawner;
    public GameObject scoreManager;
    public GameObject currencyManager;

    private bool mainViewOpen = true;
    private string username;
    private bool once = true;
    private bool onUI;

    private static bool retry = false;

    private int money=1;
    private int scoreChecker=0;

    private void Awake() {
        Application.targetFrameRate = 300;
    }

    private void Start() {

        fadeOut.SetActive(true);

        if (retry) {
            mainScreen.SetActive(false);
        }

        justTap.SetActive(false);
        justTap.SetActive(true);

    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
       EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update() {
        if (mainViewOpen) {
            if (Input.touchCount == 1) {
                if ((Input.GetTouch(0).phase == TouchPhase.Began && !IsPointerOverUIObject()) || retry) {
                    mainViewOpen = false;
                    retry = false;
                    StartGame();

                }
            }
            
            if (((Input.GetKeyDown("w") || Input.GetMouseButtonDown(0)) && !IsPointerOverUIObject() ) || retry ) {
                mainViewOpen = false;
                retry = false;
                StartGame();

            }
        }

        if (player.GetComponent<Player>().dead) {
            EndGame();
        }

        if (obstacleSpawner.GetComponent<ObstacleSpawner>().count > scoreManager.GetComponent<ScoreManager>().score && !player.GetComponent<Player>().dead) {
            PassedThroughObstacle();
        }

        if (inputFieldText.GetComponent<TextMeshProUGUI>().text.Length > 1)
            submitOKButton.interactable = true;
        else
            submitOKButton.interactable = false;
    }

    public void StartGame() {
        foreach (Button btn in mainButtons)
            btn.interactable = false;

        player.GetComponent<Player>().jumpAllowed = true;
        animator.SetTrigger("start_game");
        shop.SetActive(false);
        leaderboard.SetActive(false);
    }

    public void EndGame() {
        cam.GetComponent<CameraMovement>().follow = false;
        scoreManager.GetComponent<ScoreManager>().CheckForHighScore();
        if (once) {
            animator.SetTrigger("end_game");
            gameOverScreen.SetActive(true);
            once = false;
        }
        leaderboard.SetActive(true);
        inputView.SetActive(true);
    }
    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Retry() {
        retry = true;
        Reset();
    }

    public void PassedThroughObstacle() {
        SoundManager.PlaySound(SoundManager.pointClip);
        scoreManager.GetComponent<ScoreManager>().IncreaseScore();
        scoreChecker++;
        currencyManager.GetComponent<CurrencyManager>().IncreaseCurrencyByAmount(money);
        if (scoreChecker % 2 == 0 && money < 25)
            money++;
    }

    public void submitHighscore() {
        username = inputFieldText.GetComponent<TextMeshProUGUI>().text;
        if (string.IsNullOrEmpty(username)||username.Equals("")||username==null)
            username = "entry_empty";
    
        username.Trim();
        
        inputView.SetActive(false);
        leaderboard.SetActive(true);
        submitButtonGameOver.interactable = false;
        Highscores.AddNewHighscore(username, scoreManager.GetComponent<ScoreManager>().score);
    }

    private void OnEnable() {
        foreach (Button btn in mainButtons)
            btn.onClick.AddListener(() => MainButtonClick(btn));

        foreach (Button btn in backButtons)
            btn.onClick.AddListener(() => BackButtonClick(btn));
    }

    private void MainButtonClick(Button btn) {
        mainViewOpen = false;
    }

    private void BackButtonClick(Button btn) {
        mainViewOpen = true;
    }
}
