using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmission : MonoBehaviour
{
    RubbishCollectable rc;

    public void SetRubbishCollectable(RubbishCollectable c)
    {
        rc = c;
    }

    public RubbishCollectable GetRubbishCollectable()
    {
        return rc;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
