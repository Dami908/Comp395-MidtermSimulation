using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    public GameObject window;
    public float serviceProbability; //probability that car will enter service. p
    public float serviceSpeed; // how many minutes per car. example is 1. mean interservice time
                               
    private float distance; //15 units in current setup
    public float updateTime; //equivalent to 1 min. Lets equate this to 2s for now
    public float speed; //in cars/minute
    public float currentTime;
    public float minutesSinceServiceStart;
    public int carsPassed;
    public int carsEntered;

    public Text pValueText;
    public Text meanInterserviceTimeText;
    public Text minutesElapsedText;
    public Text carsEnteredText;
    public Text carsPassedText;
    public Text carsInSystemText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = updateTime;
        distance = transform.position.z - window.transform.position.z;
        speed = (distance / updateTime) / serviceSpeed;
        minutesSinceServiceStart = 0f;
        carsPassed = 0;

        pValueText.text = "p = " + serviceProbability;
        meanInterserviceTimeText.text = "Using " + serviceSpeed + " minutes per car";
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
           if (Random.value < serviceProbability)
            {
                Instantiate(car, transform.position, Quaternion.identity);
            }
            currentTime = updateTime;
            minutesSinceServiceStart += 1;
        }
        minutesElapsedText.text = minutesSinceServiceStart + " mins since service start";
        carsEnteredText.text = "Cars entered: " + carsEntered;
        carsPassedText.text = "Cars passed: " + carsPassed;
        carsInSystemText.text = "Cars in system: " + (carsEntered - carsPassed);

    }
}
