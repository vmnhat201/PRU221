using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class BuffData
{
    public BuffStyle style;
    public float quantity;
    public string introName;
    public string name;
    public string tag;
    public BuffData(Buff buff)
    {   
        if (buff == null)
        {
            Debug.Log("Buff is null");
            return;
        }
        style = buff.style;
        quantity = buff.quantity;
        introName = buff.intro.name;
        name = buff.name;
        tag = buff.tag;
    }
}
