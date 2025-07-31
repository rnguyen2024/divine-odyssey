using UnityEngine;
using UnityEngine.UI;

public class WaterDragReveal : MonoBehaviour
{
    [Header("Water Objects")]
    public Transform leftWater;
    public Transform rightWater;
    public GameObject bottomLayerGround;

    [Header("Drag Settings")]
    public float maxDragDistance = 100f; // Pixels the player must drag downward
    public float waterMoveAmount = 5f;   // How far the water should spread (Unity units)
    public float waterMoveSpeed = 5f;    // Movement smoothing

    [Header("Sun and Clouds")]
    public Transform sun;
    public float sunMoveAmount = 2f;     // How far the sun should descend
    public float sunMoveSpeed = 2f;
    public SpriteRenderer cloud1;
    public SpriteRenderer cloud2;
    public float cloudFadeSpeed = 1f;

    [Header("UI Message")]
    public Text messageText; // Assign a UI Text object in the Inspector
    public float messageDisplayTime = 4f;

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
            FadeOutCloud(cloud1);
            FadeOutCloud(cloud2);
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

    System.Collections.IEnumerator HideMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (messageText != null)
            messageText.text = "";
    }
}
