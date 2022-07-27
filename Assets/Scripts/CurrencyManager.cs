using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public GameObject player;
    public int currencyAmount = 0;
    public GameObject currencyText;
    private int displayCurrency;

    public GameObject currencyGain;

    public bool developer = false;
    public bool reset = false;

    private float _t;
    private float interval = 0.02f;
    public int amountBorder;
    private int increaseTo;
    private int decreaseTo;

    private bool startUpdate = false;
    private bool decreaseAmount = false;
    private bool increaseAmount = false;

    private int currencyCollected = 20;
    public GameObject currencyCollectedParticle;
    public GameObject currencyParticle;
    public GameObject[] currencyCollectedPop;
    public GameObject obstacleSpawner;

    void Start()
    {
        if (developer) {
            PlayerPrefs.SetInt("Currency", 100000);
        }

        if (reset) {
            PlayerPrefs.SetInt("Currency", 0);
        }

        currencyAmount = PlayerPrefs.GetInt("Currency");
        currencyText.GetComponent<TextMeshProUGUI>().SetText(currencyAmount.ToString());
    }

    private void Update() {

        if (startUpdate) {
            if (_t < interval)
                _t += Time.deltaTime;
            else {
                _t -= interval;

                if (decreaseAmount) {
                    int currentProgress = displayCurrency - decreaseTo;
                    if (currentProgress > 1000)
                        displayCurrency -= 100;
                    else if (currentProgress > 100)
                        displayCurrency -= 50;
                    else if (currentProgress > 10)
                        displayCurrency -= 5;
                    else if(currentProgress >=1)
                        displayCurrency -= 1;
                }

                if (increaseAmount) {
                    interval = 0.03f;
                    int currentProgress = increaseTo - displayCurrency;
                    if (currentProgress > 1000)
                        displayCurrency += 50;
                    else if (currentProgress > 100)
                        displayCurrency += 15;
                    else if (currentProgress > 10)
                        displayCurrency += 2;
                    else if (currentProgress >= 0)
                        displayCurrency += 1;
                }

                currencyText.GetComponent<TextMeshProUGUI>().SetText(displayCurrency.ToString());
                
            }
        }
        if (displayCurrency == decreaseTo) {
            decreaseAmount = false;
            startUpdate = false;
        }

        if(displayCurrency == increaseTo) {
            increaseAmount = false;
            startUpdate = false;
        }

        if (player.GetComponent<Player>().getMoney) {
            currencyCollectedParticle.transform.position = obstacleSpawner.GetComponent<ObstacleSpawner>().currencyPrefab.transform.position;
            currencyCollectedParticle.SetActive(true);

            IncreaseCurrencyByAmount(currencyCollected);

            foreach (GameObject o in currencyCollectedPop)
                    o.transform.position = new Vector3(o.transform.position.x, currencyCollectedParticle.transform.position.y, o.transform.position.z);
        
            switch (currencyCollected) {
                case 20:    currencyCollectedPop[0].gameObject.SetActive(true);
                    break;
                case 40:
                    currencyCollectedPop[1].gameObject.SetActive(true);
                    break;
                case 60:
                    currencyCollectedPop[2].gameObject.SetActive(true);
                    break;
                case 80:
                    currencyCollectedPop[3].gameObject.SetActive(true);
                    break;
                case 100:
                    currencyCollectedPop[4].gameObject.SetActive(true);
                    break;
            }

            if (currencyCollected<=80)
                currencyCollected += 20;

            player.GetComponent<Player>().getMoney = false;
        }
    }

    public void IncreaseCurrencyByAmount(int amount) {
        currencyAmount = PlayerPrefs.GetInt("Currency");

        currencyGain.SetActive(false);
        currencyGain.GetComponent<TextMeshProUGUI>().text = "+" + amount;
        currencyGain.SetActive(true);

        if (amount == 1) {
            currencyAmount += amount;
            currencyText.GetComponent<TextMeshProUGUI>().SetText(currencyAmount.ToString());
        } else {

            displayCurrency = currencyAmount;
            currencyAmount += amount;
            increaseTo = currencyAmount;

            increaseAmount = true;
            startUpdate = true;
        }
        PlayerPrefs.SetInt("Currency", currencyAmount);
    }

    public void IncreaseCurrencyByTen() {
        currencyAmount = PlayerPrefs.GetInt("Currency");
        currencyAmount+=10;
        currencyText.GetComponent<TextMeshProUGUI>().SetText(currencyAmount.ToString());
        PlayerPrefs.SetInt("Currency", currencyAmount);
    }

    public void ReduceCurrency(int amount) {
        displayCurrency = currencyAmount;
        currencyAmount -= amount;

        if (decreaseTo == 0)
            decreaseTo = currencyAmount;
        else
            decreaseTo -= amount;

        decreaseAmount = true;
        startUpdate = true;
        

    }

}