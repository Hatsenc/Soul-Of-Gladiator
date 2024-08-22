using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HW
{
    public class PlayerMovement : MonoBehaviour
{
    Transform cameraObject;
    InputManager inputManager;
    Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float rotationSpeed = 10;
        void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        cameraObject = Camera.main.transform;
        myTransform = transform;
    }

    public void Update()
    {
       float  delta = Time.deltaTime;

       inputManager.TickInput(delta);

       moveDirection = cameraObject.forward * inputManager.vertical;
       moveDirection = cameraObject.right * inputManager.horizontal;
       moveDirection.Normalize();

       float speed = movementSpeed;
       moveDirection *= speed;

       Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, nomralVector);
       rigidbody.velocity = projectedVelocity;

       
    }

    #region Movement
    Vector3 nomralVector;
    Vector3 targetPos;

    private void HandleRotaation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputManager.moveAmount;

        targetDir = cameraObject.forward * inputManager.vertical;
        targetDir = cameraObject.right * inputManager.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if(targetDir == Vector3.zero)
            targetDir  = myTransform.forward;

        float rs = rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

        myTransform.rotation = targetRotation;
    }
    #endregion

}

}

