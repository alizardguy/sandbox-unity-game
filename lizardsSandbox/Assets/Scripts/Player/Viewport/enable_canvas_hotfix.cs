using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enable_canvas_hotfix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
