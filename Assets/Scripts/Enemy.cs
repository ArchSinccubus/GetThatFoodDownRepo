using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float drinkPercentage;

    public bool Drink;

    public float MaxWaitTime;

    [SerializeField]
    List<string> commands;


    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (IsControl)
        {
            Invoke("ControlCharacter", 0.3f);
            IsControl = false;
        }


        if (IsWaiting)
        {
            StomachControl();
        }

    }

    public void setupCommands(string[] s)
    {
        commands = new List<string>();

        foreach (var item in s)
        {
            string[] words = item.Split(' ');

            switch (words[0])
            {
                case "Eat":
                    string foodName = words[1];
                    int amount = int.Parse(words[2]);


                    for (int i = 0; i < amount; i++)
                    {
                        commands.Add("Eat " + foodName);
                    }
                    break;
                case "Drink":
                    amount = int.Parse(words[1]);


                    for (int i = 0; i < amount; i++)
                    {
                        commands.Add("Drink");
                    }
                    break;
                case "Wait":
                    amount = int.Parse(words[1]);

                        commands.Add("Wait " + amount);

                    break;
                default:
                    break;
            }
        }
    }

    public void interpetCommand(string s)
    {
        //Eat Main Dish X times
        //Drink X times
        //Wait X seconds

            string[] words = s.Split(' ');

            switch (words[0])
            {
                case "Eat":
                    string foodName = words[1];
                        StartCoroutine(Consume(Foods, foodName));
                    break;
                case "Drink":
                    string drinkName = words[0];

                        StartCoroutine(Consume(Drinks, drinkName));
                    break;
                case "Wait":
                        int secs = int.Parse(words[1]);
                        StartCoroutine(WaitTime(secs));
                    break;
                default:
                    break;
            }
        


    }

    public IEnumerator Consume(List<Food> list, string name)
    {
        Food F = pickFood(list, name);

        return base.Consume(F);
    }

    public IEnumerator WaitTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        ToggleControl(true);

    }

    public override void ControlCharacter()
    {
        if (Foods.Count > 0 && !GameController.instance.VictoryCheck.HasValue)
        {
            string newComm = commands[0];
            commands.RemoveAt(0);

            //Food F = pickFood((Drink && Drinks.Count > 0) ? Drinks : Foods);

            StopAllCoroutines();
            interpetCommand(newComm);


            //if (Drink)
            //{
            //    Drink = false;
            //    Invoke("DrinkPercent", UnityEngine.Random.Range(0, MaxWaitTime));

            //}
        }
        
    }

    public void startLevel()
    {
        Invoke("DrinkPercent", UnityEngine.Random.Range(0, MaxWaitTime));
    }


    public Food pickFood(List<Food> foods, string name)
    {
        Food F = foods.FindLast(o => o.tag.Contains(name));

        return F;
    }

    public void DrinkPercent()
    {
        float percent = UnityEngine.Random.Range(0, 100);

        if (percent > drinkPercentage && Drinks.Count > 0 && StomachCapacity > 0)
        {
            Drink = true;
        }
        else
        {
            Drink = false;
            Invoke("DrinkPercent", UnityEngine.Random.Range(0, MaxWaitTime));
        }
    }

    public void DrinkWait()
    {

        DrinkPercent();

    }
}
