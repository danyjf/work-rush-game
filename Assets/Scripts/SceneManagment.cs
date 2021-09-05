using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagment : MonoBehaviour {
    public void StartGame() {
        SceneManager.LoadScene("Intro");
    }

    public void Load(string name) {
        SceneManager.LoadScene(name);
    }
}
