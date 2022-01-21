using System.Collections;
using System.Collections.Generic;using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class NPC : Character
{
    public delegate void HealthChanged(float health);

    public event HealthChanged healthChanged;
    public virtual void Deselect()
    {
        
    }
    
    public virtual Transform Select()
    {
        return hitBox;
    }

    public void OnHealthChanged(float health)
    {
        if (healthChanged != null)
        {
            healthChanged(health);
        }
        
    }
}


