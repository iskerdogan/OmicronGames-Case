
using UnityEngine;

namespace Utility
{
    public class FpsCounter : MonoBehaviour
    { 
        public float updateInterval = 0.5f;
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private float fps;
        GUIStyle textStyle = new GUIStyle();

        private void Start()
        {
            timeleft = updateInterval;

            textStyle.fontSize = 32;
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = new Color(173f, 216f, 230f);
        }

        private void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (!(timeleft <= 0.0)) return;
            fps = (accum / frames);
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
        private void OnGUI()
        {
            GUI.Label(new Rect(5, 255, 400, 100), fps.ToString("F1") + "FPS", textStyle);
        }
    }
}