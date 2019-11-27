﻿using Microsoft.MixedReality.Toolkit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckingPhaseManager : MonoBehaviour
{
    public GameObject Key;
    public GameObject House;
    public GameObject Tree;
    public GameObject Apple;

    private List<GameObject> GameObjects;

    private bool checking = false;

    void Start()
    {
        EventManager.StartListening("CheckingPhase", HandleStartCheckingPhase);
    }

    private void Update()
    {
        
        GameObjects = new List<GameObject>
        {
            House,
            Tree,
            Key,
            Apple
        };
        foreach (GameObject o in GameObjects)
        {
            Debug.Log(o.ToString());
        }
        
    }

    private void HandleStartCheckingPhase()
    {
        Debug.Log("Trigger works");
        CheckingPhase();
    }

    //Spawn the objects in random order and ask the user to pick a specific one
    private void CheckingPhase()
    {
        CreateAllObjectsAndDisplayInRandomOrder(GameObjects);
    }

    private void CreateAllObjectsAndDisplayInRandomOrder(List<GameObject> gameObjects)
    {
        List<GameObject> randomList = new List<GameObject>();

        for (int i = 0; i < GameObjects.Count; i++)
        {
            System.Random rnd = new System.Random();
            int index = rnd.Next(GameObjects.Count);
            randomList.Add(GameObjects[i]);
        }

        Vector3 startPosition = new Vector3(-0.75f, 0, 1);

        foreach (GameObject obj in GameObjects)
        {
            GameObject objectToCreate = Instantiate(obj) as GameObject;
            objectToCreate.transform.position = startPosition;
            startPosition += new Vector3(0.5f, 0, 0);
        }

        //TODO: the virtual assistant tells the user the object to find
        var test = House.AddComponent<Interactable>();
        Debug.Log(test);
        var h = test.AddReceiver<InteractableOnFocusReceiver>();
        Debug.Log(h);
        h.OnFocusOn.AddListener(() => Debug.Log("I'm getting focused"));
        h.OnFocusOff.AddListener(() => Debug.Log("I'm getting !focused"));
        //End of CheckingPhase
        End();
    }

    //Stop listening to events and trigger the new phase
    private void End()
    {
        EventManager.StopListening("CheckingPhase", HandleStartCheckingPhase);
        //start the checking phase
        //TODO: find a better way to call the method

        //Trigger the new phase

    }
}