using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
    private CarSpawner spawner;
    private float speed;
    public bool passed;
    public bool wait;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<CarSpawner>();
        speed = spawner.speed;
        spawner.carsEntered += 1;
        passed = false;
        wait = false;
    }

    // Update is called once per frame
    void Update()
    {
        float zMove = Time.deltaTime * speed;
        if (transform.position.z < 0 && !wait)
        {
            StartCoroutine(Wait());
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - zMove);
        }

        if (transform.position.z < 0 && !passed && wait)
        {
            passed = true;
            spawner.carsPassed += 1;
        }
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(spawner.waitTime);
        wait = true;
    }
}
