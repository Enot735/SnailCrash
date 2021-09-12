using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{
    private float AngleInDegrees = 45f;
    private float Power = 50f;
    private float V;

    private Transform[] TargetArrIn = new Transform[4];

    private GameObject newThingIn;

    private bool isUsed_0 = false;
    private bool isUsed_1 = false;
    private bool isUsed_2 = false;
    private bool isUsed_3 = false;


    //float g = Physics.gravity.y;
    float g = -20f;

    public void InitTargetsTransform(GameObject newThing, Transform[]  TargetArr)
    {
        TargetArrIn = TargetArr;

        newThingIn = newThing;
    }

    public float CalculateSpeed(Transform TargetTransform, Transform SpawnTransform)
    {
        Vector3 fromTo = TargetTransform.position - SpawnTransform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        return Mathf.Sqrt(Mathf.Abs(v2)) * Power;
    }

    public void AddForceToThing(GameObject newThing, float V, Transform SpawnTransform)
    {

        Rigidbody baseOfThing = newThing.transform.GetChild(0).GetComponent<Rigidbody>();

        baseOfThing.AddForce(-SpawnTransform.right * V, ForceMode.Acceleration);
        
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
        if (collision.gameObject.name == "BaseG" && (!isUsed_0 || !isUsed_1 || !isUsed_2 || !isUsed_3))
            {

            if (!isUsed_1)
            {
                Debug.Log("newThingIn.transform.position = " + newThingIn.transform.position);
                Debug.Log("TargetArrIn[1] = " + TargetArrIn[0]);
                V = CalculateSpeed(TargetArrIn[0], newThingIn.transform);
                AddForceToThing(newThingIn, V, newThingIn.transform);
                isUsed_1 = true;
            }
            else
            {
                AddForceToThing(newThingIn, V, newThingIn.transform);
            }

        }

        if (collision.gameObject.name == "BaseT2")
        {
            Destroy(newThingIn);
        }
    }
}