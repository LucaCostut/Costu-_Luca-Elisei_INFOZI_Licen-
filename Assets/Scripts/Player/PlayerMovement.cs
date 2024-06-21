using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;

    private Camera cam;
    private GameObject spells;

    private Vector2 movementDirection;

    private void Start()
    {
        cam        = Camera.main;
        spells     = transform.parent.GetChild(1).gameObject;

        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        Rotate();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = movementDirection * playerStats.speed;
        spells.transform.position = transform.position;
    }

    private void Rotate()
    {
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        if (Time.timeScale == 0 || movementDirection == Vector2.zero)
            return;

        transform.up = -1 * movementDirection;
    }

    public void SetMovementDirection(Vector2 movDir)
    {
        movementDirection = movDir;
    }
}
