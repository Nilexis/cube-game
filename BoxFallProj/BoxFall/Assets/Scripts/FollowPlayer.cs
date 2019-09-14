using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 camOffset;
    public int dis = 1;
    private Vector3 camPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camPos = new Vector3(0, player.transform.position.y, player.transform.position.z);
        transform.position = camPos + camOffset*dis;
    }
}
