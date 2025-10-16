using Unity.VisualScripting;
using UnityEngine;

public class SkillObject_Chronosphere : SkillObject_Base
{
    private Skill_Chronosphere chronosphereManager;

    private float expandSpeed = 2;
    private float duration;

    private float slowDownPercent = .9f;

    private Vector3 targetScale;
    private bool isShrinking;


    public void SetupChronosphere(Skill_Chronosphere chronosphereManager)
    {
        this.chronosphereManager = chronosphereManager;

        duration = chronosphereManager.GetChronosphereDuration();
        slowDownPercent = chronosphereManager.GetSlowPercrntage();
        expandSpeed = chronosphereManager.expandSpeed;
        float maxSize = chronosphereManager.maxChronosphereSize;

        targetScale = Vector3.one * maxSize;
        Invoke(nameof(ShrinkChronosphere), duration);
    }

    private void Update()
    {
        HandleScaling();
    }
    
    private void HandleScaling()
    {
        float sizeDifference = Mathf.Abs(transform.localScale.x - targetScale.x);
        bool shouldChangeScale = sizeDifference > .1f;

        if (shouldChangeScale)
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, expandSpeed * Time.deltaTime);

        if (isShrinking && sizeDifference < .1f)
            Destroy(gameObject);
    }

    private void ShrinkChronosphere()
    {
        targetScale = Vector3.zero;
        isShrinking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;

        enemy.SlowDownEntity(duration, slowDownPercent, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
                Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy == null)
            return;


        enemy.StopSlowDown();
    }
}
