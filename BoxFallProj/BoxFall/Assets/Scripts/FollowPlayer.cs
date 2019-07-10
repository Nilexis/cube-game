using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;
    private Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camPos = player.transform.position;
        camPos.Set(0, 0, camPos.z);
        camPos += camOffset;
        transform.position = camPos + camOffset;
    }
}
