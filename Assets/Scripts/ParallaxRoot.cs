using UnityEngine;

public class ParallaxRoot : MonoBehaviour
{
    [SerializeField] Transform backgroundLayer;
    [SerializeField] float backgroundSpeed = 0.1f;

    [SerializeField] Transform midgroundLayer;
    [SerializeField] float midgroundSpeed = 0.5f;

    [SerializeField] Transform foregroundLayer;
    [SerializeField] float foregroundSpeed = 1.0f;
    [SerializeField] private float tolerance = 50;

    private Transform playerTransform;
    void Start()
    {
        playerTransform = Player.Instance.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Not use parallax if player is not centered 
        if (IfPlayerCenteredScreen())
        {
            float playerX = playerTransform.position.x;

            float backgroundOffset = playerX * backgroundSpeed;
            backgroundLayer.position = new Vector3(-backgroundOffset, backgroundLayer.position.y, backgroundLayer.position.z);

            float midgroundOffset = playerX * midgroundSpeed;
            midgroundLayer.position = new Vector3(-midgroundOffset, midgroundLayer.position.y, midgroundLayer.position.z);

            float foregroundOffset = playerX * foregroundSpeed;
            foregroundLayer.position = new Vector3(-foregroundOffset, foregroundLayer.position.y, foregroundLayer.position.z);
        }
    }

    private bool IfPlayerCenteredScreen()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(playerTransform.position);

        float distanceToCenter = Mathf.Abs(playerScreenPosition.x - Screen.width / 2);

        if (distanceToCenter <= tolerance)
        {
            return true;
        }

        return false;
    }
}
