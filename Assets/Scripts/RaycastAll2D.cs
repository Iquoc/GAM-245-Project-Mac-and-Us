using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//https://www.youtube.com/watch?v=B34iq4O5ZYI
public class RaycastAll2D : MonoBehaviour
{
    Ray ray;

    // 3D
    RaycastHit[] hits3D = new RaycastHit[5];

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

        try {
            enemy = master.GetComponent<EnemyController2D>();
            distance = enemy.getVisionDistance();
        }
        catch
        {

        }
    }

    //void CheckForColliders()
    //{
    //    // Not sorted
    //    hits3D = Physics.RaycastAll(ray);

    //    // Sorted
    //    hits2D = Physics2D.RaycastAll(transform.position, transform.up);

    //    // 3D
    //    //if (hits3D.Length > 0)
    //    //{
    //    //    // Sort raycast hit / Creates new array every call negatively impacting performance
    //    //    //Array.Sort(hits, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));

    //    //    // Sorts raycast hit and only reads the size allocated AND number of NEW raycast hits
    //    //    int numHits = Physics.RaycastNonAlloc(ray, hits3D);


    //    //    //for (int i = 0; i < hits.Length; i++)
    //    //    //{
    //    //    //    Debug.Log(hits[i].collider);
    //    //    //}

    //    //    if (numHits > 0)
    //    //    {
    //    //        for (int i = 0; i < numHits; i++)
    //    //        {
    //    //            Debug.Log(hits3D[i].collider);
    //    //        }
    //    //    }
    //    //}

    //    // 2D 
    //    if (hits2D.Length > 0)
    //    {
    //        int numHits = myCollider.Raycast(-transform.up, hits2D);
    //        //int numHits = Physics2D.Raycast(transform.position, Vector3.right, contactFilter, hits2D);

    //        if (numHits > 0)
    //        {
    //            for (int i = 0; i < numHits; i++)
    //            {
    //                Debug.Log(hits2D[i].collider + " was hit!");
    //            }
    //        }

    //    }
    //}

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

    //void OnDrawGizmosSelected()
    //{
    //    if (transform.forward == null)
    //    {
    //        return;
    //    }

    //    Gizmos.color = new Color(1, 1, 0, 0.75F);
    //    Gizmos.DrawWireSphere(transform.position, distance/2);

    //    Gizmos.DrawRay(transform.position, direction);
    //}

    //public Vector3 getScan()
    //{
    //    RaycastHit2D[] rc = CheckForColliders();

    //    if (rc.Length > 0)
    //    {
    //        int len = rc.Length;
    //        for (int i = 0; i < len; i++)
    //        {
    //            if (rc[i].collider.tag == "Player")
    //            {
    //                return rc[i].collider.transform.position;
    //            }
    //        }
    //    }
    //    return new Vector3();
    //}

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
