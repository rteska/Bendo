using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private GameObject playerObject;
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float movementSpeed;

    private bool inInv;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRB.freezeRotation = true;
        inInv = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inInv)
        {
            PlayerMove();
            SpeedLimit();
        }
        else
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                inInv = false;
            }
        }
        

    }

    //This moves the player 
    public void PlayerMove()
    {
        
        Vector3 movementVector = Vector3.zero;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movementVector = (playerObject.transform.forward * z) + (playerObject.transform.right * x);

        playerRB.AddForce(movementVector.normalized * movementSpeed * 10f, ForceMode.Force);

        if (Input.GetKey(KeyCode.I))
        {
            inInv = true;
        }

        
    }

    public void SpeedLimit()
    {
        Vector3 topVelocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        if (topVelocity.magnitude > movementSpeed)
        {
            Vector3 reduceVel = topVelocity.normalized * movementSpeed;
            playerRB.velocity = new Vector3(reduceVel.x, 0f, reduceVel.z);
        }
    }
}
