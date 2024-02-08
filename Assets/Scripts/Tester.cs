using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    
    [SerializeField] private Animator mAnimator;
    public float speed;
    public float groundDistance;

    public LayerMask terrainLayer;
    public Rigidbody playerRigidBody;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //get player rigidbody
        playerRigidBody = gameObject.GetComponent<Rigidbody>();
        //get animator 
        mAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;

        if(Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if(hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(x, 0, y);
        //normalize movement vector 
        if (moveDir.sqrMagnitude > 1) // Only normalize if necessary
        {
            moveDir = moveDir.normalized;
        }
        //move the character
        playerRigidBody.velocity = moveDir * speed;

        //animations 
        if(mAnimator != null)
        {
            //set the required bool for animating to true 
            mAnimator.SetBool("isMoving", true);
        }
        //if moving left set movement direction to left(3)  // up is 0 right is 1 down is 2 left is 3 
        if(x != 0 && x < 0)
        {
            if (mAnimator != null)
            {
                mAnimator.SetInteger("movingDirection", 3);
            }
        }//if moving right 
        else if(x != 0 && x > 0)
        {
            if (mAnimator != null)
            {
                mAnimator.SetInteger("movingDirection", 1);
            }
        }//if moving down
        else if(y != 0 && y < 0)
        {
            if (mAnimator != null)
            {
                mAnimator.SetInteger("movingDirection", 2);
            }
        }//if moving up 
        else if(y != 0 && y > 0)
        {
            if (mAnimator != null)
            {
                mAnimator.SetInteger("movingDirection", 0);
            }
        }
        else//it's not moving anywhere so set isMoving to false
        {
            if(mAnimator != null)
            {
                mAnimator.SetBool("isMoving", false);
            }

        }
    }

}
