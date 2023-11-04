using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool isGrounded;
    private float friction;

    // Triggers when the character has a collision with a 2D object
    private void OnCollisionEnter2D(Collision2D col)
    {
        EvaluateCollision(col);
        RetrieveFriction(col);
    }

    // Triggers when the character has stays in contact with a 2D object
    private void OnCollisionStay2D(Collision2D col)
    {
        EvaluateCollision(col);
        RetrieveFriction(col);
    }

    // Triggers when the character is no longer colliding with a 2D object
    private void OnCollisionExit2D(Collision2D col)
    {
        isGrounded = false;
        friction = 0;
    }

    // Game layers:
    // 0: Default
    // 1: TransparentFX
    // 2: Ignore Raycast
    // 3: Player
    // 4: Water
    // 5: UI
    // 6: Ground
    private void EvaluateCollision(Collision2D col)
    {
        for (int i = 0; i < col.contactCount; i++)
        {
            if (col.gameObject.layer == 6)
            {
                Vector2 normal = col.GetContact(i).normal;
                isGrounded |= normal.y >= 0.9f;
            }
        }
    }

    // Similar to ground, but this is a friction check
    private void RetrieveFriction(Collision2D col)
    {
        PhysicsMaterial2D mat = col.rigidbody.sharedMaterial;
        friction = 0;
        if (mat != null)
        {
            friction = mat.friction;
        }
    }

    // Behold! Some getters! Imagine having proper variable protection!
    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public float GetFriction()
    {
        return friction;
    }
}
