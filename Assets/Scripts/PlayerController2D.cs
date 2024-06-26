using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public GameManager gameMan;

    private GameObject player;
    private Rigidbody2D rb;
    public float speed = 10.0f;

    // Animation
    private AnimationController2D anim;

    //https://www.youtube.com/watch?v=TfjL56tDAOc
    //https://www.youtube.com/watch?v=gs7y2b0xthU
    [SerializeField]
    private float rotationSpeed;
    public Vector2 movementInput;

    // Start is called before the first frame update
    void Start()
    {
        // Manager
        gameMan = GameManager.instance;
        anim = this.GetComponent<AnimationController2D>();


        // Game Object
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();

        movementInput = new Vector2(0.0f, 0.0f);
        rotationSpeed = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // https://discussions.unity.com/t/2d-how-to-make-character-slowly-stop-after-moving/244550/2
        float hori = Input.GetAxisRaw("Horizontal"); // Stores horizontal input
        float vert = Input.GetAxisRaw("Vertical"); // Stores vertical input

        movementInput = new Vector2(hori, vert);
        anim.updateMovementInput(movementInput);

        if (movementInput != new Vector2(0.0f, 0.0f)) { gameMan.playAudio("playing player audio"); }

        rb.velocity = new Vector3(hori, vert) * speed; // Changes velocity based on (2D input) * speed

        //RotateInDirectionOfInput();

        // https://www.youtube.com/watch?v=B34iq4O5ZYI
    }

    //private void FixedUpdate()
    //{
    //    RotateInDirectionOfInput();
    //}

    private void RotateInDirectionOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            //rb.MoveRotation(rotation);
        }
    }
}
