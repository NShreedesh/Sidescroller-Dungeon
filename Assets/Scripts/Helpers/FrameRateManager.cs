using UnityEngine;

namespace Helpers
{
    public class FrameRateManager : MonoBehaviour
    {
        [SerializeField]
        private bool shouldSetGivenFps;
        [SerializeField]
        private int fps = 60;

        private void Start()
        {
            Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;

            if (shouldSetGivenFps)
            {
                Application.targetFrameRate = fps;
            }
        }
    }
}