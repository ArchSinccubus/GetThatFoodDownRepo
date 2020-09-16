using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int levelBackground;
    public int EnemySkin;

    public MultiDimensionalInt MainDish;
    public MultiDimensionalInt FirstDish;
    public MultiDimensionalInt Drink;


    public MultiDimensionalInt[] levelFoodMatrix;
    public string[] LevelAI;

    [HideInInspector]
    public List<Food> PlayerFoods;
    [HideInInspector]
    public List<Food> PlayerDrinks;
    [HideInInspector]
    public List<Food> EnemyFoods;
    [HideInInspector]
    public List<Food> EnemyDrinks;

    public bool? Winner;

    public void init()
    {
        PlayerFoods = new List<Food>();
        PlayerDrinks = new List<Food>();
        EnemyFoods = new List<Food>();
        EnemyDrinks = new List<Food>();

    }

    public void startLevel()
    {
        GameController.instance.Player.StomachCapacity = 0;
        GameController.instance.Player.IsVomiting = false;
        GameController.instance.Enemy.StomachCapacity = 0;
        GameController.instance.Enemy.IsVomiting = false;
        GameController.instance.Enemy.Drink = false;

        UImanager.FillStomachBar(0);

        GameController.instance.Player.Foods = PlayerFoods;
        GameController.instance.Player.Drinks = PlayerDrinks;


        GameController.instance.Enemy.Foods = EnemyFoods;
        GameController.instance.Enemy.Drinks = EnemyDrinks;


        GameController.instance.Enemy.startLevel();

    }

    /*
     * 
     * 
     *                 case 2:
                    F.tag = "Drink";
                    F.transform.SetParent(TablePos.Find("Drink"), false);
                    F.transform.localPosition = Vector3.up * j * 0.2f;
                    break;
                    */


    public void fillFoods(Transform PlayerTablePos, Transform EnemyTablePos)
    {
        FillByType(PlayerTablePos, true, PlayerFoods, FirstDish, "FirstDish");
        FillByType(EnemyTablePos, false, EnemyFoods, FirstDish, "FirstDish");
        FillByType(PlayerTablePos, true, PlayerFoods, MainDish, "MainDish");
        FillByType(EnemyTablePos, false, EnemyFoods, MainDish, "MainDish");

        FillByType(PlayerTablePos, PlayerDrinks, GameController.instance.DrinkDetails);
        FillByType(EnemyTablePos, EnemyDrinks, GameController.instance.DrinkDetails);



        GameController.instance.Enemy.setupCommands(LevelAI);
    }

    private void FillByType(Transform TablePos, bool player, List<Food> CharacterFoodList, MultiDimensionalInt array, string tag)
    {
        Transform origPos = TablePos.Find(tag).GetChild(0);

        for (int i = 0; i < array.FoodAmount; i++)
        {
            Food F = GameController.getFoodFromPool();

            F.FillProperties(GameController.instance.FoodTypes[array.FoodType]);

            F.tag = tag;
            F.transform.SetParent(TablePos.Find(tag), false);
            F.transform.position = origPos.GetChild(i).position;


            CharacterFoodList.Add(F);
        }

        /*for (int j = 0; j < array.FoodAmount; j++)
        {
            Food F = GameController.getFoodFromPool();

            F.FillProperties(GameController.instance.FoodTypes[array.FoodType]);

            //F.transform.position += ((F.type == FoodTypes.Food) ? Vector3.left : Vector3.right) * 0.4f;

                    F.tag = tag;
                    F.transform.SetParent(TablePos.Find(tag), false);
                    F.transform.localPosition = Vector3.up * j * 0.1f;


                CharacterFoodList.Add(F);


        }*/
    }

    private void FillByType(Transform TablePos, List<Food> CharacterDrinkList, DrinkProperties array)
    {

            Food F = GameController.getFoodFromPool();

            F.FillProperties(array);

            F.tag = "Drink";
            F.transform.SetParent(TablePos.Find("Drink"), false);
            F.transform.localPosition = Vector3.up * 0.01f;

            CharacterDrinkList.Add(F);
    }


    public void unloadFoods()
    {
        for (int i = PlayerFoods.Count - 1; i >= 0; i--)
        {
            GameController.returnFoodToPool(PlayerFoods[i], PlayerFoods);
        }
        for (int i = PlayerDrinks.Count - 1; i >= 0; i--)
        {
            GameController.returnFoodToPool(PlayerDrinks[i], PlayerDrinks);
        }
        for (int i = EnemyFoods.Count - 1; i >= 0; i--)
        {
            GameController.returnFoodToPool(EnemyFoods[i], EnemyFoods);
        }
        for (int i = EnemyDrinks.Count - 1; i >= 0; i--)
        {
            GameController.returnFoodToPool(EnemyDrinks[i], EnemyDrinks);
        }
    }

    public bool? checkWinner()
    {
        if (PlayerFoods.Count == 0)
        {
            return true;
        }
        else if (EnemyFoods.Count == 0)
        {
            return false;
        }
        else
        {
            return new bool?();
        }
        
    }
}
