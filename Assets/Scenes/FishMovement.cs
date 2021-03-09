using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    private bool positive;
    // Start is called before the first frame update
    void Start()
    {
        positive = (Random.value > 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        if (position.x > 4.5f && positive)
        {
            positive = false;
        }
        if (position.x < -4.5f && !positive)
        {
            positive = true;
        }
        transform.position = new Vector3(position.x += positive ? 0.001f : -0.001f, position.y, position.z);

    }
}
