using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCamera : MonoBehaviour
{
    private PlayerController playerController;

    //

    [Header("Camera Settings")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float lookSensitivity = 150f;

    [SerializeField] private float maxPitch = 80f;
    [SerializeField] private float minPitch = -80f;
    private float currrentPitch = 0f;

    //

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();

        playerController.onLook += HandleLook;
    }

    private void HandleLook(Vector2 lookInput)
    {
        currrentPitch += Time.deltaTime * lookInput.y * lookSensitivity;
        currrentPitch = Mathf.Clamp(currrentPitch, minPitch, maxPitch);

        cameraTransform.localRotation = Quaternion.Euler(-currrentPitch, 0f, 0f);
    }
}
