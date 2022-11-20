using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundMask;

    // private void Awake()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public bool IsTouchingGround()
    {
        return Physics.CheckSphere(transform.position, groundCheckRadius, groundMask);
    }
}
