using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharController : MonoBehaviour
{
    CharacterController charCont;

    private Vector3 moveDirection = Vector3.zero;
    public float speed = 5f;
    public float gravity = 20f;
    public float jumpSpeed = 8f;
    private float verticalVelocity;

    public Animator anim;
    private float jumpStart = 0f;
    public float jumpCooldown = 1f;
    
    public float aimSpeed;
    public GameObject currWeapon;
    
    //AudioSource walkingSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        charCont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        
        Aiming(Input.GetMouseButton(1));
    }

    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //transform deals with a specific axis and the movement on that axis while considering its rotation
        //Time.deltaTime can be used to make something happen at a constant rate regardless of the (possibly wildly fluctuating) framerate. e.g. projectiles, movement
        moveDirection = transform.right * moveX + transform.forward * moveZ;
        moveDirection *= speed;

        //Everything in here runs ONLY if the player is on the ground during the current frame
        if (charCont.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (Input.GetButton("Sprint") && (moveZ != 0 || moveX != 0))
            {
                speed = 10f;
            }
            else
            {
                speed = 5f;
            }

            if (Input.GetButton("Jump") && (Time.time > jumpStart + jumpCooldown))
            {
                verticalVelocity = jumpSpeed;
                jumpStart = Time.time;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = 0;
        moveDirection.y = verticalVelocity;

        charCont.Move(moveDirection * Time.deltaTime);
    }

    void Aiming(bool p_isAiming)
    {
        Transform w_anchor = currWeapon.transform.Find("Anchor");
        Transform w_state_ads = currWeapon.transform.Find("States/ADS");
        Transform w_state_hip = currWeapon.transform.Find("States/Hip");

        if (p_isAiming)
        {
            w_anchor.position = Vector3.Lerp(w_anchor.position, w_state_ads.position, aimSpeed * Time.deltaTime);
            anim.SetBool("isAiming", true);
        }
        else
        {
            w_anchor.position = Vector3.Lerp(w_anchor.position, w_state_hip.position, aimSpeed * Time.deltaTime);
            anim.SetBool("isAiming", false);
        }        
    }    
}