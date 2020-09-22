using System;
using UnityEngine;

public class Fear // Entirely by NDS8
{
    private String fear;
    private float fitness;
    private int category;
    private String fearType;    //Can be AudioRandom, AudioBehind, VisualWall, VisualFloor, MoveItem, Spawn, Environmental
    private GameObject gObject;


    public Fear(String fear, float fitness, int category, String fearType)
    {
        this.fear = fear;
        this.fitness = fitness;
        this.category = category;
        this.fearType = fearType;
    }
    
    // Return the name of the fear
    public String getFear()
    {
        return fear;
    }

    // Set the name of the fear
    public void setFear(String fear)
    {
        this.fear = fear;
    }

    // Return the fear type
    public String getFearType()
    {
        return fearType;
    }

    // Set the fear type
    public void setFearType(String fearType)
    {
        this.fearType = fearType;
    }

    // Return the fitness
    public float getFitness()
    {
        return this.fitness;
    }

    // Set the fitness
    public void setFitness(float fitness)
    {
        this.fitness = fitness;
    }

    // Return the category
    public int getCategory()
    {
        return this.category;
    }

    // Set the category
    public void setCategory(int category)
    {
        this.category = category;
    }

    // Return the fear as a string
    public string toString()
    {
        return ("{" + "fear='" + fear.ToString() + "\'" + ", fitness=" + fitness.ToString() + "}");
    }

    // Attach a gameObject to the fear
    public void setGameObject(GameObject gameobject) //Allow a game object to be attached to the fear.
    {
        this.gObject = gameobject;
    }

    // Return stored gameObject
    public GameObject returnObject()
    {
        return this.gObject;
    }
}
