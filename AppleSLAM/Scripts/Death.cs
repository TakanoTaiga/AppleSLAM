using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private int counter = 0;
    private void FixedUpdate() {
        if(counter > 60 * 120){
            Destroy(this.gameObject);
        }else{
            counter += 1;
        }
    }
}
