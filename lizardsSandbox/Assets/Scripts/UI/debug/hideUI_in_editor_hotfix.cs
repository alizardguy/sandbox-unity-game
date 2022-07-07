using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideUI_in_editor_hotfix : MonoBehaviour
{
    [SerializeField] private GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        self.SetActive(true);
    }
}
