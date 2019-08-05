using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour 
    

{

    private float lGrab = 0f;
    private float lFinger = 0f;
    private float lThumb = 0f;

    [SerializeField]
    private float rGrab = 0f;
    [SerializeField]
    private float rFinger = 0f;
    [SerializeField]
    private float rThumb = 0f;

    public Transform handR;
    public Transform handL;
    private Animator LHandAnimator;
    private Animator RHandAnimator;
    public GameObject RHand;
    public GameObject LHand;
    public bool leftyMode = false;
    public bool manual_override = false;

    // Start is called before the first frame update
    void Start()
    {
        LHandAnimator = LHand.GetComponent<Animator>();
        RHandAnimator = RHand.GetComponent<Animator>();
    }

    // Updates the values for hand positions based on Oculus Input
    void UpdateGestures()
    {

        if (!manual_override)
        {
            rGrab = (leftyMode) ? OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) : OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            rFinger = (leftyMode) ? OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) : OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            rThumb = (leftyMode) ? (OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons) ? 1f : 0f) : (OVRInput.Get(OVRInput.NearTouch.SecondaryThumbButtons) ? 0f : 1f);

            lGrab = (!leftyMode) ? OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) : OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            lFinger = (!leftyMode) ? OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) : OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
            lThumb = (!leftyMode) ? (OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons) ? 1f : 0f) : (OVRInput.Get(OVRInput.NearTouch.SecondaryThumbButtons) ? 0f : 1f);
        }

        RHandAnimator.SetFloat("RGrab", rGrab);
        RHandAnimator.SetFloat("RFinger", rFinger);
        RHandAnimator.SetFloat("RThumb", rThumb);

        LHandAnimator.SetFloat("LGrab", lGrab);
        LHandAnimator.SetFloat("LFinger", lFinger);
        LHandAnimator.SetFloat("LThumb", lThumb);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGestures();
    }

    public bool IsPointing(bool isLeft)
    {
        if (isLeft)
        {
            if (lGrab > 0.5f && lFinger < 0.5f)
                return true;
        }
        else
        {
            if (rGrab > 0.5f && rFinger < 0.5f)           
            {
                Debug.Log("is Pointing");
                return true;
            } 
        }
        return false;
    }
}
