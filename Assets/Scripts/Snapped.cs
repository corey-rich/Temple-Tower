using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapped : MonoBehaviour
{
    public Transform snappingPoint;     // add in engine
    public Transform parentSegment;     // add in engine
    public Transform otherSnap;         // add in engine
    public float offset;

    private void Start()
    {
        // calculates offset for segment thats being moved
        offset = Vector3.Distance(transform.position, otherSnap.transform.position) / 2;
        if (gameObject.tag == "SnapPointRight")
            offset -= offset * 2;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SnapTrigger")
        {
            // moves segment
            parentSegment.position = new Vector3(snappingPoint.transform.position.x + offset, parentSegment.position.y, parentSegment.position.z);
        }
    }
}
