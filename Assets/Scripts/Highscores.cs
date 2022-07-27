using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    const string privateCode = "RggcU_L-aUyMCKiAfCl3bAbzQf9J1itES_5Wcn4NBhVw";
    const string publicCode = "5d7bdf9fd1041303ec950ba8";
    const string webURL = "http://dreamlo.com/lb/";

    DisplayHighscores highscoreDisplay;
    public Highscore[] highscoresList;
    static Highscores instance;

    void Awake() {
        highscoreDisplay = GetComponent<DisplayHighscores>();
        instance = this;
    }

    public static void AddNewHighscore(string username, int score) {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score) {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error)) {
            print("Upload Successful");
            DownloadHighscores();
        } else {
            print("Error uploading: " + www.error);
        }
    }

    public void DownloadHighscores() {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator DownloadHighscoresFromDatabase() {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(webURL + publicCode + "/pipe/")) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError) {
                print("Error Downloading: " + webRequest.error);
            } else {
                FormatHighscores(webRequest.downloadHandler.text);
                highscoreDisplay.OnHighscoresDownloaded(highscoresList);
            }
        }
    }

    void FormatHighscores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
        }
    }

}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score) {
        username = _username;
        score = _score;
    }

}
