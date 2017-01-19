using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ModalPanel : MonoBehaviour {

    public Text question;
    public Image iconImage;
    public Button yesButton;
    public Button noButton;
    public Button cancelButton;
    public Button okButton;
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

    /// <summary>
    /// Yes/No/Cancel: a string, a Yes event, a No event, and Cancel event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="yesEvent"></param>
    /// <param name="noEvent"></param>
    /// <param name="cancelEvent"></param>
    public void ResponseChoice(string question, UnityAction yesEvent, UnityAction noEvent, UnityAction cancelEvent) {
        modalPanelObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);
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

        yesButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        cancelButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        cancelButton.onClick.AddListener(cancelEvent);
        cancelButton.onClick.AddListener(ClosePanel);

        this.question.text = question;
        this.iconImage.sprite = iconImage;

        this.iconImage.gameObject.SetActive(true);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        okButton.gameObject.SetActive(false);
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

        okButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        okButton.onClick.AddListener(okEvent);
        okButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        if (iconImage) {
            this.iconImage.sprite = iconImage;
            this.iconImage.gameObject.SetActive(true);
        } else {
            this.iconImage.gameObject.SetActive(false);
        }
        
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Yes/No: A string, a Yes event, a No event.
    /// </summary>
    /// <param name="question"></param>
    /// <param name="yesEvent"></param>
    /// <param name="noEvent"></param>
    public void ResponseChoice(string question, UnityAction yesEvent, UnityAction noEvent) {
        modalPanelObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);

        noButton.onClick.RemoveAllListeners();
        // the AddListeners will do whatever is in the parentheses.
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);

        this.question.text = question;

        this.iconImage.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(false);
    }

    void ClosePanel() {
        modalPanelObject.SetActive(false);
    }
}