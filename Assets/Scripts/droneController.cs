using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneController : MonoBehaviour
{
    public Transform currentDesiredTransform;
    public float droneVelo;
    public List<Transform> posicoesDroneNoMundo;
    public Transform playerFollowPos;
    private int currentPosIndex;
    private float distancia;

    // Start is called before the first frame update
    void Start()
    {
        currentPosIndex = 0;
        currentDesiredTransform = posicoesDroneNoMundo[0].transform;
        distancia = Vector3.Distance(transform.position, currentDesiredTransform.position);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentDesiredTransform.position, droneVelo);
    }

    void GoToNextDesiredSpot()
    {
        currentDesiredTransform = posicoesDroneNoMundo[currentPosIndex];
        distancia = Vector3.Distance(transform.position, currentDesiredTransform.position);
    }
    void ChangeToPlayerPos()
    {
        currentDesiredTransform = playerFollowPos;
        distancia = Vector3.Distance(transform.position, currentDesiredTransform.position);
    }
    void goToSpecificPos(Transform objeto)
    {
        currentDesiredTransform = objeto.transform;
        distancia = Vector3.Distance(transform.position, currentDesiredTransform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTROU NUM TRIGGER");
        if (collision.gameObject.tag == "eventPlayer")
        {
            playerFollowPos = GameObject.FindGameObjectWithTag("dronePlayerPos").transform;
            ChangeToPlayerPos();
        }
        if(collision.gameObject.tag == "droneDesiredPos")
        {
            goToSpecificPos(collision.gameObject.transform);
        }
    }
    
}
