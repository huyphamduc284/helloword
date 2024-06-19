using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyPressDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text keyDisplayText;

    void Start()
    {
        keyDisplayText.text = "...";
    }
    void Update()
    {
        if (Input.anyKey)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    keyDisplayText.text = keyCode.ToString();
                    keyDisplayText.color = GenerateVisibleColor();
                }
            }
        }
        // else
        // {
        //     keyDisplayText.text = "...";
        // }
    }
    private Color GenerateVisibleColor()
    {
        float h, s, v;
        Color.RGBToHSV(keyDisplayText.color, out h, out s, out v);

        if (v < 0.5f)
        {
            return new Color(Random.Range(0.5f, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1));
        }
        else
        {
            return new Color(Random.Range(0, 0.5f), Random.Range(0, 0.5f), Random.Range(0, 0.5f));
        }
    }
}