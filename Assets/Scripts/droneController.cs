using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneController : MonoBehaviour
{
    public Vector3 currentDesiredPos;
    public float droneVelo;
    public List<Transform> posicoesDroneNoMundo;
    public Transform playerFollowPos;
    private int currentPosIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentPosIndex = 0;
        currentDesiredPos = posicoesDroneNoMundo[0].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentDesiredPos, droneVelo);
    }

    void GoToNextDesiredSpot()
    {
        currentDesiredPos = posicoesDroneNoMundo[currentPosIndex].position;
    }
    void ChangeToPlayerPos()
    {
        currentDesiredPos = playerFollowPos.position;
    }
    void goToSpecificPos(Transform objeto)
    {
        currentDesiredPos = objeto.position;
    }
}
