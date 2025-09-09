using UnityEngine;
[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float ParallaxMultiplier;

    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * ParallaxMultiplier);
    }

}
