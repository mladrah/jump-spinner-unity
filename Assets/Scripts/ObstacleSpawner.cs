using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Vector3 spawnPoint;
    public Vector3 spawnPointCurrency;
    public float intervall;
    public int startSize;
    public GameObject obstaclePrefab;
    public GameObject currencyPrefab;
    public GameObject currencyCollectedParticle;
    public int spawnOffset;
    public List<GameObject> obstacleList = new List<GameObject>();

    public int nextDifficulty;
    public int numberOfTimesIncreased;
    public int numberOfLaps;
    public bool startDifficulty = false;
    public float obstaclePosition;
    public float obstacleSpeed;
    public float obstacleBorder;

    public int count = 0;
    private int listIndex = 0;
    private int teleportIndex = 0;
    
    public Color col;

    private int dummyCount = 0;
    void Start()
    {
        currencyPrefab.transform.position = spawnPointCurrency;
        currencyPrefab.SetActive(true);

        for(int i=0; i<startSize; i++) {
            GameObject obstacle = Instantiate(obstaclePrefab, spawnPoint, Quaternion.identity);
            obstacleList.Add(obstacle);
            spawnPoint.y += intervall;
        }
    }

    void Update()
    {
        if (obstacleList[listIndex].GetComponent<Obstacle>().contact) {
            listIndex++;
            ThroughObstacle();

            dummyCount++;
            if(dummyCount %2==0)
                currencyCollectedParticle.SetActive(false);
        }

        if (!currencyPrefab.activeInHierarchy) {
            spawnPointCurrency.y += 45;
            currencyPrefab.transform.position = spawnPointCurrency;
            currencyPrefab.SetActive(true);
        }
    }
    
    private void MoveLastObstacle() {
        if (startDifficulty)
            IncreaseDifficulty();

        spawnPoint.x = obstacleList[teleportIndex].transform.position.x;
        obstacleList[teleportIndex].transform.position = spawnPoint;
        ChangeObstacleColor(teleportIndex);

        obstacleList[teleportIndex].GetComponent<Obstacle>().contact = false;
        spawnPoint.y += intervall;
        teleportIndex++;
        if (teleportIndex >= startSize) {
            teleportIndex = 0;
            startDifficulty = true;
        }
    }

    private void ChangeObstacleColor(int index) {

        col = ColorManagerObstacle.ChangeObstacleColor();

        obstacleList[teleportIndex].GetComponent<Obstacle>().left.GetComponent<SpriteRenderer>().color = col;
        obstacleList[teleportIndex].GetComponent<Obstacle>().right.GetComponent<SpriteRenderer>().color = col;

        obstacleList[teleportIndex].GetComponent<Obstacle>().leftGlow.GetComponent<SpriteRenderer>().color = col;
        obstacleList[teleportIndex].GetComponent<Obstacle>().rightGlow.GetComponent<SpriteRenderer>().color = col;
    }

    private void ThroughObstacle() {
        if (listIndex >= startSize)
            listIndex = 0;
        count++;
        if (count >= 4) {
            MoveLastObstacle();
        }
    }

    private void IncreaseDifficulty() {
        if (count == nextDifficulty)
            numberOfLaps += 2;
        if (numberOfTimesIncreased < numberOfLaps) {
            obstacleList[teleportIndex].GetComponent<Obstacle>().left.transform.position += new Vector3(obstaclePosition, 0, 0);
            obstacleList[teleportIndex].GetComponent<Obstacle>().right.transform.position -= new Vector3(obstaclePosition, 0, 0);

            obstacleList[teleportIndex].GetComponent<Obstacle>().leftBorder += obstacleBorder;
            obstacleList[teleportIndex].GetComponent<Obstacle>().rightBorder -= obstacleBorder;
            obstacleList[teleportIndex].GetComponent<Obstacle>().moveSpeed += obstacleSpeed;

            if (teleportIndex + 1 == startSize)
                numberOfTimesIncreased++;
        }
    }
 
}
