using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCube : MonoBehaviour
{
    [SerializeField] int cubePerAxis;
    [SerializeField] int X, Y, Z;
    [SerializeField] float delay;    
    [SerializeField] Transform parent;

    void Start()
    {
        Invoke("Main", delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Main()
    {
        // for(int z = 0; z < cubePerAxis; z++)
        // {
        //     for(int y = 0; y < cubePerAxis; y++)
        //     {
        //         for(int x = 0; x < cubePerAxis; x++)
        //         {
        //             CreateCube(new Vector3(x, y, z));
        //         }
        //     }   
        // }

        for(int z = 0; z < cubePerAxis; z++)
        {            
                for(int x = 0; x < cubePerAxis; x++)
                {
                    CreateCube(new Vector3(x, 0, z));
                }             
        }
        
        Destroy(gameObject);
    }

    void CreateCube(Vector3 coordinates)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        cube.transform.localScale = new Vector3(transform.localScale.x / cubePerAxis,
                                transform.localScale.y,
                                transform.localScale.z / cubePerAxis);


        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale /2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);
        cube.transform.SetParent(parent);
        cube.tag = "Walltmp";

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        Concrit concrit_ = cube.AddComponent<Concrit>();

        gameObject.transform.parent.GetComponent<ConcritController>().concrits.Add(concrit_);
                
    }
}
