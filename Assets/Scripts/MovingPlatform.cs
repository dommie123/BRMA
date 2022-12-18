using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float changeDirectionDelay;
    [SerializeField] private float moveRange;
    [SerializeField] private Vector3 moveDirection;

    private float stopForSeconds;
    private float goForSeconds;
    private bool returnToOrigin;
    private Vector3 originPoint;
    private Rigidbody body;
    public BoxCollider trigger;

    private void Awake()
    {
        originPoint = transform.position;    
        stopForSeconds = 0f;
        moveDirection = moveDirection.normalized;
        returnToOrigin = false;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (HasReachedDestination())
        {
            stopForSeconds = changeDirectionDelay;
            goForSeconds = 2f;
            returnToOrigin = !returnToOrigin;

            InvertMoveDirection();
        }

        if (stopForSeconds > 0f)
        {
            stopForSeconds -= Time.deltaTime;
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            if (goForSeconds > 0f)
            {
                goForSeconds -= Time.deltaTime;
            }
        }
    }

    private void InvertMoveDirection()
    {
        moveDirection.x = -moveDirection.x;
        moveDirection.y = -moveDirection.y;
        moveDirection.z = -moveDirection.z;
    }

    private bool HasReachedDestination()
    {
        if (!returnToOrigin)
            return Vector3.Distance(transform.position, originPoint) >= moveRange && goForSeconds <= 0f;
        return Vector3.Distance(transform.position, originPoint) <= 0f && goForSeconds <= 0f;
    }

    private void OnTriggerStay(Collider other) 
    {
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (other.gameObject.layer != groundLayer)
            other.transform.parent = this.transform;
    }

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Trigger has left");
        int groundLayer = LayerMask.NameToLayer("Ground");

        if (other.gameObject.layer != groundLayer)
            other.transform.parent = null;
    }
}
