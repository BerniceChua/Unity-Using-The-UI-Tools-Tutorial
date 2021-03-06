﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TestModalWindow : MonoBehaviour {
    public Sprite icon;

    public Transform spawnpoint;
    public GameObject thingToSpawn;

    private ModalPanel modalPanel;
    private DisplayManager displayManager;

    private UnityAction myYesAction;
    private UnityAction myNoAction;
    private UnityAction myCancelAction;
    private UnityAction myOkAction;

    void Awake() {
        modalPanel = ModalPanel.Instance();
        displayManager = DisplayManager.Instance();

        myYesAction = new UnityAction(TestYesFunction);
        myNoAction = new UnityAction(TestNoFunction);
        myCancelAction = new UnityAction(TestCancelFunction);
        myOkAction = new UnityAction(TestOkFunction);
    }

    // Send to the Modal Panel to set up the Buttons and Functions to call
    public void TestYesNoCancel() {
        modalPanel.ResponseChoice("Would you like a poke in the eye?\nHow about with a sharp stick?", myYesAction, myNoAction, myCancelAction);
        /* 
         * alternate version is that you can put the functions directly into the parameters (remember Lambda functions?)
         * example:
         * modalPanel.ResponseChoice("Would you like a poke in the eye?\nHow about with a sharp stick?", TestYesFunction, TestNoFunction, TestCancelFunction);
         */
    }

    // Send to the Modal Panel to set up the Buttons and Functions to call (with Image!!)
    public void TestYesNoCancelwithImage() {
        modalPanel.ResponseChoice("Do you like this icon?", icon, myYesAction, myNoAction, myCancelAction);
    }

    // Send to the Modal Panel to set up the Buttons and Functions to call (with Image!!)
    public void TestAnnouncement() {
        //modalPanel.ResponseChoice("You will get through this", myOkAction);
        //modalPanel.ResponseChoice("You will get through this", TestOkFunction);

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails();
        modalPanelDetails.question = "You will get through this";
        modalPanelDetails.button1Details = new EventButtonDetails();
        modalPanelDetails.button1Details.buttonTitle = "OK";
        modalPanelDetails.button1Details.action = TestOkFunction;

        modalPanel.NewChoice(modalPanelDetails);
    }

    public void TestAnnouncementWithIcon() {
        //modalPanel.ResponseChoice("This icon was from the Survival Shooter tutorial.", myOkAction, icon);
        //modalPanel.ResponseChoice("This icon was from the Survival Shooter tutorial.", TestOkFunction, icon);

        /*
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails();
        modalPanelDetails.question = "This icon was from the Survival Shooter tutorial.";
        modalPanelDetails.button1Details = new EventButtonDetails();
        modalPanelDetails.iconImage = icon;
        modalPanelDetails.button1Details.buttonTitle = "OK";
        modalPanelDetails.button1Details.action = TestOkFunction;
        */
        // refactor this to be an initializer list -- make custom initializer with new ModalPanelDetails{} instead of new ModalPanelDetails().
        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            question = "This icon was from the Survival Shooter tutorial.",
            iconImage = icon,
            button1Details = new EventButtonDetails { buttonTitle = "OK", action = TestOkFunction }
        };

        modalPanel.NewChoice(modalPanelDetails);
    }

    // Send to the Modal Panel to set up the Buttons and Functions to call (with Image!!)
    public void TestYesNo() {
        modalPanel.ResponseChoice("Am I awesome yet?", myYesAction, myNoAction);
    }


    public void TestLambda() {
        modalPanel.ResponseChoice("Do you want to create a sphere?", () => { InstantiateObject(thingToSpawn); }, myNoAction);
    }

    public void Test2Lambdas() {
        modalPanel.ResponseChoice("Do you want to create 2 spheres?", () => { InstantiateObject(thingToSpawn, thingToSpawn); }, myNoAction);
    }

    public void Test3Lambdas() {
        //modalPanel.ResponseChoice("Do you want to create 3 spheres?", () => { InstantiateObject(thingToSpawn, thingToSpawn); InstantiateObject(thingToSpawn); }, myNoAction);
        //modalPanel.ResponseChoice("Do you want to create 3 spheres?", () => { InstantiateObject(thingToSpawn, thingToSpawn); InstantiateObject(thingToSpawn); }, TestNoFunction);

        ModalPanelDetails modalPanelDetails = new ModalPanelDetails {
            question = "Do you want to create 3 spheres?",
            button1Details = new EventButtonDetails {
                buttonTitle = "Yes, please!",
                action = () => {
                    InstantiateObject(thingToSpawn, thingToSpawn);
                    InstantiateObject(thingToSpawn);
                }
            },
            button2Details = new EventButtonDetails {
                buttonTitle = "No",
                action = TestNoFunction
            }
        };

        modalPanel.NewChoice(modalPanelDetails);
    }

    public void Test4Lambdas() {
        modalPanel.ResponseChoice("Do you want to create 4 spheres?", () => { int spawnCount = 4; for (int i = 0; i < spawnCount; i++) { InstantiateObject(thingToSpawn); } }, myNoAction);
    }

    public void Test5Lambdas() {
        //modalPanel.ResponseChoice("Do you want to create 5 spheres?", () => { int spawnCount = 5; for (int i = 0; i < spawnCount; i++) { InstantiateObject(thingToSpawn, spawnCount); } }, myNoAction);
        modalPanel.ResponseChoice("Do you want to create 5 spheres?", () => { int spawnCount = 5; for (int i = 0; i < spawnCount; i++) { InstantiateObject(thingToSpawn, spawnCount); } }, TestNoFunction);
    }

    // These are wrapped into UnityActions
    void TestYesFunction() {
        displayManager.DisplayMessage("Hell yeah!!  Yup!");
    }

    void TestNoFunction() {
        displayManager.DisplayMessage("No way!");
    }

    void TestCancelFunction() {
        displayManager.DisplayMessage("I give up...");
    }

    void TestOkFunction() {
        displayManager.DisplayMessage("OK");
    }

    void InstantiateObject(GameObject thingToInstantiate) {
        displayManager.DisplayMessage("Here you go!");
        Instantiate(thingToInstantiate, spawnpoint.position, spawnpoint.rotation);
    }

    void InstantiateObject(GameObject thingToInstantiate, GameObject thingToInstantiate2) {
        displayManager.DisplayMessage("Here you go!");
        Instantiate(thingToInstantiate, spawnpoint.position - Vector3.one, spawnpoint.rotation);
        Instantiate(thingToInstantiate2, spawnpoint.position + Vector3.one, spawnpoint.rotation);
    }

    void InstantiateObject(GameObject thingToInstantiate, int instantiateCount) {
        displayManager.DisplayMessage("Here you go!");

        for (int i = 0; i < instantiateCount; i++) {
            Instantiate(thingToInstantiate, spawnpoint.position + (Vector3.one * i), spawnpoint.rotation);
        }
        
    }

}