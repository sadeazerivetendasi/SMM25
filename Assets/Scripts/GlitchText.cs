using UnityEngine;
using TMPro;
using System.Collections;

public class GlitchText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textComponent;
    
    [Header("Glitch Settings")]
    [SerializeField] private float glitchInterval = 0.1f;    // Time between glitches
    [SerializeField] private float glitchDuration = 0.05f;   // How long each glitch lasts
    [SerializeField] private float glitchChance = 0.3f;      // Chance of glitch occurring
    [SerializeField] private bool useColorGlitch = true;     // Enable color distortion
    [SerializeField] private bool useCharacterGlitch = true; // Enable character replacement
    [SerializeField] private bool useOffsetGlitch = true;    // Enable position offset
    
    [Header("Color Glitch")]
    [SerializeField] private Color[] glitchColors = new Color[] {
        new Color(1f, 0f, 0f, 1f),    // Red
        new Color(0f, 1f, 1f, 1f),    // Cyan
        new Color(1f, 0f, 1f, 1f)     // Magenta
    };
    
    private string originalText;
    private Color originalColor;
    private Vector3 originalPosition;
    private string glitchCharacters = "!@#$%^&*()-=+[]\\;',./{}|:\"<>?`~1234567890";
    
    void Start()
    {
        // If no text component assigned, try to get one from this gameObject
        if (textComponent == null)
            textComponent = GetComponent<TextMeshProUGUI>();
        
        if (textComponent == null) {
            Debug.LogError("No TextMeshProUGUI component found!");
            enabled = false;
            return;
        }
        
        // Store original values
        originalText = textComponent.text;
        originalColor = textComponent.color;
        originalPosition = textComponent.rectTransform.anchoredPosition;
        
        // Start glitch coroutine
        StartCoroutine(GlitchRoutine());
    }
    
    IEnumerator GlitchRoutine()
    {
        while (true)
        {
            // Wait for next glitch attempt
            yield return new WaitForSeconds(glitchInterval);
            
            // Only glitch sometimes based on chance
            if (Random.value < glitchChance)
            {
                StartCoroutine(PerformGlitch());
            }
        }
    }
    
    IEnumerator PerformGlitch()
    {
        // Decide which glitch effects to apply this time
        bool doColorGlitch = useColorGlitch && Random.value > 0.5f;
        bool doCharGlitch = useCharacterGlitch && Random.value > 0.5f;
        bool doOffsetGlitch = useOffsetGlitch && Random.value > 0.5f;
        
        // Apply color glitch
        if (doColorGlitch)
        {
            textComponent.color = glitchColors[Random.Range(0, glitchColors.Length)];
        }
        
        // Apply character replacement glitch
        string glitchedText = originalText;
        if (doCharGlitch)
        {
            char[] charArray = originalText.ToCharArray();
            int glitchCount = Mathf.Max(1, charArray.Length / 10); // Glitch about 10% of characters
            
            for (int i = 0; i < glitchCount; i++)
            {
                int charIndex = Random.Range(0, charArray.Length);
                charArray[charIndex] = glitchCharacters[Random.Range(0, glitchCharacters.Length)];
            }
            
            glitchedText = new string(charArray);
            textComponent.text = glitchedText;
        }
        
        // Apply position offset glitch
        if (doOffsetGlitch)
        {
            float xOffset = Random.Range(-10f, 10f);
            float yOffset = Random.Range(-5f, 5f);
            textComponent.rectTransform.anchoredPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
        }
        
        // Hold the glitch effect
        yield return new WaitForSeconds(glitchDuration);
        
        // Restore original values
        textComponent.color = originalColor;
        textComponent.text = originalText;
        textComponent.rectTransform.anchoredPosition = originalPosition;
    }
    
    // Optional: Add this method to manually trigger a glitch
    public void TriggerGlitch()
    {
        StartCoroutine(PerformGlitch());
    }
    
    // Optional: Add this for story events that require extended glitching
    public IEnumerator ExtendedGlitchSequence(float duration)
    {
        float endTime = Time.time + duration;
        float intensity = 1.0f;
        
        // Store original glitch chance to restore later
        float originalGlitchChance = glitchChance;
        float originalInterval = glitchInterval;
        
        // Increase glitch chance and frequency
        glitchChance = 0.8f;
        glitchInterval = 0.05f;
        
        while (Time.time < endTime)
        {
            // Randomly decide to do an extreme glitch
            if (Random.value < 0.2f)
            {
                // More extreme character replacement
                char[] extremeGlitch = originalText.ToCharArray();
                for (int i = 0; i < extremeGlitch.Length; i++)
                {
                    if (Random.value < 0.4f)
                        extremeGlitch[i] = glitchCharacters[Random.Range(0, glitchCharacters.Length)];
                }
                textComponent.text = new string(extremeGlitch);
                
                // More extreme offset
                float xOffset = Random.Range(-20f, 20f);
                float yOffset = Random.Range(-10f, 10f);
                textComponent.rectTransform.anchoredPosition = originalPosition + new Vector3(xOffset, yOffset, 0);
                
                // Random color changes
                textComponent.color = new Color(Random.value, Random.value, Random.value);
                
                yield return new WaitForSeconds(0.1f);
                
                // Reset temporarily
                textComponent.text = originalText;
                textComponent.rectTransform.anchoredPosition = originalPosition;
                textComponent.color = originalColor;
            }
            
            yield return new WaitForSeconds(0.05f);
        }
        
        // Restore original parameters
        glitchChance = originalGlitchChance;
        glitchInterval = originalInterval;
        
        // Make sure we're restored to normal state
        textComponent.text = originalText;
        textComponent.rectTransform.anchoredPosition = originalPosition;
        textComponent.color = originalColor;
    }
}