using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventButtonDetails {
    public string buttonTitle;
    public Sprite buttonBackground;
    public UnityAction action;
}

public class ModalPanelDetails {
    public string title;
    public string question;
    public Sprite iconImage;
    public Sprite panelBackgroundImage;
    public EventButtonDetails button1Details;
    public EventButtonDetails button2Details;
    public EventButtonDetails button3Details;
}

public class ModalPanel : MonoBehaviour {

    public Text question;
    public Image iconImage;
    /* these buttons were renamed to the ones below
    public Button yesButton;
    public Button noButton;
    public Button cancelButton;
    public Button okButton;
    */
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public Text button1Text;
    public Text button2Text;
    public Text button3Text;
    public Text button4Text;

    public GameObject modalPanelObject;

    // gives easy access to this script: modified singleton style reference
    private static ModalPanel modalPanel;

    public static ModalPanel Instance() {
        if (!modalPanel) {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs ti be ibe actuve ModalPanel script on a GameObject in this scene.");
        }

        return modalPanel;
    }

    /* one way of doing this is params, but it has drawbacks
     * drawbacks are:
     * -- you cannot control or limit the params
     * -- slower than overloading
    public void NewChoice(string question, params EventButtonDetails[] buttonDetails) {

    }
    */

    /* another way:
     * drawbacks:
     * you need to fill out all the optional parameters of the EventButtonDetails before you can use the optional parameter of the Sprite iconImage
    public void NewChoice(string question, EventButtonDetails buttonDetails1, EventButtonDetails buttonDetails2 = null, EventButtonDetails buttonDetails3 = null, Sprite iconImage = null) {

    }
    */

    public void NewChoice(ModalPanelDetails details) {
        modalPanelObject.SetActive(true);

        this.iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);

        this.question.text = details.question;

        if (details.iconImage) {
            this.iconImage.sprite = details.iconImage;
            this.iconImage.gameObject.SetActive(true);
        }

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(details.button1Details.action);
        button1.onClick.AddListener(ClosePanel);
        button1Text.text = details.button1Details.buttonTitle;
        button1.gameObject.SetActive(true);

        if (details.button2Details != null) {
            button2.onClick.RemoveAllListeners();
            button2.onClick.AddListener(details.button2Details.action);
            button2.onClick.AddListener(ClosePanel);
            button2Text.text = details.button2Details.buttonTitle;
            button2.gameObject.SetActive(true);
        }

        if (details.button3Details != null) {
            button3.onClick.RemoveAllListeners();
            button3.onClick.AddListener(details.button3Details.action);
            button3.onClick.AddListener(ClosePanel);
            button3Text.text = details.button3Details.buttonTitle;
            button3.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Yes/No/Cancel: a string, a Yes event, a No event, and Cancel event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="yesEvent"></param>
    /// <param name="noEvent"></param>
    /// <param name="cancelEvent"></param>
    public void ResponseChoice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {
        modalPanelObject.SetActive(true);

        button1.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button1.onClick.AddListener(yesEvent);
        button1.onClick.AddListener(ClosePanel);

        button2.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button2.onClick.AddListener(noEvent);
        button2.onClick.AddListener(ClosePanel);

        button3.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button3.onClick.AddListener(cancelEvent);
        button3.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(false);
    }

    /// <summary>
    /// Override;
    /// Yes/No/Cancel with image: a string, a sprite, a Yes event, a No event, and Cancel event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="iconImage"></param>
    /// <param name="yesEvent"></param>
    /// <param name="noEvent"></param>
    /// <param name="cancelEvent"></param>
    public void ResponseChoice(string question, Sprite iconImage, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {
        modalPanelObject.SetActive(true);

        button1.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button1.onClick.AddListener(yesEvent);
        button1.onClick.AddListener(ClosePanel);

        button2.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button2.onClick.AddListener(noEvent);
        button2.onClick.AddListener(ClosePanel);

        button3.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button3.onClick.AddListener(cancelEvent);
        button3.onClick.AddListener(ClosePanel);

        this.question.text = question;
        this.iconImage.sprite = iconImage;

        this.iconImage.gameObject.SetActive(true);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(false);
    }

    /*
     * commenting this out, to show how to use optional parameters in the new version
    /// <summary>
    /// An announcement: a string and a Cancel/ok event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="okEvent"></param>
    public void ResponseChoice(string question, UnityAction okEvent) {
        modalPanelObject.SetActive(true);

        okButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        okButton.onClick.AddListener(okEvent);
        okButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }

    public void ResponseChoice(string question, Sprite iconImage, UnityAction okEvent) {
        modalPanelObject.SetActive(true);

        okButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        okButton.onClick.AddListener(okEvent);
        okButton.onClick.AddListener(ClosePanel);

        this.question.text = question;
        this.iconImage.sprite = iconImage;

        this.iconImage.gameObject.SetActive(true);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }
    */

    public void ResponseChoice(string question, UnityAction okEvent, Sprite iconImage = null) {
        modalPanelObject.SetActive(true);

        button4.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button4.onClick.AddListener(okEvent);
        button4.onClick.AddListener(ClosePanel);

        this.question.text = question;

        if (iconImage) {
            this.iconImage.sprite = iconImage;
            this.iconImage.gameObject.SetActive(true);
        } else {
            this.iconImage.gameObject.SetActive(false);
        }
        
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(true);
    }

    /// <summary>
    /// Yes/No: A string, a Yes event, a No event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="yesEvent"></param>
    /// <param name="noEvent"></param>
    public void ResponseChoice(string question, UnityAction yesEvent, UnityAction noEvent) {
        modalPanelObject.SetActive(true);

        button1.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button1.onClick.AddListener(yesEvent);
        button1.onClick.AddListener(ClosePanel);

        button2.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        button2.onClick.AddListener(noEvent);
        button2.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
    }

    void ClosePanel() {
        modalPanelObject.SetActive(false);
    }
}