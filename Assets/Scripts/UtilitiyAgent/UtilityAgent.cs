using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAgent : Agent
{
    [SerializeField] MeterUI meter;
    Need[] needs;

    public float happiness
    {
        get
        {
            float totalMotive = 0;

            foreach (var need in needs)
            {
                totalMotive += need.motive;
            }

            return 1 - (totalMotive / needs.Length);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        needs = GetComponentsInChildren<Need>();

        meter.name = "HAPPINESS";
        meter.text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("speed", movement.velocity.magnitude);
        meter.slider.value = happiness;
        meter.worldPosition = transform.position + Vector3.up * 4;
    }
}
