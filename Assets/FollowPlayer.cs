using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public static FollowPlayer instance;
    public Transform player; // The player's transform
    public float height = 2.0f; // Height above the player
    public float smoothSpeed = 5.0f; // Smoothing speed for camera movement
    public float distance = 10f;


    private void Start()
    {
        instance = this;
        GameObject playerTagGet = GameObject.FindWithTag("Player");
        if(playerTagGet != null)
        {
            player = playerTagGet.transform;
        }
        else
        {
            StartCoroutine(playerTagCoroutine());
        }
    }
    private void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("No target (player) assigned to the camera script.");
            return;
        }

        // Calculate the desired position for the camera
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y + height, player.position.z - distance);

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
    IEnumerator playerTagCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject playerTagGet = GameObject.FindWithTag("Player");
        if (playerTagGet != null)
        {
            player = playerTagGet.transform;
        }
        else
        {
            StartCoroutine(playerTagCoroutine());
        }
    }

    public void FollowStartFunc()
    {
        GameObject playerTagGet = GameObject.FindWithTag("Player");
        if (playerTagGet != null)
        {
            player = playerTagGet.transform;
        }
        else
        {
            StartCoroutine(playerTagCoroutine());
        }
    }
}
