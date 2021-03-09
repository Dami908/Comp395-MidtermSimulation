﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishSpawner : MonoBehaviour
{
    public GameObject fish;
    public Text numberOfFishesText;
    public Text currentTimeText;
    public Text harvestStrategyTest;
    public Text currentFishAdd;
    public Text currentFishSubtract;
    public Text updatingTimeText;
    public float appR = 0.01f;
    public float appK = 1000f;
    public float appDt = 10f;
    public float currentTime;
    public float currentFish;
    public float updateTime;
    private float oldFish;
    public float fishAdd;
    public float fishSubtract;
    public HarvestStrategy harvestStrategy = HarvestStrategy.NoHarvesting;
    public System.Func<float, float, float, float, float> F_NoHarvesting_Gain = (old, r, k, dt) => dt * old * r * (1 - (old/k));
    public System.Func<float, float, float, float> F_SubcriticalHarvest_Loss = (r, k, dt) => r * k * dt / 8;
    public System.Func<float, float, float, float> F_SupercriticalHarvest_Loss = (r, k, dt) => r * k * dt / 2;

    public enum HarvestStrategy
    {
        NoHarvesting,
        SubcriticalHarvesting,
        SupercriticalHarvesting
    }

    // Start is called before the first frame update
    void Start()
    {
        currentFish = 500;
        oldFish = 0;
        SpawnFishes(oldFish, currentFish);
        updateTime = appDt;
        harvestStrategyTest.text = "Harvest Strategy: " + harvestStrategy.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        updateTime -= Time.deltaTime;
        numberOfFishesText.text = "Number of fishes: " + (int)currentFish;
        currentTimeText.text = "Current time: " + currentTime.ToString("F");
        updatingTimeText.text = "Updating in: " + updateTime.ToString("F");


        if (updateTime < 0)
        {
            oldFish = currentFish;
            fishAdd = F_NoHarvesting_Gain(oldFish, appR, appK, appDt);
            switch (harvestStrategy){
                case HarvestStrategy.NoHarvesting:
                    fishSubtract = 0;
                    break;
                case HarvestStrategy.SubcriticalHarvesting:
                    fishSubtract = F_SubcriticalHarvest_Loss(appR, appK, appDt);
                    break;
                case HarvestStrategy.SupercriticalHarvesting:
                    fishSubtract = F_SupercriticalHarvest_Loss(appR, appK, appDt);
                    break;
                default:
                    break;
            }
            currentFish = currentFish + fishAdd - fishSubtract;
            SpawnFishes(oldFish, currentFish);
            updateTime = appDt;

            currentFishAdd.text = "Fish added: " + (int)fishAdd;
            currentFishSubtract.text = "Fish harvested: " + (int)fishSubtract;

        }
    }

    void SpawnFishes(float oldFish, float newFish)
    {
        int currentFishObjects = (int)(newFish / 10);
        int oldFishObjects = (int)(oldFish / 10);
        int deltaFishObjects = currentFishObjects - oldFishObjects;
        if (deltaFishObjects > 0)
        {
            while (deltaFishObjects > 0)
            {
                var position = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(0, 9f), Random.Range(-4.5f, 4.5f));
                Instantiate(fish, position, Quaternion.identity);
                deltaFishObjects--;
            }
        }
        else
        {
            while (deltaFishObjects < 0)
            {
                Destroy(GameObject.FindWithTag("fish"));
                deltaFishObjects++;

            }
        }

    }

    
}
