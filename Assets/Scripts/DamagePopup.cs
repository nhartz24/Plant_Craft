using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    [SerializeField] private Transform pfDamagePopup;
    private float disappearTimer;
    private Color textColor;


    public void Create(Vector3 position, float damageAmount) {
        Transform damagePopupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount.ToString());
    }

    public void CreateWithPlus(Vector3 position, float amount, Color newColor) {
        Transform popupTransform = Instantiate(pfDamagePopup, position, Quaternion.identity);
        DamagePopup popup = popupTransform.GetComponent<DamagePopup>();
        popup.Setup("+"+amount.ToString(), newColor);
    }

    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string damageAmount) {
        textMesh.SetText(damageAmount);
        textColor = textMesh.color;
        disappearTimer = 0f;
    }

    public void Setup(string damageAmount, Color newColor) {
        textMesh.SetText(damageAmount);
        textMesh.color = newColor;
        textColor = textMesh.color;
        disappearTimer = 0f;
    }

    private void Update() {
        float moveYSpeed = 10f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime; 

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) {
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0) {
                Destroy(gameObject);
            }

        }
    }


}
