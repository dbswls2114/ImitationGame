using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveMent : MonoBehaviour
{
    public static CameraMoveMent I;
    public GameObject Player;
    private Vector3 _offset = new Vector3(0,0,-10);
    private float _smoothSpeed = 0.125f;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if(Player == null) return;
        Vector3 desiredPosition = Player.transform.position + _offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

        transform.position = smoothedPosition;
    }

}
