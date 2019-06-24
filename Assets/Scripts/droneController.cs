using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneController : MonoBehaviour
{
    public Transform currentDesiredPos;
    public float droneVelo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentDesiredPos.position, droneVelo);
    }
}
