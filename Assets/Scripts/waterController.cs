using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WaterDragReveal : MonoBehaviour
{
    
    
    [Header("Water Objects")]
    public Transform leftWater;
    public Transform rightWater;
    public GameObject bottomLayerGround;

    [Header("Drag Settings")]
    public float maxDragDistance = 60f; // Pixels the player must drag downward
    public float waterMoveAmount = 10f;   // How far the water should spread (Unity units)
    public float waterMoveSpeed = 2f;    // Movement smoothing

    [Header("Sun and Clouds")]
    public Transform sun;
    public float sunMoveAmount = 3f;     // How far the sun should descend
    public float sunMoveSpeed = 2f;
    public SpriteRenderer cloud1;
    public SpriteRenderer cloud2;
    public float cloudFadeSpeed = 1f;

    [Header("UI Message")]
    public Text messageText; // Assign a UI Text object in the Inspector
    public float messageDisplayTime = 4f;

    [Header("Void Background")]
    public SpriteRenderer voidBackground; // Assign this in the Inspector
    public float backgroundFadeAmount = 1f; // Full fade from opaque to transparent


    private Vector2 dragStart;
    private bool isDragging = false;
    private float dragProgress = 0f;
    private bool hasRevealed = false;
    private bool animateExtras = false;

    private Vector3 leftStartPos;
    private Vector3 rightStartPos;
    private Vector3 sunStartPos;

    void Start()
    {
        leftStartPos = leftWater.position;
        rightStartPos = rightWater.position;

        if (sun != null)
            sunStartPos = sun.position;

        if (bottomLayerGround != null)
            bottomLayerGround.SetActive(false);

        if (messageText != null)
            messageText.text = ""; // Start with empty message

        if (cloud1 != null)
        {
            Color c = cloud1.color;
            c.a = 0f;
            cloud1.color = c;
        }
        if (cloud2 != null)
        {
            Color c = cloud2.color;
            c.a = 0f;
            cloud2.color = c;
        }
    
    }

    void Update()
    {
        // Handle Dragging
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && !hasRevealed)
        {
            float dragDistance = Input.mousePosition.y - dragStart.y;
            dragProgress = Mathf.Clamp01(-dragDistance / maxDragDistance);

            // Move water left and right based on downward drag
            Vector3 leftTarget = leftStartPos + Vector3.left * waterMoveAmount * dragProgress;
            Vector3 rightTarget = rightStartPos + Vector3.right * waterMoveAmount * dragProgress;

            leftWater.position = Vector3.Lerp(leftWater.position, leftTarget, Time.deltaTime * waterMoveSpeed);
            rightWater.position = Vector3.Lerp(rightWater.position, rightTarget, Time.deltaTime * waterMoveSpeed);

            // Move sun downward proportionally
            if (sun != null)
            {
                Vector3 sunTarget = sunStartPos + Vector3.down * sunMoveAmount * dragProgress;
                sun.position = Vector3.Lerp(sun.position, sunTarget, Time.deltaTime * sunMoveSpeed);
            }

            if (dragProgress >= 1f)
            {
                RevealGround();
            }
        }

        // Animate Cloud Fade
        if (animateExtras)
        {
            FadeInCloud(cloud1);
            FadeInCloud(cloud2);
        }

        // Fade out the void background based on drag progress
        if (voidBackground != null)
        {
            Color bgColor = voidBackground.color;
            bgColor.a = Mathf.Lerp(1f, 1f - backgroundFadeAmount, dragProgress);
            voidBackground.color = bgColor;
        }

    }

    void RevealGround()
    {
        if (hasRevealed) return;
        hasRevealed = true;

        if (bottomLayerGround != null)
            bottomLayerGround.SetActive(true);

        animateExtras = true;

        if (messageText != null)
        {
            messageText.text = "🌍 Ground revealed! The firmament is set.";
            StartCoroutine(HideMessageAfterSeconds(messageDisplayTime));
        }

        Debug.Log("🌍 Ground revealed! The firmament is set.");
    }

    void FadeOutCloud(SpriteRenderer cloud)
    {
        if (cloud == null) return;

        Color color = cloud.color;
        color.a = Mathf.MoveTowards(color.a, 0f, Time.deltaTime * cloudFadeSpeed);
        cloud.color = color;
    }

    void FadeInCloud(SpriteRenderer cloud)
    {
        if (cloud == null) return;

        Color color = cloud.color;
        color.a = Mathf.MoveTowards(color.a, 1f, Time.deltaTime * cloudFadeSpeed);
        cloud.color = color;
    }


    System.Collections.IEnumerator HideMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (messageText != null)
            messageText.text = "";
            SceneManager.LoadScene("Day 3");
    }
    
}
