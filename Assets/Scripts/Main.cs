using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {
    //spawn objects
    public GameObject energyObject;

    //Controllers
    public GameObject cameraRig;
    public SteamVR_TrackedController left;
    public SteamVR_TrackedController right;

    //world elements
    List<GameObject> energys = new List<GameObject>();
    
    bool applyForce(SteamVR_TrackedController contr, GameObject particle)
    {
        if (contr.triggerPressed)
        {
            var direction = contr.transform.position - particle.transform.position;
            direction.Normalize();
            var c = Vector3.Angle(direction, particle.GetComponent<Rigidbody>().velocity);
            var power = 0.3f * c;
            particle.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Force);
        }
        return contr.triggerPressed;
    }

    bool applyForce2(SteamVR_TrackedController contr, GameObject particle)
    {
        if (contr.triggerPressed)
        {
            var direction = contr.transform.forward;
            direction.Normalize();
            var c = Vector3.Angle(direction, particle.GetComponent<Rigidbody>().velocity);
            var power = 0.3f * c;
            particle.GetComponent<Rigidbody>().velocity = direction * 2;
            //particle.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Force);
        }
        return contr.triggerPressed;
    }

    // Use this for initialization
    void Start () {
        for(var i = 0; i < 1; i++)
        {
            var obj = Instantiate(energyObject);
            obj.transform.position = new Vector3(i/3.0f, 0, 5);
            energys.Add(obj);
        }
        

        
    }

    private void FixedUpdate()
    {
        
            energys.ForEach((e) => {
                var appied = applyForce(right, e);
                var applied2 = applyForce2(left, e);
                if (!appied && !applied2)
                {
                    e.GetComponent<Rigidbody>().AddForce(-e.GetComponent<Rigidbody>().velocity * 0.1f);
                }
            });
        
    }

    // Update is called once per frame
    void Update () {
        
	}
}
