using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<genericItem> items = new List<genericItem>();

    void BuildDataBase()
    {
        items = new List<genericItem>(
            new genericItem(0, "debug01", "an item I left in like a dumb dumb.");
            //todo: fix this shit
            //a source to work with https://medium.com/@yonem9/create-an-unity-inventory-part-1-basic-data-model-3b54451e25ec
            );
    }
}
