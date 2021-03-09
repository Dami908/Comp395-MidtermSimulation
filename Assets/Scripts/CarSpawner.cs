using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    public GameObject window;
    public float serviceProbability; //probability that car will enter service. p
    public int serviceSpeed; // how many minutes per car. example is 1. mean interservice time
                               
    private float distance; //15 units in current setup
    public float updateTime; //equivalent to 1 min. Lets equate this to 2s for now
    public float waitTime; //visually shows how long the wait should happen. We will set this to 0.5f.
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

    public InputField pInput;
    public InputField interserviceTimeInput;

    // Start is called before the first frame update
    void Start()
    {
        pValueText.text = "Probability of service "; //serviceProbability
        meanInterserviceTimeText.text = "Inter-service time ";
        CalculateServiceProbability();
        CalculateMeanInterServiceTime();
        meanInterserviceTimeText.text = "Using " + serviceSpeed + " minutes per car";

        currentTime = updateTime;
        distance = transform.position.z - window.transform.position.z;
        speed = (distance / (updateTime-waitTime)) / serviceSpeed;
        minutesSinceServiceStart = 0f;
        carsEntered = 0;
        carsPassed = 0;

    }

    void CalculateServiceProbability()
    {
        if (float.TryParse(pInput.text, out serviceProbability))
        {
            if (serviceProbability < 0)
            {
                serviceProbability = 0;
                pInput.text = "0";
            }
            if (serviceProbability > 1)
            {
                serviceProbability = 1;
                pInput.text = "1";
            }
        }
        else
        {
            serviceProbability = 0.5f;
            pInput.text = "0.5";

        }
    }
    void CalculateMeanInterServiceTime()
    {
        if (int.TryParse(interserviceTimeInput.text, out serviceSpeed))
        {
            if (serviceSpeed < 1)
            {
                serviceSpeed = 1;
                interserviceTimeInput.text = "1";
            }
        }
        else
        {
            serviceSpeed = 1;
            interserviceTimeInput.text = "1";

        }

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


    public void valuesChanged()
    {
        DeleteAllCars();
        Start();
    }



    void DeleteAllCars()
    {
        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
        for (int i = 0; i < cars.Length; i++)
        {
            Destroy(cars[i]);
        }
    }
}
