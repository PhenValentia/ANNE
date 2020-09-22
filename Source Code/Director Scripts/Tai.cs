using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Tai { // by JB2051

    private float mean;
    private float sd;
    private float maxProbability;
    private float imin;
    private float imax;


    public Tai(float mean, float sd, float maxProbability)
    {
        this.mean = mean;
        this.sd = sd;
        this.maxProbability = maxProbability;
    }

    public void changeDistribution(float mean, float sd, float maxProbability)
    {
        this.mean = mean;
        this.sd = sd;
        this.maxProbability = maxProbability;
    }

    public double getProbability(float time)
    {
        double power = -0.5*(time-this.mean)*(time-this.mean) / (this.sd*this.sd); //first part of distribution equation
        double equation = 1 / (Math.Sqrt(2*Math.PI*this.sd*this.sd)); //second part 0.0132980761
        double epower = Math.Exp(power);
        //Debug.Log("E: " + epower);
        return equation * epower*this.maxProbability*100;
    }

    public float getTime() //get a random time interval to activate bot
    {
        System.Random random = new System.Random();


        double rand = random.Next(100);
        int beforeafter = random.Next(2);
        if(beforeafter == 1)
        {
            imin = this.mean+1;
            imax = this.mean + (2 * this.sd);
        }
        else
        {
            imin = this.mean-(2*this.sd);
            imax = this.mean;
        }
        for (float i = imin; i<= imax; i++) //between values of +- 2 standard deviations, if none then do 1 standard deviation above
        {
            double prob = getProbability(i);
            //Debug.Log("i: " + i);
            //Debug.Log("Prob:" + prob);
            //Debug.Log("Rand:" + rand);

            if (beforeafter == 1)
            {
                if (rand >= prob)
                {
                    prob = this.maxProbability - prob;
                    return i;
                }
            }
            else
            {
                if (rand <= prob)
                {
                    return i;
                }
            }
        }
        return this.mean + this.sd;
    }
}
