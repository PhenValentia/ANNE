using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIFitnessFunction : MonoBehaviour { // by NDS8 and JB2051 (50%:50%)

    private List<Fear> cat1;
    private List<Fear> cat2;
    private List<Fear> cat3;

    [SerializeField]
    private float cat1mod;
    [SerializeField]
    private float cat2mod;
    [SerializeField]
    private float cat3mod;

    private Tai tBot;

    [SerializeField]
    private float target = -100f;
    [SerializeField]
    private float time = 0;

    // Use this for initialization

    public AIFitnessFunction()
    {
        CreateCategory1();
        CreateCategory2();
        CreateCategory3();
    }
    
    void CreateCategory1 ()
    {
        cat1 = new List<Fear>(); //adding all fears to cat 1
        cat1.Add(new Fear("BloodOnWalls", 30, 1, "VisualWall"));
        //cat1.Add(new Fear("ClownFaceOnWall", 30, 1, "VisualWall"));
        cat1.Add(new Fear("WritingOnWall", 30, 1, "VisualWall"));
        //cat1.Add(new Fear("ItemsFloating", 30, 1, "MoveItem"));
        cat1.Add(new Fear("DeadBodies", 30, 1, "VisualFloor"));
    }

    void CreateCategory2()
    {
        cat2 = new List<Fear>(); //adding all fears to cat2
        cat2.Add(new Fear("LightsFlickering", 50, 2, "Environmental"));
        cat2.Add(new Fear("door-knock", 50, 2, "AudioBehind"));
        //cat2.Add(new Fear("Spiders", 50, 2, "Spawn"));
        cat2.Add(new Fear("Ghost", 50, 2, "Spawn"));
        cat2.Add(new Fear("DoorClosing", 50, 2, "MoveItem"));
        cat2.Add(new Fear("Scream", 50, 2, "Audio"));
        cat2.Add(new Fear("wooden-floor-creaking-", 50, 2, "Audio"));
        //cat2.Add(new Fear("Begging", 50, 2, "Audio"));
    }
    void CreateCategory3()
    {
        cat3 = new List<Fear>(); //adding all fears to cat 3
        cat3.Add(new Fear("LimbMonster", 80, 3, "Spawn"));
        cat3.Add(new Fear("FootstepsBehindYou", 80, 3, "AudioBehind"));
        cat3.Add(new Fear("loudScream", 80, 3, "Audio"));
        //cat3.Add(new Fear("Whispers", 80, 3, "Audio"));
        //cat3.Add(new Fear("ArmsThroughWall", 80, 3, "VisualWall"));
    }

    void UpdateFitnessCat1() // updates the fitness of cat 1 based on max fitness
    {
        float highestFitness = 0;
        //Debug.Log("Cat 1 Highest Fitness:" + highestFitness);
        foreach (Fear entry in cat1)
        {
            if (entry.getFitness() > highestFitness)
            {
                highestFitness = entry.getFitness();
            }
        }
        //Debug.Log("Cat 1 Highest Fitness:" + highestFitness);
        cat1mod = 100 / highestFitness;
        List<Fear> tempCat1 = new List<Fear>();
        foreach (Fear entry in cat1)
        {
            tempCat1.Add(new Fear(entry.getFear(), entry.getFitness() * cat1mod, 1, entry.getFearType()));
            //Debug.Log("Setting New Fitness of entry:" + entry.getFear() + " and fitness:" + entry.getFitness() * cat1mod);
        }
        cat1 = tempCat1;
    }

    void UpdateFitnessCat2() //same as above but for cat 2
    {
        float highestFitness = 0;
        //Debug.Log("Cat 2 Highest Fitness:" + highestFitness);
        foreach (Fear entry in cat2)
        {
            if (entry.getFitness() > highestFitness)
            {
                highestFitness = entry.getFitness();
            }
        }
        //Debug.Log("Cat 2 Highest Fitness:" + highestFitness);
        cat2mod = 100 / highestFitness;
        List<Fear> tempCat2 = new List<Fear>();
        foreach (Fear entry in cat2)
        {
            tempCat2.Add(new Fear(entry.getFear(), entry.getFitness() * cat2mod, 2, entry.getFearType()));
            //Debug.Log("Setting New Fitness of entry:" + entry.getFear() + " and fitness:" + entry.getFitness() * cat2mod);
        }
        cat2 = tempCat2;
    }

    void UpdateFitnessCat3() //same here
    {
        float highestFitness = 0;
        //Debug.Log("Cat 3 Highest Fitness:" + highestFitness);
        foreach (Fear entry in cat3)
        {
            if(entry.getFitness() > highestFitness)
            {
                highestFitness = entry.getFitness();
            }
        }
        //Debug.Log("Cat 3 Highest Fitness:" + highestFitness);
        cat3mod = 100/highestFitness;
        List<Fear> tempCat3 = new List<Fear>();
        foreach (Fear entry in cat3)
        {
            tempCat3.Add(new Fear(entry.getFear(), entry.getFitness() * cat3mod, 3, entry.getFearType()));
            //Debug.Log("Setting New Fitness of entry:" + entry.getFear() + " and fitness:" + entry.getFitness() * cat3mod);
        }
        cat3 = tempCat3;
    }

    void UpdateAllFitness() //update all categories at once
    {
        UpdateFitnessCat1();
        UpdateFitnessCat2();
        UpdateFitnessCat3();
    }

    public Fear FireOnce() //request a fear
    {
        //Set Random Number
        float rand = Random.value;
        //Debug.Log("Rand Selected " + rand);
        //Identify which category based on Modifiers
        if (rand < cat1mod / (cat1mod + cat2mod + cat3mod))
        {
            //Debug.Log("Firing Cat1");
            return FireCat1();
        }
        else if (rand < cat1mod + cat2mod / (cat1mod + cat2mod + cat3mod))
        {
            //Debug.Log("Firing Cat2");
            return FireCat2();
        }
        else
        {
            //Debug.Log("Firing Cat3");
            return FireCat3();
        }
    }

    Fear FireCat1() //fire category 1
    {
        //Set Random Number
        float rando = Random.value;
        //Set Total Fitness
        float fitnessTotal = 0;
        foreach (Fear entry in cat1)
        {
            fitnessTotal += entry.getFitness();
        }
        //Set Counter
        float counter = 0;
        //Set Boolean to display it not being fired
        bool notFired = true;
        //Set Element Detail Catchers
        string fireElementName = null;
        float fireElementFitness = 0f;
        string fireElementType = null;
        //Establish Which One to Fire and Fire it
        foreach (Fear entry in cat1)
        {
            counter += entry.getFitness();
            if (rando < counter / fitnessTotal && notFired)
            {
                notFired = false;
                fireElementName = entry.getFear();
                fireElementFitness = entry.getFitness();
                fireElementType = entry.getFearType();
            }
        }
        if (fireElementName != null && fireElementFitness != 0f)
        {
            return new Fear(fireElementName, fireElementFitness, 1, fireElementType);
        }
        else
        {
            return null;
        }
    }

    Fear FireCat2() //fire category 2
    {
        //Set Random Number
        float rando = Random.value;
        //Set Total Fitness
        float fitnessTotal = 0;
        foreach (Fear entry in cat2)
        {
            fitnessTotal += entry.getFitness();
        }
        //Set Counter
        float counter = 0;
        //Set Boolean to display it not being fired
        bool notFired = true;
        //Set Element Detail Catchers
        string fireElementName = null;
        float fireElementFitness = 0f;
        string fireElementType = null;
        //Establish Which One to Fire and Fire it
        foreach (Fear entry in cat2)
        {
            counter += entry.getFitness();
            if (rando < counter / fitnessTotal && notFired)
            {
                notFired = false;
                fireElementName = entry.getFear();
                fireElementFitness = entry.getFitness();
                fireElementType = entry.getFearType();
            }
        }
        if (fireElementName != null && fireElementFitness != 0f)
        {
            return new Fear(fireElementName, fireElementFitness, 2, fireElementType);
        }
        else
        {
            return null;
        }
    }

    Fear FireCat3() //fire category 3
    {
        //Set Random Number
        float rando = Random.value;
        //Set Total Fitness
        float fitnessTotal = 0;
        foreach (Fear entry in cat3)
        {
            fitnessTotal += entry.getFitness();
        }
        //Set Counter
        float counter = 0;
        //Set Boolean to display it not being fired
        bool notFired = true;
        //Set Element Detail Catchers
        string fireElementName = null;
        float fireElementFitness = 0f;
        string fireElementType = null;
        //Establish Which One to Fire and Fire it
        foreach (Fear entry in cat3)
        {
            counter += entry.getFitness();
            if (rando < counter / fitnessTotal && notFired)
            {
                notFired = false;
                fireElementName = entry.getFear();
                fireElementFitness = entry.getFitness();
                fireElementType = entry.getFearType();
            }
        }
        if (fireElementName != null && fireElementFitness != 0f)
        {
            return new Fear(fireElementName, fireElementFitness, 3, fireElementType);
        }
        else
        {
            return null;
        }
    }

    void FireCat1Element(string elementName, float elementFitness, string elementType) //fire category 1 element
    {
        //Fire Based on Element Name
        //Debug.Log("Fired " + elementName);
        //Change Fitness by Checking
        //Debug.Log("Previous Fitness " + elementFitness);
        cat1.Remove(new Fear(elementName, elementFitness, 1, elementType));
        cat1.Add(new Fear(elementName, EvaluateFitness(elementFitness), 1, elementType));
    }

    void FireCat2Element(string elementName, float elementFitness, string elementType) //fire category 2 element
    {
        //Fire Based on Element Name
        //Debug.Log("Fired " + elementName);
        //Change Fitness by Checking
        //Debug.Log("Previous Fitness " + elementFitness);
        cat2.Remove(new Fear(elementName, elementFitness, 2, elementType));
        cat2.Add(new Fear(elementName, EvaluateFitness(elementFitness), 2, elementType));
    }

    void FireCat3Element(string elementName, float elementFitness, string elementType) //fire category 3 element
    { 
        //Fire Based on Element Name
        //Debug.Log("Fired " + elementName);
        //Change Fitness by Checking
        //Debug.Log("Previous Fitness " + elementFitness);
        cat3.Remove(new Fear(elementName, elementFitness, 3, elementType));
        cat3.Add(new Fear(elementName, EvaluateFitness(elementFitness) ,3, elementType));
    }

    public void reballanceElement(Fear fear, float percentageChange) //rebalance fears
    {
        if(fear.getCategory() == 1){
            if (percentageChange == 0.0f)
            {
                cat1.Remove(new Fear(fear.getFear(), fear.getFitness(), 1, fear.getFearType()));
                cat1.Add(new Fear(fear.getFear(), fear.getFitness() - 8, 1, fear.getFearType()));
            }
            else
            {
                cat1.Remove(new Fear(fear.getFear(), fear.getFitness(), 1, fear.getFearType()));
                cat1.Add(new Fear(fear.getFear(), fear.getFitness() + (16 * percentageChange), 1, fear.getFearType()));
            }
        }
        if (fear.getCategory() == 2)
        {
            if (percentageChange == 0.0f)
            {
                cat2.Remove(new Fear(fear.getFear(), fear.getFitness(), 2, fear.getFearType()));
                cat2.Add(new Fear(fear.getFear(), fear.getFitness() - 6, 2, fear.getFearType()));
            }
            else
            {
                cat2.Remove(new Fear(fear.getFear(), fear.getFitness(), 2, fear.getFearType()));
                cat2.Add(new Fear(fear.getFear(), fear.getFitness() + (12 * percentageChange), 2, fear.getFearType()));
            }
        }
        if (fear.getCategory() == 3)
        {
            if (percentageChange == 0.0f)
            {
                cat3.Remove(new Fear(fear.getFear(), fear.getFitness(), 3, fear.getFearType()));
                cat3.Add(new Fear(fear.getFear(), fear.getFitness() - 4, 3, fear.getFearType()));
            }
            else
            {
                cat3.Remove(new Fear(fear.getFear(), fear.getFitness(), 3, fear.getFearType()));
                cat3.Add(new Fear(fear.getFear(), fear.getFitness() + (8 * percentageChange), 3, fear.getFearType()));
            }
        }
        UpdateAllFitness();
    }

    float EvaluateFitness(float fitness) //evaluate fitness of a fear by a certain fitness
    {
        //Random Placeholder
        return (fitness + 100 * (Random.value - 0.5f));
    }

    void DebugCats() //debug all categories
    {
        //Show All Entries
        Debug.Log("Cat1");
        foreach (Fear entry in cat1)
        {
            Debug.Log("Entity: " + entry.getFear() + "- Fitness: " + entry.getFitness());
        }
        Debug.Log("Cat2");
        foreach (Fear entry in cat2)
        {
            Debug.Log("Entity: " + entry.getFear() + "- Fitness: " + entry.getFitness());
        }
        Debug.Log("Cat3");
        foreach (Fear entry in cat3)
        {
            Debug.Log("Entity: " + entry.getFear() + "- Fitness: " + entry.getFitness());
        }

    }
}