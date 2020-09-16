using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public UImanager UImanager;

    bool doneRound, gameStarted;

    public LevelManager Levels;
    //float DifficultyMod;
    public Player Player;
    public Enemy Enemy;
    public SoundManager SM;
    public UImanager UI;

    public static GameController instance;

    public FoodProperties[] FoodTypes;
    public DrinkProperties DrinkDetails;
    public Model[] CharacterModels;
    public SceneTextures[] LevelBackgrounds;
    public List<Food> FoodPool;
    public Food FoodPrefab;

    public bool? VictoryCheck;

    public Transform PlayerFood, EnemyFood;

    public Button PauseB, resetB, nextB;

    public Transform NomralELoc, PonyLoc;
    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        doneRound = false;
        VictoryCheck = null;
        instance = this;
        FoodPool = new List<Food>();
        for (int i = 0; i < 500; i++)
        {
            Food F = Instantiate<Food>(FoodPrefab);
            F.name += i;
            FoodPool.Add(F);
        }

        UI.init();
        Player.init();
        Enemy.init();
        Levels.LoadLevelFoodless(Levels.CurrentLevel);

        //startGame();
    }

    // Update is called once per frame
    void Update()
    {

        if (VictoryCheck.HasValue == false && Time.timeScale != 0 && gameStarted)
        {
            VictoryCheck = Levels.CheckVictory();
        }


    }

    public void TogglePause()
    {
        if (Time.timeScale == 1)
        {
            TogglePause(true);
            //nextB.gameObject.SetActive(true);
        }
        else
        {
            TogglePause(false);
            //nextB.gameObject.SetActive(false);
        }
    }

    public void TogglePause(bool tog)
    {
        if (!tog)
        {
            //Player.ToggleControl(true);
            //Enemy.ToggleControl(true);
            //resetB.gameObject.SetActive(false);
            nextB.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            //Player.ToggleControl(false);
            //Enemy.ToggleControl(false);
            //resetB.gameObject.SetActive(true);
            nextB.gameObject.SetActive(true);
            Time.timeScale = 0;

        }
    }


    public IEnumerator victoryRoutine()
    {

        while (VictoryCheck.HasValue == false)
        {
            yield return null;

        }

        Player.ToggleControl(false);
        Enemy.ToggleControl(false);
        Player.StopAllCoroutines();
        Enemy.StopAllCoroutines();

        Player.anim.Stop();
        Enemy.anim.Stop();

        PauseB.interactable = false;



        if (VictoryCheck == true)
        {
            UImanager.StomachBar.gameObject.SetActive(false);

            //nextB.gameObject.SetActive(true);

            Player.playAnimation("Win_ANIM", 1, false);
            //Player.CharCam.GetComponent<Animation>().Play();
            Enemy.playAnimation("Lose_ANIM", 1, false);
        }
        else if (VictoryCheck == false)
        {
            resetB.gameObject.SetActive(true);
            Player.playAnimation("Lose_ANIM", 1, false);
            Enemy.playAnimation("Win_ANIM", 1, false);
        }

        doneRound = true;
    }

    public void startGame()
    {

        PauseB.interactable = true;
        Levels.LoadLevel(Levels.CurrentLevel);
        StartCoroutine(victoryRoutine());
        gameStarted = true;
    }

    public void NextLevel()
    {
        Levels.LoadNextLevel();
        PauseB.interactable = true;
        TogglePause(false);
        //resetB.gameObject.SetActive(false);
        nextB.gameObject.SetActive(false);
        VictoryCheck = null;
        UImanager.StomachBar.gameObject.SetActive(true);
        if (doneRound)
        {
            doneRound = false;
            StartCoroutine(victoryRoutine());
        }
    }

    public void resetLevel()
    {
        VictoryCheck = null;
        Levels.ResetLevel();
        PauseB.interactable = true;
        TogglePause(false);
        UImanager.StomachBar.gameObject.SetActive(true);
        //resetB.gameObject.SetActive(false);
        if (doneRound)
        {
            doneRound = false;
            StartCoroutine(victoryRoutine());
        }
    }

    public static Model getModel(int i)
    {
        return instance.CharacterModels[i];
    }

    public static FoodProperties getFood(int i)
    {
        return instance.FoodTypes[i];
    }

    public static SceneTextures getSceneTextures(int i)
    {
        return instance.LevelBackgrounds[i];
    }

    public static void DressCharacters()
    {
        instance.Player.DressModel(instance.Player.modelNum);
        //instance.Enemy.DressModel(instance.Enemy.modelNum);
    }

    public static Food getFoodFromPool()
    {
        Food F = instance.FoodPool[0];
        instance.FoodPool.RemoveAt(0);

        return F;
    }

    public static void returnFoodToPool(Food F, List<Food> originList)
    {

        F.transform.SetParent(null);

        originList.Remove(F);

        Destroy(F.GetComponent<Rigidbody>());
        //Destroy(F.GetComponent<MeshCollider>());
            F.transform.position = instance.FoodPrefab.transform.position;

        instance.FoodPool.Add(F);
    }


}
