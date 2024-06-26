using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.Device;

public class EnemyController2D : MonoBehaviour
{
    // Manager
    public TmpAnimationController2D anim;

    // ENEMY GAMEOBJECT
    private GameObject enemy;
    private Rigidbody2D rb;
    public float speed = 2.0f;

    // VISION
    public RaycastHit2D[] vision;
    [SerializeField]
    private float visionDistance = 2f;
    public int len;

    // PoI GAMEOBJECT
    private GameObject objectOfInterest; 

    // Point of Interest
    private Vector2 poi;
    private bool spotted;

    //https://www.youtube.com/watch?v=TfjL56tDAOc
    [SerializeField]
    private float rotationSpeed;
    private Vector2 movementInput;

    // STATES
    private bool alert;
    private bool chase;
    [SerializeField]
    private float escapeDist = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;
        rb = enemy.GetComponent<Rigidbody2D>();
        anim = enemy.GetComponent<TmpAnimationController2D>();

        objectOfInterest = GameObject.Find("Player");

        spotted = false;
        rotationSpeed = 1000;

        //vision = enemy.GetComponent<RaycastAll2D>();
        visionDistance = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (chase)
        {
            Chase();
        } 
        else if (alert)
        {
            Search();
        } 
        else
        {
            Patrol();
        }
        //if (spotted)
        //{
        //    Search();
        //} 
        //else
        //{
        //    Patrol();
        //}

    }

    private void FixedUpdate()
    {
        
    }

    // Updates objects seen in line of sight
    public void updateVision(RaycastHit2D[] seen, int len) {
        vision = seen;
        bool blocked = false;

        for (int i = 0; i < len; i++) {
            string tag = vision[i].collider.tag;
            if (tag == "Wall")
            {
                blocked = true;
                break;
            }
            else if (vision[i].collider.tag == "Player")
            {
                objectOfInterest = vision[i].collider.gameObject;
                updateVision(objectOfInterest.transform.position);

                //poi = vision[i].collider.gameObject.transform.position;
                spotted = true;
                //Debug.Log("SPOTTED!");
                break;
            }
        }
    }

    public void updateVision(RaycastHit2D seen)
    {
        updateVision(seen.collider.transform.position);
    }

    private void updateVision(Vector2 pos)
    {
        poi = pos;
        spotted = true;
        ChangeState(2);
    }

    public float getVisionDistance()
    {
        return visionDistance;
    }

    private void RotateInDirectionOfInput()
    {
        if (movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, movementInput);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void ChangeState(int state)
    {
        switch (state)
        {
            case 0:
                // PATROL
                alert = false;
                chase = false;
                break;
            case 1:
                // SEARCH
                alert = true;
                chase = false;
                break;
            case 2:
                // CHASE
                alert = true;
                chase = true;
                break;
            default:
                Debug.Log("NON-EXISTENT STATE");
                break;
        }
    }

    private void Patrol()
    {
        rb.velocity = new Vector3();
        //rb.rotation += 0.1f;
    }

    private void Search()
    {
        Vector2 pos = transform.position;
        Vector2 displacement = poi - pos;

        if (displacement.x >= 0.5f || displacement.x <= -0.5f)
        {
            //https://www.youtube.com/watch?v=dlYoy4galr4

            rb.velocity = new Vector3(displacement.x / Math.Abs(displacement.x), 0.0f, 0.0f) * speed;

            movementInput = new Vector2(displacement.x, 0.0f);
            //rb.rotation = Quaternion.SetLookRotation();
        }
        else if (displacement.y >= 0.5f || displacement.y <= -0.5f)
        {
            rb.velocity = new Vector3(0.0f, displacement.y / Math.Abs(displacement.y), 0.0f) * speed;

            movementInput = new Vector2(0.0f, displacement.y);
        }
        else
        {
            ChangeState(0);
            spotted = false;
        }

        anim.updateMovementInput(movementInput);

        //RotateInDirectionOfInput();
    }

    private void Chase()
    {
        Vector2 dist = objectOfInterest.transform.position - transform.position;
        float magn = dist.magnitude;
        Debug.Log(magn + " " + objectOfInterest);
        if (magn < escapeDist)
        {
            updateVision(objectOfInterest.transform.position);
            Search();
        } else
        {
            spotted = false;
            ChangeState(1);
        }
    }

    public Vector2 getMovementInput()
    {
        return movementInput;
    }
}
