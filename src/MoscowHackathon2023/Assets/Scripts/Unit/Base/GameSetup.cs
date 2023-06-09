using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit.Base
{
    public class GameSetup : MonoBehaviour
    {
        private void Start()
        {
            QualitySettings.vSyncCount = 1;
#if !UNITY_EDITOR
        }
#else
            // Показывать FPS на самодельном канвасе только в нерелизных билдах.
            timeleft = updateInterval;
            CreateCanvas();
        }

        private Text fpsText;

        private void CreateCanvas()
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvasGO.transform.SetParent(transform);
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            GameObject textGO = new GameObject("FPS");
            textGO.transform.SetParent(canvas.transform);

            fpsText = textGO.AddComponent<Text>();
            fpsText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            fpsText.fontSize = 18;
            fpsText.alignment = TextAnchor.UpperLeft;

            RectTransform rectTransform = fpsText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0f, 1f); 
            rectTransform.anchorMax = new Vector2(0f, 1f);
            rectTransform.pivot = new Vector2(0f, 1f);
            rectTransform.anchoredPosition = new Vector2(10f, -10f);
        }

        public float updateInterval = 0.5f;

        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private float fps;

        private void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            if (timeleft <= 0.0f)
            {
                fps = accum / frames;
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;

                fpsText.text = ((int)fps).ToString() + " FPS";
            }
        }
#endif
    }
}