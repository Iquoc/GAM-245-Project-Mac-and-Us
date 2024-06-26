using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpAnimationController2D : MonoBehaviour
{
    // Game Object
    public GameObject master;

    // Animation
    public Sprite[] spritesheet;
    public SpriteRenderer sprRen; // down = 0; right = 1; up = 2; left = 3
    

    // Input
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        master = this.gameObject;
        sprRen = master.GetComponent<SpriteRenderer>();
        //spritesheet = "/Sprites/Enemy/Enemy_sprite_sheet.png";
    }

    // Update is called once per frame
    void Update()
    {
        //m_Animator.SetFloat("vertical", movementInput.y);
        //m_Animator.SetFloat("horizontal", movementInput.x);
        if (movementInput.x > 0) // right 
        {
            sprRen.sprite = spritesheet[1];
        }
        if (movementInput.x < 0) // left 
        {
            sprRen.sprite = spritesheet[3];
        }
        if (movementInput.y < 0) // down 
        {
            sprRen.sprite = spritesheet[0];
        }
        if (movementInput.y > 0) // up 
        {
            sprRen.sprite = spritesheet[2];
        }
    }

    public void updateMovementInput(Vector2 moveIn)
    {
        movementInput = moveIn;
    }
}
