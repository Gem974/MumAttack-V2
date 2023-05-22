using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MumEscape_Cells : MonoBehaviour
{
    //Variables
    private int x, y;
    public bool _isEmpty = true;

    public void setX(int val)
    {
        this.x = val;
    }

    public int getX()
    {
        return x;
    }
    public void setY(int val)
    {
        this.y = val;
    }

    public int getY()
    {
        return y;
    }

}
