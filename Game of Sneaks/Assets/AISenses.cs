using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour
{
    public float viewDistance = 10;//Distance of vision 
    public float fieldOfView = 60; // Field of view
    public float hearingScale = 1.0f; // How well they can hear.

    const float DEBUG_ANGLE_DISTANCE = 2.0f;
    const float DEGREES_TO_RADIANS = Mathf.PI / 180.0f;
    // Components
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanHear(GameObject target)
    {
        Noisemaker targetNoiseMaker = target.GetComponent<Noisemaker>();
        if (targetNoiseMaker == null)
        {
            return false;
        }

        Transform targetTf = target.GetComponent<Transform>();
        if (Vector3.Distance(targetTf.position, tf.position) <= targetNoiseMaker.volume * hearingScale)
        {
            return true;
        }

        return false;

    }

    public void DrawDebugAngle()
    {
        Vector3 perpendicularDirection = new Vector3(-tf.right.y, tf.right.x);
        float oppositeSideLength = Mathf.Tan(fieldOfView * 0.5f * DEGREES_TO_RADIANS) * DEBUG_ANGLE_DISTANCE;

        Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.right + perpendicularDirection * oppositeSideLength, Color.green);
        Debug.DrawLine(tf.position, tf.position + DEBUG_ANGLE_DISTANCE * tf.right - perpendicularDirection * oppositeSideLength, Color.green);
    }

    public bool CanSee(GameObject target)
    {
        Collider2D targetCollider = target.GetComponent<Collider2D>();
        if (targetCollider == null)
        {
            return false;
        }
        // If they are outside the view angle, we cannot see them
        // To check, we need the vector to our target, and compare that angle to our forward vector
        Transform targetTransform = target.GetComponent<Transform>();
        Vector3 vectorToTarget = targetTransform.position - tf.position;
        vectorToTarget.Normalize();

        DrawDebugAngle();
        if (Vector3.Angle(vectorToTarget, tf.right) >= fieldOfView)
        {
            return false;
        }

        // If they are in our field-of-view (thus we could get here), 
        //     raycast to make sure nothing is blocking our view
        RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, vectorToTarget, viewDistance);

        Debug.Log("HitInfo?: " + targetCollider);

        // if our raycast hit nothing, we can't see them
        if (hitInfo.collider == null)
        {
            return false;
        }

        // If our raycast hit them first, then we can see them
        if (hitInfo.collider == targetCollider)
        {
            Debug.DrawLine(tf.position, tf.position + vectorToTarget * viewDistance, Color.red);
            return true;
        }

        return false;
    }
}
