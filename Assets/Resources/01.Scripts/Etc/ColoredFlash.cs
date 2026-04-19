using System.Collections;
using UnityEngine;

public class ColoredFlash : MonoBehaviour
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration = 0.1f;

    private Renderer[] renderers;
    private Material[] originalMaterials;
    private Material flashMatInstance;
    private Coroutine flashRoutine;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalMaterials = new Material[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
            originalMaterials[i] = renderers[i].material;

        flashMatInstance = new Material(flashMaterial);
    }

    public void Flash(Color color)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);
        flashRoutine = StartCoroutine(FlashRoutine(color));
    }

    private IEnumerator FlashRoutine(Color color)
    {
        flashMatInstance.color = color;
        foreach (var r in renderers)
            r.material = flashMatInstance;

        yield return new WaitForSeconds(duration);

        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material = originalMaterials[i];

        flashRoutine = null;
    }
}
