using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MyCheats
{
    public class CheatsMono : MonoBehaviour
    {
        public List<CheatBase> cheats = new();

        private readonly float timeClearInput = 3;
        private readonly float timeShowingText = 4;
        private float timePrewInput;
        private bool needShowText;

        private readonly StringBuilder inputed = new();
        private WaitForSecondsRealtime timer;
        private CheatBase currentCheat;
        private GUIStyle currentStyle;
        private Coroutine coroutine;


        private void Awake()
        {
            timer = new WaitForSecondsRealtime(timeShowingText);
        }

        private void Update()
        {
            if (!string.IsNullOrEmpty(Input.inputString))
            {
                char ch = Input.inputString.ToLower()[0];
                ch = ch.RusToEngKeyboardLayout();
                inputed.Append(ch);

                timePrewInput = Time.unscaledTime;

                var cheat = cheats.FirstOrDefault(x => inputed.ToString().Contains(x.Name));
                if (cheat != null)
                {
                    currentCheat = cheat;
                    StartCoroutine(currentCheat.Do());
                    inputed.Clear();

                    if (coroutine != null) StopCoroutine(coroutine);
                    coroutine = StartCoroutine(Late());
                }
            }
            if (Time.unscaledTime - timePrewInput > timeClearInput) inputed.Clear();
        }

        private IEnumerator Late()
        {
            needShowText = true;

            yield return timer;

            needShowText = false;

            coroutine = null;
        }
        private void InitStyles()
        {
            currentStyle = new GUIStyle();
            currentStyle.normal.background = MakeTex(2, 2, Color.white);
            currentStyle.fontSize = 54;
            currentStyle.alignment = TextAnchor.MiddleCenter;
        }
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();

            return result;
        }
        private void OnGUI()
        {
            // инитил в разных местах, но работает корректно только так
            InitStyles();

            if (!needShowText)
                return;

            var rect = new Rect(0, Screen.height - 150, 1300, 100);
            var text = $"Cheat {currentCheat.GetType().Name} is applied!";
            GUI.Box(rect, text, currentStyle);
        }
    }
}