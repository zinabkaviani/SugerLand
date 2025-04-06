using UnityEngine;
using UnityEngine.UIElements;

public class UIBarHandler : MonoBehaviour
{
    private VisualElement m_Healthbar;
    public static UIBarHandler instance { get; private set; }
    public float displayTime = 4.0f;

    private float m_TimerDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances
        }
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("UIBarHandler: UIDocument component is missing!");
            return;
        }

        m_Healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");

        if (m_Healthbar == null)
        {
            Debug.LogError("UIBarHandler: Could not find 'HealthBar' in UIDocument.");
            return;
        }

        SetHealthValue(1.0f); // Initialize the health bar to 100% when the game starts
    }

    // Method to update the health value based on percentage
    public void SetHealthValue(float percentage)
    {
        // Ensure the percentage is clamped between 0 and 1
        percentage = Mathf.Clamp01(percentage);

        // Update the health bar width relative to the background width (percentage from 0 to 1)
        m_Healthbar.style.width = new Length(percentage * 100, LengthUnit.Percent);
    }
}