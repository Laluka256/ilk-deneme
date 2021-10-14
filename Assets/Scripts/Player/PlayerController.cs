using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCam;

    public float walkSpeed = 500.0f;
    public float jumpSpeed = 300.0f;

    protected Rigidbody myRigidbody;
    protected CapsuleCollider myCollider;
    protected Transform groundCheck;

    protected Vector2 inputVec;
    protected float startRot;

    void Start()
    {
        playerCam = GameObject.FindWithTag("MainCamera");
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myCollider = gameObject.GetComponent<CapsuleCollider>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        startRot = Mathf.Round(gameObject.transform.eulerAngles.y);
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    float GetSpeed()
    {
        return walkSpeed;
    }

    bool IsGrounded()
    {
        int _layerMask = ~LayerMask.GetMask("Player");
        return Physics.CheckCapsule(myCollider.bounds.center, new Vector3(groundCheck.position.x, groundCheck.position.y + myCollider.radius - 0.01f, groundCheck.position.z), myCollider.radius, _layerMask);
    } 

    void MoveCharacter() 
    {
        if (inputVec.x != 0.0f) //Check if wasd input is present
        {
            //TODO:  duvara doðru yürüdüðümüz zaman aþaðýya düþmek yerine duvarda sabit kalýyor bunu düzelt.
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, startRot * -inputVec.x, transform.eulerAngles.z);
            myRigidbody.velocity = new Vector3((transform.forward * GetSpeed() / 100).x, myRigidbody.velocity.y, myRigidbody.velocity.z);
        }
    }
    public void OnMove(InputValue input) 
    {
        inputVec = input.Get<Vector2>();
    }
    public void OnJump() 
    {
        if (IsGrounded())
        {
            myRigidbody.AddForce(gameObject.transform.up * jumpSpeed * 100);
        }
    }
}
