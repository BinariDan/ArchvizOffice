using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCheck : MonoBehaviour
{

private int layerMask;
private HandController handController;
public Transform rEmit;
//public Transform lEmit;
//public Animator LHandAnimator;
public Animator RHandAnimator;
//private float rGrab;
//private float rFinger;
//private float rThumb;
//private float lGrab;
//private float lFinger;
//private float lThumb;
private GameObject lastHit;

private void Start() 
    {
        layerMask = 1 << 9;
        handController = GetComponent<HandController>();
    }

    // Update is called once per frame
    void Update()
    {

         // Bit shift the index of the layer (8) to get a bit mask
        
        //rGrab = RHandAnimator.GetFloat("RGrab");
        //rFinger = RHandAnimator.GetFloat("RFinger");
        //rThumb = RHandAnimator.GetFloat("RThumb");

        //lGrab = LHandAnimator.GetFloat("LGrab");
        //lFinger = LHandAnimator.GetFloat("LFinger");
        //lThumb = LHandAnimator.GetFloat("LThumb");

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = layerMask;
        if (handController.IsPointing(false))
        {
            
            RaycastHit hit;
            // Does the ray intersect any objects within the furniture layer
            if (Physics.Raycast(rEmit.position, rEmit.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                var info = hit.transform.gameObject.GetComponent<Info>();
                
                if (info != null)
                {
                    //Debug.Log("Did Hit");

                    if (lastHit != info.obj && lastHit != null)
                        lastHit.SetActive(false);

                    lastHit = info.obj;
                    info.obj.SetActive(true);
                }

                //Debug.DrawRay(rEmit.position, rEmit.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            }
            else
            {
                if (lastHit != null)
                {
                    lastHit.SetActive(false);
                    lastHit = null;
                }
                /*
                toonSource.Find("toonLine").SetActive(false);
                Debug.DrawRay(rEmit.position, rEmit.TransformDirection(Vector3.forward) * 1000, Color.white);
                Debug.Log("Did not Hit");
                if (info != null)
                    info.obj.SetActive(false);
                */
            }
        }
        else
        {
            if (lastHit != null)
            {
                lastHit.SetActive(false);
                lastHit = null;
            }
        }

    }
}

