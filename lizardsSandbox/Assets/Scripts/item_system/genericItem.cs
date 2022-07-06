using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericItem
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    public genericItem(int id, string title, string description)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Assets/textures/Sprites/Items/" + title);
    }

    public genericItem(genericItem genericItem)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Assets/textures/Sprites/Items/" + title);
    }
}
