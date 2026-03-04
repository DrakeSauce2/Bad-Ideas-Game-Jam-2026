using System.Collections;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private RectTransform upperEyelid;
    private readonly Vector3 upperEyelidOpenPos = new Vector3(0f, 900f, 0f);
    private readonly Vector3 upperEyelidClosedPos = new Vector3(0f, -295f, 0f);

    [SerializeField] private RectTransform lowerEyelid;
    private readonly Vector3 lowerEyelidOpenPos = new Vector3(0f, -900f, 0f);
    private readonly Vector3 lowerEyelidClosedPos = new Vector3(0f, 295f, 0f);

    //

    private bool bIsBlinking = false;

    //

    private void OnEnable()
    {
        BlinkEvents.onBlink += Blink;
        BlinkEvents.onBlinkRelease += Unblink;
    }

    private void OnDisable()
    {
        BlinkEvents.onBlink -= Blink;
        BlinkEvents.onBlinkRelease -= Unblink;
    }

    public void Blink()
    {
        if (bIsBlinking) return;

        upperEyelid.localPosition = upperEyelidClosedPos;
        lowerEyelid.localPosition = lowerEyelidClosedPos;

        bIsBlinking = true;
    }

    private void Unblink()
    {
        upperEyelid.localPosition = upperEyelidOpenPos;
        lowerEyelid.localPosition = lowerEyelidOpenPos;

        bIsBlinking = false;
    }

}
