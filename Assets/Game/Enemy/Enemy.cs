using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRange = 5f;
    public float speed = 2f;
    public float patrolDistance = 3f;
    public bool isMovingRight = true;

    private Transform playerTransform;
    private Vector2 startPosition;
    private Vector2 patrolDestination;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;
        patrolDestination = GetPatrolDestination();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position); //Distance between Player and Enemy

        if (distance < detectionRange)
        {
            // The Player is in range, move towards the player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            transform.position = (Vector2) transform.position + (direction * speed * Time.deltaTime);
        }
        else
        {
            // The Player is out of range, patrol in a straight line
            Vector2 direction = (patrolDestination - (Vector2)transform.position).normalized;
            transform.position = (Vector2) transform.position + (direction * speed * Time.deltaTime);

            // Check if we have reached the patrol destination, then turn around
            if (Vector2.Distance(transform.position, patrolDestination) < 0.1f)
            {
                isMovingRight = !isMovingRight;
                patrolDestination = GetPatrolDestination();
            }
        }
    }

    Vector2 GetPatrolDestination()
    {
        Vector2 destination;
        if (isMovingRight) {
            destination = startPosition + Vector2.right * patrolDistance;
        } else {
            destination = startPosition + Vector2.left * patrolDistance;
        }

        if (destination.x < startPosition.x - patrolDistance) {
            destination.x = startPosition.x - patrolDistance;
        } else if (destination.x > startPosition.x + patrolDistance) {
            destination.x = startPosition.x + patrolDistance;
        }
        return destination;
    }
}
