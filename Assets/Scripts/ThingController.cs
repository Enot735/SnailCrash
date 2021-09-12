using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{
    private float AngleInDegrees = 45f;
    private float Power = 50f;

    private Transform[] TargetArrIn = new Transform[4];

    private GameObject newThingIn;

    //float g = Physics.gravity.y;
    float g = -20f;

    public void InitTargetsTransform(GameObject newThing, Transform[]  TargetArr)
    {
        TargetArrIn = TargetArr;
        newThingIn = newThing;
    }

    private float CalculateSpeed(Transform TargetTransform, Transform SpawnTransform)
    {
        Debug.Log("transform.position = " + transform.position);

        //Vector3 fromTo = TargetTransform.position - transform.position;
        Vector3 fromTo = TargetTransform.position - SpawnTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        return Mathf.Sqrt(Mathf.Abs(v2)) * Power;
    }

    public void AddForceToThing(GameObject newThing, Transform TargetTransform, Transform SpawnTransform)
    {

        float v = CalculateSpeed(TargetTransform, SpawnTransform);

        Debug.Log("v = " + v);

        Rigidbody baseOfThing = newThing.transform.GetChild(0).GetComponent<Rigidbody>();

        baseOfThing.AddForce(-SpawnTransform.right * v, ForceMode.Acceleration);
        
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isUsed_0 = false;
        bool isUsed_1 = false;
        bool isUsed_2 = false;
        bool isUsed_3 = false;

        if (collision.gameObject.name == "BaseG" && (!isUsed_0 || !isUsed_1 || !isUsed_2 || !isUsed_3))
        {

            if (!isUsed_0)
            {
                AddForceToThing(newThingIn, TargetArrIn[0], newThingIn.transform);
                isUsed_0 = true;
                return;
            }

            if (!isUsed_1)
            {
                AddForceToThing(newThingIn, TargetArrIn[1], newThingIn.transform);
                isUsed_1 = true;
                return;
            }

            if (!isUsed_2)
            {
                AddForceToThing(newThingIn, TargetArrIn[2], newThingIn.transform);
                isUsed_2 = true;
                return;
            }

            if (!isUsed_3)
            {
                AddForceToThing(newThingIn, TargetArrIn[3], newThingIn.transform);
                isUsed_3 = true;
                return;
            }

        }

        if (collision.gameObject.name == "BaseT2")
        {
            Destroy(newThingIn);
        }
    }
}