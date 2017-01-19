using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditive : MonoBehaviour {

	public void LoadAddOnClick(int level) {
        // Application.LoadLevelAdditive(level); // obsolete, new version below
        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }
}
