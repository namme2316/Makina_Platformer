using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5;
    [SerializeField] bool rotateOtherSide;
    private Transform trans;

    private void Awake()
    {
        trans = GetComponent<Transform>();
    }


    void Update()
    {
        if(rotateOtherSide)
        {
            trans.Rotate(new Vector3(0, 0, 360 * rotationSpeed * Time.deltaTime));
        }
        else
        {
            trans.Rotate(new Vector3(0, 0, 360 * -rotationSpeed * Time.deltaTime));
        }
        
    }
}
