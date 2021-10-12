using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject playerCam;

    public float walkSpeed = 800.0f;

    protected Rigidbody myRigidbody;
    protected CapsuleCollider myCollider;
    protected Transform groundCheck;

    protected Vector2 inputVec;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindWithTag("MainCamera");
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myCollider = gameObject.GetComponent<CapsuleCollider>();
        groundCheck = gameObject.transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveCharacter();
        Debug.Log(IsGrounded());
    }

    float GetSpeed()
    {
        return walkSpeed;
    }

    bool IsGrounded()
    {
        int _layerMask = ~LayerMask.GetMask("Player");
        return Physics.CheckCapsule(myCollider.bounds.center, new Vector3(groundCheck.position.x, groundCheck.position.y - myCollider.radius, groundCheck.position.z), myCollider.radius, _layerMask);
    } 

    void MoveCharacter() 
    {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, playerCam.transform.eulerAngles.y, transform.eulerAngles.z);
        if (IsGrounded())
        {
            myRigidbody.velocity = (transform.forward * GetSpeed() / 100) * inputVec.y;
        }
    }
    public void OnMove(InputValue input) 
    {
        inputVec = input.Get<Vector2>();
    }
    public void OnJump() 
    { 
    
    }
}
