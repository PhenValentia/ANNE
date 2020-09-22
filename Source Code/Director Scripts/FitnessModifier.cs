using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessModifier { // by NDS8 and JB2051 (50%:50%)
    private float l1Mod;
    private float l2Mod;
    private float l3Mod;

    public FitnessModifier(Reballancing reb)
    {
        updateModifiers(reb);
        l3Mod = 1;
    }

    public void updateModifiers(Reballancing reb)
    {
        Fear m1 = reb.getMaxFear(1);
        Fear m2 = reb.getMaxFear(2);
        Fear m3 = reb.getMaxFear(3);

        this.l1Mod = m3.getFitness() / m1.getFitness();
        this.l2Mod = m3.getFitness() / m2.getFitness();
    }

    public float getModifier(int level)
    {
        if(level == 1)
        {
            return this.l1Mod;
        }
        if(level == 2)
        {
            return this.l2Mod;
        }
        if(level == 3)
        {
            return this.l2Mod;
        }
        return 1.0f;
    }
}
