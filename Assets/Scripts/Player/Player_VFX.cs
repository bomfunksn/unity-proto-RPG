using System.Collections;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    [Header("Mirror image VFX")]
    [Range(0.01f, 0.2f)]
    [SerializeField] private float mirrorImageInterval = .05f;
    [SerializeField] private GameObject mirrorImagePrefab;
    private Coroutine mirrorImageCo;

    public void DoMirrorImageEffect(float duration)
    {
        if (mirrorImageCo != null)
            StopCoroutine(mirrorImageCo);
        mirrorImageCo = StartCoroutine(MirrorImageEffectCo(duration));
    }

    private IEnumerator MirrorImageEffectCo(float duration)
    {
        float timeTracker = 0;

        while (timeTracker < duration)
        {
            CreateMirrorImage();

            yield return new WaitForSeconds(mirrorImageInterval);
            timeTracker = timeTracker + mirrorImageInterval;
        }
    }
    private void CreateMirrorImage()
    {
        GameObject mirrorImage = Instantiate(mirrorImagePrefab, transform.position, transform.rotation);
        mirrorImage.GetComponentInChildren<SpriteRenderer>().sprite = sr.sprite;
    }
}
