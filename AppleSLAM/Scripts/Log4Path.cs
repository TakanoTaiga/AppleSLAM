using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log4Path : MonoBehaviour
{
    public GameObject PointPrefab;
    public GameObject nowPointPrefab;
    public Transform camTf;

    private GameObject nowPoint;

    void Start()
    {

    }


    private int counter = 0;

    private void FixedUpdate() {
        var pos = new Vector3(camTf.position.x * 5 + Screen.width * 0.5f , camTf.position.z * 5 + Screen.height * 0.5f , camTf.position.y);//150
        if(counter >= 20){
            var obj = Instantiate(PointPrefab , pos , Quaternion.identity);
            obj.transform.parent = this.transform;
            counter = 0;
        }else{
            counter += 1;
        }

        var euler = camTf.transform.rotation.eulerAngles;
        var Quater = Quaternion.Euler(0f,0f,360 - euler.y);

        Destroy(nowPoint);
        nowPoint = Instantiate(nowPointPrefab , pos , Quater);
        nowPoint.transform.parent = this.transform;

    }
}
