using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController2D : MonoBehaviour
{
    Ray ray;

    // 2D
    RaycastHit2D[] hits2D = new RaycastHit2D[5];
    //RaycastHit2D hits2D = new RaycastHit2D();
    public ContactFilter2D contactFilter;

    public Collider2D myCollider;

    public GameObject master;

    private EnemyController2D enemy;
    public float distance;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        //ray = new Ray(transform.position, transform.forward);
        ray = new Ray(transform.position, direction);

        master = this.gameObject;

        //direction = -transform.up;
        direction = transform.right;

        try
        {
            enemy = master.GetComponent<EnemyController2D>();
            distance = enemy.getVisionDistance();
        }
        catch
        {

        }
    }

    private RaycastHit2D[] CheckForColliders()
    {
        // RaycastAll returns everything that was hit along with the length of the ray
        // Sorted
        hits2D = Physics2D.RaycastAll(transform.position, direction, distance);
        //hits2D = Physics2D.Raycast(transform.position, transform.up);

        Vector2 lineDir = new Vector2(transform.position.x, transform.position.y) + (direction * distance);
        //Debug.DrawRay(transform.position, transform.up, Color.white, 0.1f);
        Debug.DrawLine(transform.position, lineDir, Color.white);

        // 2D 
        if (hits2D.Length > 0)
        {
            //int numHits = myCollider.Raycast(transform.up, hits2D);
            ////int numHits = Physics2D.Raycast(transform.position, Vector3.right, contactFilter, hits2D);

            // Raycast returns the first thing hit by the ray
            int numHits = myCollider.Raycast(direction, hits2D, distance);


            if (numHits > 0)
            {
                for (int i = 0; i < numHits; i++)
                {
                    //Debug.Log(hits2D[i].collider + " was hit!");
                }
            }

            enemy.updateVision(hits2D, numHits);

        }

        return hits2D;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForColliders();

        try
        {
            distance = enemy.getVisionDistance();
            updateDirection(enemy.getMovementInput());
        }
        catch
        {

        }
    }

    public void updateDirection(Vector2 newDir)
    {
        if (newDir == Vector2.zero)
        {
            direction = -transform.up;
        }
        else
        {
            direction = newDir.normalized;
        }

    }
}

