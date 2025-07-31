using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class LightCreationController : MonoBehaviour
{

    public VideoPlayer videoPlayer;

    [Header("Light Settings")]
    public Light directionalLight;
    public float lightIntensity = 1.5f;
    public float transitionDuration = 2f;

    [Header("Audio")]
    public AudioSource voiceClip;

    [Header("UI")]
    public Canvas instructionCanvas;
    public string instructionText = "Press Space to begin creation";

    [Header("Environment")]
    public GameObject visualElements;

    private bool hasLightBeenCreated = false;
    private Text instructionUIText;

    void Start()
    {
        
        if (directionalLight != null)
        {
            directionalLight.intensity = 0f;
        }

        if (visualElements != null)
            visualElements.SetActive(false);

        if (instructionCanvas != null)
        {
            instructionCanvas.enabled = true;
            instructionUIText = instructionCanvas.GetComponentInChildren<Text>();
            if (instructionUIText != null)
                instructionUIText.text = instructionText;
        }

        if (videoPlayer != null)
        {
        videoPlayer.Stop();
        videoPlayer.loopPointReached += OnVideoEnd;
        }
        
            
    }

    void Update()
    {
        if (!hasLightBeenCreated && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ActivateLight());
        }
    }

    private System.Collections.IEnumerator ActivateLight()
    {
        hasLightBeenCreated = true;

        if (instructionCanvas != null)
            instructionCanvas.enabled = false;

        if (voiceClip != null)
            voiceClip.Play();

        float elapsed = 0f;
        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsed / transitionDuration);
            if (directionalLight != null)
                directionalLight.intensity = Mathf.Lerp(0f, lightIntensity, normalizedTime);

            yield return null;
        }

        if (directionalLight != null)
            directionalLight.intensity = lightIntensity;

        if (visualElements != null)
            visualElements.SetActive(true);


        if (videoPlayer != null){
            videoPlayer.Play();
        }
    }
    

    private void OnVideoEnd(VideoPlayer vp)
{
    SceneManager.LoadScene("Day 2"); // Make sure Scene2 is in your build settings
}
}