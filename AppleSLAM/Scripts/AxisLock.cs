using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxisLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var angle = this.transform.eulerAngles;
        angle.x = 0;
        angle.y = 0;
        angle.z = 0;
        this.transform.eulerAngles = angle;
    }
}
