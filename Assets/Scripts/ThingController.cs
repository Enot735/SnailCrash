using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingController : MonoBehaviour
{
    private float AngleInDegrees = 45f;
    private float Power = 50f;

    private Transform TargetTransform;
    private Transform SpawnTransform;
    private Transform TargetTower2Transform;

    private Transform[] TargetArr = new Transform[4];

    private GameObject newThing;

    //float g = Physics.gravity.y;
    float g = -20f;

    private float CalculateSpeed(Transform TargetTransform)
    {
        Vector3 fromTo = TargetTransform.position - transform.position;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleInDegrees * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        return Mathf.Sqrt(Mathf.Abs(v2)) * Power;
    }

    public void AddForceToThing(GameObject newThing_1, Transform TargetTransform_1, Transform SpawnTransform_1, Transform TargetTower2Transform_1)
    {

        TargetTransform = TargetTransform_1;
        SpawnTransform = SpawnTransform_1;
        TargetTower2Transform = TargetTower2Transform_1;
        newThing = newThing_1;

        float v = CalculateSpeed(TargetTransform);

        Debug.Log("v = " + v);

        Rigidbody baseOfThing = newThing.transform.GetChild(0).GetComponent<Rigidbody>();

        baseOfThing.AddForce(-SpawnTransform.right * v, ForceMode.Acceleration);
        
    }

    public void AddForceToThing(GameObject newThing_1, Transform[] TargetArr_1)
    {

        //TargetTransform = TargetTransform_1;
        //SpawnTransform = SpawnTransform_1;
        //TargetTower2Transform = TargetTower2Transform_1;
        newThing = newThing_1;

        TargetArr = TargetArr_1;

        float v = CalculateSpeed(TargetArr[0]);

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
        //Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "BaseG")
        {
            float v = CalculateSpeed(TargetTower2Transform);
            v -= 100;
            Debug.Log("v_2 = " + v);
            Rigidbody baseOfThing = newThing.transform.GetChild(0).GetComponent<Rigidbody>();
            baseOfThing.AddForce(-SpawnTransform.right * v, ForceMode.Acceleration);

        }

        if (collision.gameObject.name == "BaseT2")
        {
            Destroy(newThing);
        }
    }
}