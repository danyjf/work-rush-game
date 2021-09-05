using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour {
    public Text text;

    private void Start() {
        text.text = "";
        StartCoroutine(WaitAndCallWith(3.0f, "ShowMessage", "$#&%#@!!!"));
    }

    private IEnumerator WaitAndCallWith(float time, string coroutine, string message) {
        yield return new WaitForSeconds(time);

        StartCoroutine(coroutine, message);
    }

    private IEnumerator ShowMessage(string message) {
        text.text = "";

        for(int i = 0; i < message.Length; i++) {
            text.text += message[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Game");
    }
}
