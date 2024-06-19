using TMPro;
using UnityEngine;

public class MenuButtonHover : MonoBehaviour {
    public Color baseColor;
    public Color hoverColor;

    private TMP_Text textBox;
    private Canvas canvas;
    private Camera uiCamera;
    private RectTransform textBoxRectTransform;

    private void Awake() {
        textBox = GetComponent<TMP_Text>();
        canvas = GetComponentInParent<Canvas>();
        textBoxRectTransform = textBox.GetComponent<RectTransform>();

        uiCamera = canvas.worldCamera;
    }

    private void Update() {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);

        bool isIntersectingRectTransform = TMP_TextUtilities.IsIntersectingRectTransform(textBoxRectTransform, mousePosition, uiCamera);

        if (isIntersectingRectTransform) {
            textBox.color = hoverColor;
        } else {
            textBox.color = baseColor;
        }
    }
}