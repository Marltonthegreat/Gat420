using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAgent : Agent
{

    [SerializeField] Perception perception;
    [SerializeField] MeterUI meter;

    const float MIN_SCORE = 0.2f;

    Need[] needs;
    UtilityObject activeUtilityObject = null;
    
    public bool isUsingUtilityObject { get { return activeUtilityObject != null; } }
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
        
        if (activeUtilityObject == null)
        {
            var gameObjects = perception.GetGameObjects();
            List<UtilityObject> utilityObjects = new List<UtilityObject>();
            foreach (var go in gameObjects)
            {
                if (go.TryGetComponent(out UtilityObject utilityObject))
                {
                    utilityObject.visible = true;
                    utilityObject.score = GetUtilityObjectScore(utilityObject);
                    if (utilityObject.score > MIN_SCORE) utilityObjects.Add(utilityObject);
                }
            }

            // set active utility object to first utility object
            activeUtilityObject = (utilityObjects.Count() == 0) ? null : utilityObjects[0];
            if (activeUtilityObject != null)
            {
                StartCoroutine(ExecuteUtilityOBjects(activeUtilityObject));
            }
        }

    }

    private void LateUpdate()
    {
        meter.slider.value = happiness;
        meter.worldPosition = transform.position + Vector3.up * 4;
    }

    IEnumerator ExecuteUtilityOBjects(UtilityObject utilityObject)
    {
        // go to locaiton
        movement.MoveTowards(utilityObject.location.position);
        while (Vector3.Distance(transform.position, utilityObject.location.position) > 0.25f)
        {
            Debug.DrawLine(transform.position, utilityObject.location.position);
            yield return null;
        }

        // start effect
        print("start");
        if (utilityObject.effect != null) utilityObject.effect.SetActive(true);

        // wait duration
        yield return new WaitForSeconds(utilityObject.duration);

        // stop effect
        print("stop");
        if (utilityObject.effect != null) utilityObject.effect.SetActive(false);

        // apply
        ApplyUtilityObject(utilityObject);

        activeUtilityObject = null;

        yield return null;
    }

    void ApplyUtilityObject(UtilityObject utilityObject)
    {
        foreach (var effector in utilityObject.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if (need != null)
            {
                need.input += effector.change;
                need.input = Mathf.Clamp(need.input, -1, 1);
            }
        }
    }

    float GetUtilityObjectScore(UtilityObject utilityObject)
    {
        float score = 0;

        foreach (var effector in utilityObject.effectors)
        {
            Need need = GetNeedByType(effector.type);

            if (need != null)
            {
                float futureNeed = need.getMotive(need.input + effector.change);
                score += need.motive - futureNeed;
            }
        }

        return score;
    }

    Need GetNeedByType(Need.Type type)
    {
        return needs.First(need => need.type == type);
    }
}
