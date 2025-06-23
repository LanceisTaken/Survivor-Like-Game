using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // References
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;

    private float idleTimer = 0f;
    private bool isIdle = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            am.SetBool("Move", true);
            spriteDirectionChecker();

            // IMMEDIATELY reset idle state when movement starts
            if (isIdle)
            {
                am.SetBool("Idling", false);
                isIdle = false;
            }

            // Reset idle timer when moving
            idleTimer = 0f;
        }
        else
        {
            am.SetBool("Move", false);

            // Only count idle time if we're not already idling
            if (!isIdle)
            {
                idleTimer += Time.deltaTime;

                // Check if 5 seconds have passed
                if (idleTimer >= 5f)
                {
                    isIdle = true;
                    am.SetBool("Idling", true);
                }
            }
        }

        // Trigger Inspect animation on 'Y' key press
        if (Input.GetKeyDown(KeyCode.Y))
        {
            am.Play("PlayerInspect", 0, 0f);
        }
    }

    void spriteDirectionChecker()
    {
        if (pm.lastHorizontalVector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
