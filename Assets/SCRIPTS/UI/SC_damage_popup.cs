using TMPro;
using UnityEngine;

public class SC_damage_popup : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float scaleUpDuration = 0.2f;
    [SerializeField] private float fadeOutDuration = 0.3f;
    [SerializeField] private Vector3 moveDirection = new Vector3(0, 1, 0);
    [SerializeField] private TMP_Text textMesh;

    private Color originalColor;
    private float timeElapsed = 0f;
    private Vector3 originalScale;

    void Awake()
    {
        originalColor = textMesh.color;
        originalScale = transform.localScale;
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        // Monter
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Grossir puis revenir à la taille normale
        if (timeElapsed < scaleUpDuration)
        {
            float t = timeElapsed / scaleUpDuration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * 1.3f, t);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 4f);
        }

        // Faire disparaître
        if (timeElapsed > duration - fadeOutDuration)
        {
            float fadeT = (timeElapsed - (duration - fadeOutDuration)) / fadeOutDuration;
            Color c = textMesh.color;
            c.a = Mathf.Lerp(originalColor.a, 0, fadeT);
            textMesh.color = c;
        }

        // Détruire après la durée totale
        if (timeElapsed >= duration)
        {
            Destroy(gameObject);
        }
    }
}