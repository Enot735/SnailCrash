using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1Controller : MonoBehaviour
{
    public Transform SpawnTransform;
    public Transform TargetTransform;
    public Transform TargetTower2Transform;

    public Transform[] TargetArr = new Transform[4];

    public GameObject Thing;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newThing = Instantiate(Thing, SpawnTransform.position, SpawnTransform.rotation);

        ThingController thScript = newThing.transform.GetChild(0).gameObject.GetComponent<ThingController>();
        
        thScript.InitTargetsTransform(newThing, TargetArr);
    }

    // Update is called once per frame
    void Update()
    {

        

    }
}
