using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelManager
{

    public Transform SceneObject, PlayerTableLoc, EnemyTableLoc;

    GameObject Room;

    public List<Level> Levels;
    public int CurrentLevel;
    public int NextLevel;

    public void LoadLevel(int level)
    {
        GameController.instance.Player.transform.localPosition = Vector3.zero;
        GameController.instance.Enemy.transform.localPosition = Vector3.zero;


        Level L = Levels[level];


        L.init();
        GameController.instance.Enemy.DressModel(L.EnemySkin);


        SceneTextures texts = GameController.getSceneTextures(L.levelBackground);

        L.fillFoods(PlayerTableLoc, EnemyTableLoc);


        Room = UnityEngine.Object.Instantiate(texts.Room);
        Room.transform.SetParent(SceneObject);



        PlayerTableLoc.gameObject.GetComponent<MeshFilter>().mesh = texts.TableTest;
        PlayerTableLoc.gameObject.GetComponent<MeshRenderer>().material = texts.TableM;
        EnemyTableLoc.gameObject.GetComponent<MeshFilter>().mesh = texts.TableTest;
        EnemyTableLoc.gameObject.GetComponent<MeshRenderer>().material = texts.TableM;

        PlayerTableLoc.Find("MainDish").GetComponent<MeshFilter>().mesh = texts.MainDishPlate;
        PlayerTableLoc.Find("MainDish").GetComponent<MeshRenderer>().material = texts.MainDishPlateM;
        PlayerTableLoc.Find("MainDish").GetComponent<MeshCollider>().sharedMesh = texts.MainDishPlate;

        PlayerTableLoc.Find("FirstDish").GetComponent<MeshFilter>().mesh = texts.FirstDishPlate;
        PlayerTableLoc.Find("FirstDish").GetComponent<MeshRenderer>().material = texts.FirstDishPlateM;
        PlayerTableLoc.Find("FirstDish").GetComponent<MeshCollider>().sharedMesh = texts.FirstDishPlate;

        EnemyTableLoc.Find("MainDish").GetComponent<MeshFilter>().mesh = texts.MainDishPlate;
        EnemyTableLoc.Find("MainDish").GetComponent<MeshRenderer>().material = texts.MainDishPlateM;
        EnemyTableLoc.Find("MainDish").GetComponent<MeshCollider>().sharedMesh = texts.MainDishPlate;

        EnemyTableLoc.Find("FirstDish").GetComponent<MeshFilter>().mesh = texts.FirstDishPlate;
        EnemyTableLoc.Find("FirstDish").GetComponent<MeshRenderer>().material = texts.FirstDishPlateM;
        EnemyTableLoc.Find("FirstDish").GetComponent<MeshCollider>().sharedMesh = texts.FirstDishPlate;




        //GameObject PTable = UnityEngine.Object.Instantiate(texts.Table);
        //PTable.transform.SetParent(PlayerTableLoc, false);
        //GameObject ETable = UnityEngine.Object.Instantiate(texts.Table);
        //ETable.transform.SetParent(EnemyTableLoc, false);




        GameController.DressCharacters();

        GameController.instance.Player.cancelAnim = false;
        GameController.instance.Enemy.cancelAnim = false;

        GameController.instance.Player.playAnimation("Gesture_ANIM" , 1, true);
        GameController.instance.Enemy.playAnimation("Gesture_ANIM", 1, true);

        L.startLevel();
    }

    public void LoadLevelFoodless(int level)
    {
        GameController.instance.Player.transform.localPosition = Vector3.zero;
        GameController.instance.Enemy.transform.localPosition = Vector3.zero;


        Level L = Levels[level];


        L.init();
        GameController.instance.Enemy.DressModel(L.EnemySkin);


        SceneTextures texts = GameController.getSceneTextures(L.levelBackground);

        //L.fillFoods(PlayerTableLoc, EnemyTableLoc);


        Room = UnityEngine.Object.Instantiate(texts.Room);
        Room.transform.SetParent(SceneObject);



        PlayerTableLoc.gameObject.GetComponent<MeshFilter>().mesh = texts.TableTest;
        PlayerTableLoc.gameObject.GetComponent<MeshRenderer>().material = texts.TableM;
        EnemyTableLoc.gameObject.GetComponent<MeshFilter>().mesh = texts.TableTest;
        EnemyTableLoc.gameObject.GetComponent<MeshRenderer>().material = texts.TableM;

        PlayerTableLoc.Find("MainDish").GetComponent<MeshFilter>().mesh = texts.MainDishPlate;
        PlayerTableLoc.Find("MainDish").GetComponent<MeshRenderer>().material = texts.MainDishPlateM;
        PlayerTableLoc.Find("MainDish").GetComponent<MeshCollider>().sharedMesh = texts.MainDishPlate;

        PlayerTableLoc.Find("FirstDish").GetComponent<MeshFilter>().mesh = texts.FirstDishPlate;
        PlayerTableLoc.Find("FirstDish").GetComponent<MeshRenderer>().material = texts.FirstDishPlateM;
        PlayerTableLoc.Find("FirstDish").GetComponent<MeshCollider>().sharedMesh = texts.FirstDishPlate;

        EnemyTableLoc.Find("MainDish").GetComponent<MeshFilter>().mesh = texts.MainDishPlate;
        EnemyTableLoc.Find("MainDish").GetComponent<MeshRenderer>().material = texts.MainDishPlateM;
        EnemyTableLoc.Find("MainDish").GetComponent<MeshCollider>().sharedMesh = texts.MainDishPlate;

        EnemyTableLoc.Find("FirstDish").GetComponent<MeshFilter>().mesh = texts.FirstDishPlate;
        EnemyTableLoc.Find("FirstDish").GetComponent<MeshRenderer>().material = texts.FirstDishPlateM;
        EnemyTableLoc.Find("FirstDish").GetComponent<MeshCollider>().sharedMesh = texts.FirstDishPlate;




        //GameObject PTable = UnityEngine.Object.Instantiate(texts.Table);
        //PTable.transform.SetParent(PlayerTableLoc, false);
        //GameObject ETable = UnityEngine.Object.Instantiate(texts.Table);
        //ETable.transform.SetParent(EnemyTableLoc, false);




        GameController.DressCharacters();

        GameController.instance.Player.cancelAnim = false;
        GameController.instance.Enemy.cancelAnim = false;

        GameController.instance.Player.playAnimation("Gesture_ANIM", 1, true);
        GameController.instance.Enemy.playAnimation("Gesture_ANIM", 1, true);

        //L.startLevel();
    }


    public void LoadNextLevel()
    {
        unloadLevel();
        Levels[CurrentLevel].unloadFoods();
        LoadLevel(NextLevel);

        CurrentLevel = NextLevel;
        NextLevel++;
    }

    public void ResetLevel()
    {


        unloadLevel();

        LoadLevel(CurrentLevel);
    }

    public void unloadLevel()
    {
        Levels[CurrentLevel].unloadFoods();

        UnityEngine.Object.Destroy(Room);

        GameController.instance.Enemy.StopAllCoroutines();
        
        GameController.instance.Player.StopAllCoroutines();
    }

    public bool? CheckVictory()
    {
        bool? test = Levels[CurrentLevel].checkWinner();

        return Levels[CurrentLevel].checkWinner();
    }
}
