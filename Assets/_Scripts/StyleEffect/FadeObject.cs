using System.Collections;
using TMPro;
using UnityEngine;

public class FadeObject : MonoBehaviour
{
    public Renderer[] Renderers; // Store all child renderers here
    public float FadeDuration = 2.0f; // Duration of the fade

    private Material[] _materials; // Store the materials of the renderers
    private Color[] _originalColors; // Store the original colors of the materials

    private bool _isFading = false;

    private void Start()
    {
        // Get all child renderers and their materials
        Renderers = GetComponentsInChildren<Renderer>();
        _materials = new Material[Renderers.Length];
        _originalColors = new Color[Renderers.Length];

        for (int i = 0; i < Renderers.Length; i++)
        {
            _materials[i] = Renderers[i].material;
            _originalColors[i] = GetMaterialColor(_materials[i]);
        }
    }

    public void StartFade()
    {
        if (_isFading) return; // Don't start another fade while already fading

        StartCoroutine(FadeOut());
    }

    public void ResetFade()
    {
        if (_isFading) return; // Don't reset while fading

        for (int i = 0; i < Renderers.Length; i++)
        {
            SetMaterialColor(_materials[i], _originalColors[i]);
        }
    }

    private IEnumerator FadeOut()
    {
        _isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < FadeDuration)
        {
            for (int i = 0; i < Renderers.Length; i++)
            {
                Color newColor = Color.Lerp(_originalColors[i], new Color(_originalColors[i].r, _originalColors[i].g, _originalColors[i].b, 0), elapsedTime / FadeDuration);
                SetMaterialColor(_materials[i], newColor);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _isFading = false;
    }

    private Color GetMaterialColor(Material material)
    {
        if (material.HasProperty("_FaceColor"))
        {
            return material.GetColor("_FaceColor");
        }
        else if (material.HasProperty("_Color"))
        {
            return material.GetColor("_Color");
        }
        else
        {
            // Debug.LogWarning("Material does not have _FaceColor or _Color property for fading.");
            return Color.white;
        }
    }

    private void SetMaterialColor(Material material, Color color)
    {
        if (material.HasProperty("_FaceColor"))
        {
            material.SetColor("_FaceColor", color);
        }
        else if (material.HasProperty("_Color"))
        {
            material.SetColor("_Color", color);
        }
        else
        {
            // Debug.LogWarning("Material does not have _FaceColor or _Color property for fading.");
        }
    }
}
