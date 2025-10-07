using UnityEngine;
using UnityEngine.UI;
public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;


    public void DirectConnection(NodeDirectionType direction, float length, float offset)
    {
        bool shouldBeActive = direction != NodeDirectionType.None;
        float finalLength = shouldBeActive ? length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0, 0, angle+offset);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);
    }

    public Image GetConnectionImage() => connectionLength.GetComponent<Image>();

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            rect.parent as RectTransform,
            childNodeConnectionPoint.position,
            null,
            out var localPosition
        );
        return localPosition;
    }


    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch (type) //calculating from right (angle 90 = 0)
        {
            case NodeDirectionType.angle45:
                return 45f;
            case NodeDirectionType.angle90:
                return 90f;
            case NodeDirectionType.angle135:
                return 135f;
            case NodeDirectionType.angle180:
                return 180f;
            case NodeDirectionType.angle225:
                return 225f;
            case NodeDirectionType.angle270:
                return 270f;
            case NodeDirectionType.angle315:
                return 315f;
            case NodeDirectionType.angle0:
                return 0f;
            default:
                return 0f;
        }
    }
}


public enum NodeDirectionType
{
    None,
    angle45,
    angle90,
    angle135,
    angle180,
    angle225,
    angle270,
    angle315,
    angle0
}