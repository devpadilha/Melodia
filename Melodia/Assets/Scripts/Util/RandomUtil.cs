using Random = System.Random;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class RandomUtil
{
    private Random random;
    private int size, ini, fim;
    private List<int> last;

    public RandomUtil(int ini, int fim)
    {
        this.random = new Random();
        this.ini = ini;
        this.fim = fim;
        this.size = this.fim - this.ini;
        this.last = new List<int>();
    }

    public int get()
    {
        int rand;
        do
        {
            rand = random.Next(0, size);
            rand = ini + rand;
        } while (last.Any(n => n == rand));
        last.Add(rand);
        //Debug.Log("Random: " + rand);
        return rand;
    }


}
