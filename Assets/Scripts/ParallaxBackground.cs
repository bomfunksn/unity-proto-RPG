using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main; //Что такое .main??? это типа название главной камеры движка, которой мы управляем синемашином?
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect; //Как из этого получилась половина ширины? Что такое "рахмер камеры" в ортографик сайзе?
        CalculateImageLenghth();
    }

    private void Update()
    {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackround(cameraLeftEdge, cameraRightEdge);//зачем тут писать в скобках при вызове метода?
        }
    }
    private void CalculateImageLenghth()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
            layer.CalculateImageWidth();
    }
}
