using UnityEngine;

public static class BlinkEvents
{
    public delegate void OnBlinkAction();
    public static event OnBlinkAction onBlink;
    public static void TriggerBlink() => onBlink?.Invoke();

    public delegate void OnBlinkHoldAction(float duration);
    public static event OnBlinkHoldAction onBlinkHold;
    public static void TriggerBlinkHold(float duration) => onBlinkHold?.Invoke(duration);

    public delegate void OnBlinkReleaseAction();
    public static event OnBlinkReleaseAction onBlinkRelease;
    public static void TriggerBlinkRelease() => onBlinkRelease?.Invoke();

    public static void ResetBlinkEvents()
    {
        onBlink = null;
        onBlinkHold = null;
        onBlinkRelease = null;
    }
}
