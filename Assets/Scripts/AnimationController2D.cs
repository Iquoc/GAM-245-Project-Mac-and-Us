using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController2D : MonoBehaviour
{
    // Gane Object
    public GameObject master;
    
    // Animation
    public Animator m_Animator;

    // Input
    private Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        master = this.gameObject;
        m_Animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetFloat("vertical", movementInput.y);
        m_Animator.SetFloat("horizontal", movementInput.x);
    }

    public void updateMovementInput(Vector2 moveIn)
    {
        movementInput = moveIn;
    }
}
