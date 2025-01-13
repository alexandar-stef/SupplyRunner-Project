using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBone : MonoBehaviour
{
    public Transform headBone;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position= headBone.position;
        
    }
}
