using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // References
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;

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
        }
        else
        {
            am.SetBool("Move", false);
        }

        // Trigger Inspect animation on 'Y' key press
        if (Input.GetKeyDown(KeyCode.Y))
        {
            am.Play("PlayerInspect", 0, 0f); // Reset and play the animation from the beginning
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
