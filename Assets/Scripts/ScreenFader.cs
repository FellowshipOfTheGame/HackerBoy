using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    [UnityEngine.SerializeField]
    public Image fadeImg;
    public Image fadeImgPrefab;

    public float speed = 1.5f;

    public float minAlpha = 0.05f;
    public float maxAlpha = 0.90f;

    private bool _isFading = false;
    public bool isFading { get; private set; }

    void Awake() {

        // If no black image exists, create a new one
        if(GameObject.Find("Black") == null){
            fadeImg = Instantiate(fadeImgPrefab);
            fadeImg.transform.SetParent(
                GameObject.Find("Canvas").transform, false);
        }

        fadeImg.rectTransform.localScale = new Vector2(
            Screen.width,
            Screen.height
        );
        fadeImg.enabled = false;
    }

    private void FadeOut() {
        fadeImg.color = Color.Lerp(
            fadeImg.color,
            Color.clear,
            speed*Time.deltaTime
        );
    }

    private void FadeIn() {
        fadeImg.color = Color.Lerp(
            fadeImg.color,
            Color.black,
            speed*Time.deltaTime
        );
    }

    public void StartFadeIn(){
        fadeImg.color = Color.clear; // Make sure a black screen doesnt pop out
        StartCoroutine("FadeInRoutine");
    }

    public void StartFadeOut(){
        StartCoroutine("FadeOutRoutine");
    }

    private IEnumerator FadeInRoutine() {
        Debug.Log("[DEBUG]: Starting fade in");
        
        // Make sure the image is enabled
        fadeImg.enabled = true;
        _isFading = true;

        do {
            // Start fading in
            FadeIn();

            // Let some alpha 
            if (fadeImg.color.a >= maxAlpha) {

                Debug.Log("[DEBUG]: Fadein ended");
                fadeImg.color = Color.black;
                _isFading = false;
                yield break;

            } else yield return null;

        } while (true);
    }

    private IEnumerator FadeOutRoutine() {
        Debug.Log("[DEBUG]: Starting fade out");
        
        // Make sure the image is enabled
        fadeImg.enabled = true;
        do {

            FadeOut();

            // Let some alpha 
            if (fadeImg.color.a <= minAlpha) {

                Debug.Log("[DEBUG]: Fadeout ended");
                _isFading = false;
                fadeImg.color = Color.clear;
                fadeImg.enabled = false;
                yield break;

            } else yield return null;
        } while (true);
    }
}
