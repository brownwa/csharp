using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingQuarters : MonoBehaviour
{
    enum FrameRate : int { FPS30 = 30, FPS60 = 60, FPS80 = 80 };

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * (int)FrameRate.FPS30);
    }
}
