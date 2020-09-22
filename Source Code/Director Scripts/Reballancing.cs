using System;
using System.Collections.Generic;

public class Reballancing // Coded by SJL52, Structured for C# by JB2051 and NDS8 (60:20:20)
{
    private int l3Size;
    private int l2Size;
    private int l1Size;

    private List<Fear> l1List;
    private List<Fear> l2List;
    private List<Fear> l3List;

    private Fear l1Highest;
    private Fear l2Highest;
    private Fear l3Highest;

    public Reballancing(List<Fear> lv1, List<Fear> lv2, List<Fear> lv3)
    {
        this.l1List = new List<Fear>(lv1);
        this.l2List = new List<Fear>(lv2);
        this.l3List = new List<Fear>(lv3);

        //levelThree.Add(new Fear("A", 100));
        //levelThree.Add(new Fear("B", 86));
        //levelThree.Add(new Fear("C", 34));

        //levelTwo.Add(new Fear("D", 94));
        //levelTwo.Add(new Fear("E", 66));
        //levelTwo.Add(new Fear("F", 90));

        //levelOne.Add(new Fear("G", 44));
        //levelOne.Add(new Fear("H", 99));
        //levelOne.Add(new Fear("I", 22));

        this.l3Size = l3List.Count;
        this.l2Size = l2List.Count;
        this.l1Size = l1List.Count;

        this.l1Highest = new Fear(null, 1, 1, null); //setup base fears to be overwritten
        this.l2Highest = new Fear(null, 1, 1, null);
        this.l3Highest = new Fear(null, 1, 1, null);
    }

    private Fear calculateHighest(List<Fear> fearlist) { 
        //returns highest valued fear
        for (int i = 0; i < fearlist.Count; i++)
        {
            Fear highest = new Fear(null, 1, 1, null);
            Fear search = fearlist[i];
            if (search.getFitness() >= highest.getFitness())
            {
                highest = search;
            }
            return highest;
        }
        return null;
    }

    public void addFear(String name, int fitness, int level, String type) 
    //adds fear to its category corresponding to level
    {
        if (level == 1)
        {
            l1List.Add(new Fear(name, fitness, 1, type));
            l1Size = l1List.Count;
            l1Highest = calculateHighest(l1List);
        }
        else if(level == 2)
        {
            l2List.Add(new Fear(name, fitness, 2, type));
            l2Size = l2List.Count;
            l2Highest = calculateHighest(l2List);
        }
        else
        {
            l3List.Add(new Fear(name, fitness, 3, type));
            l3Size = l3List.Count;
            l3Highest = calculateHighest(l3List);
        }
    }

    public float getFearValue(String name)
    //returns fear value
    {
        Fear l3 = l3List.Find(x => x.getFear() == name);
        Fear l2 = l2List.Find(x => x.getFear() == name);
        Fear l1 = l1List.Find(x => x.getFear() == name);

        if (l1 != null)
        {
            return l1.getFitness() / l1Highest.getFitness() * 100;
        }
        if (l2 != null)
        {
            return l2.getFitness() / l2Highest.getFitness() * 100;
        }
        if (l3 != null)
        {
            return l3.getFitness() / l3Highest.getFitness() * 100;
        }
        return 0.0f;
    }

    public void changeFearValue(String name, float newFitness) 
    //changes fear value
    {
        Fear l3 = l3List.Find(x => x.getFear() == name);
        Fear l2 = l2List.Find(x => x.getFear() == name);
        Fear l1 = l1List.Find(x => x.getFear() == name);

        if (l1 != null)
        {
            l1.setFitness(newFitness);
        }
        else if (l2 != null)
        {
            l2.setFitness(newFitness);
        }
        else if (l3 != null)
        {
            l3.setFitness(newFitness);
        }
    }

    public Fear getMaxFear(int level)
    {
        if(level == 1)
        {
            return this.l1Highest;
        }
        if(level == 2)
        {
            return this.l2Highest;
        }
        if(level == 3)
        {
            return this.l3Highest;
        }
        return null;
    }
       /* System.out.println();
        System.out.print("Level Three = ");
        for (int i = 0; i < levelThree.size(); i++)
        {
            int percent = levelThree.get(i).getFitness();
            System.out.print(percent * 100 / highest + "%  ");
        }
        System.out.println();
        System.out.print("Level Two = ");
        for (int i = 0; i < levelThree.size(); i++)
        {
            int percent = levelTwo.get(i).getFitness();
            System.out.print(percent * 100 / highest + "%  ");
        }
        System.out.println();
        System.out.print("Level One = ");
        for (int i = 0; i < levelOne.size(); i++)
        {
            int percent = levelOne.get(i).getFitness();
            System.out.print(percent * 100 / highest + "%  ");
        }
    */
}
