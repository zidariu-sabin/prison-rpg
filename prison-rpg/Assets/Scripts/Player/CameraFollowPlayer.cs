using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public float smoothSpeed = 0.125f;

    private Vector3 _offset = new Vector3(0, 10, 0);
    // Start is called before the first frame update
   

    
    private void LateUpdate()
    {
        Vector3 desiredPosition = Player.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
    
}
