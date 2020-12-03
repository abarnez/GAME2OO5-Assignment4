﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Vector3 forwardDirection;
    private float forwardVelocity;

    private GameObject collisionManagerObject;
    private CollisionManager collisionManager;

    // Start is called before the first frame update
    void Start()
    {
        forwardDirection = GameObject.FindWithTag("Player").transform.forward;

        collisionManagerObject = GameObject.FindWithTag("CollisionManager");
        collisionManager = collisionManagerObject.GetComponent<CollisionManager>();

        forwardVelocity = 3;
        Debug.Log("Direction = " + forwardDirection);

        collisionManager.Spheres.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += forwardDirection * forwardVelocity * Time.deltaTime;
    }
}
