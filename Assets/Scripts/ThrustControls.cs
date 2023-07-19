using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrustControls : MonoBehaviour
{
    public bool thrustEnabled=false;
    public void OnMouseDown()
    {
        thrustEnabled = true;
    }
    public void OnMouseUp()
    {
        thrustEnabled = false;
    }
}
