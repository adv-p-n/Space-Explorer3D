using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0f, 1f)] float movementFactor;
    [SerializeField] float period;

    Vector3 startingPosition;
    void Start()
    {
        startingPosition= transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period;           //continually growing over time
        const float tau = Mathf.PI * 2;            //constant value of 6.283 or 2pi
        float rawSinWave = Mathf.Sin(cycles * tau);//going from -1 to 1

        movementFactor = (rawSinWave + 1) / 2;     //recalculated to go from 0 to 1


        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
