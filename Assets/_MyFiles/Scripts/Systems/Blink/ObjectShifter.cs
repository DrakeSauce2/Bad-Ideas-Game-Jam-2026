using UnityEngine;

public class ObjectShifter : MonoBehaviour
{
    private MeshFilter meshRenderer;

    [SerializeField] Mesh[] meshesToShift;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshFilter>();
    }

    private void OnBecameVisible()
    {
        BlinkEvents.onBlink += ShiftObject;
    }

    private void OnBecameInvisible()
    {
        BlinkEvents.onBlink -= ShiftObject;
    }

    private void ShiftObject()
    {
        if(meshesToShift.Length == 0) return;

        Debug.Log("Shifting object: " + gameObject.name);
        
        int randomIndex = Random.Range(0, meshesToShift.Length);
        meshRenderer.mesh = meshesToShift[randomIndex];
    }
}
