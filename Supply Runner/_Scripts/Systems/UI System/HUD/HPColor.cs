using UnityEngine;
using UnityEngine.UI;

public class HPColor : MonoBehaviour
{
    [SerializeField]
    private Image sprite;

    [SerializeField]
    private HPBarAmount health;

    [Header("Parameters")]

    [SerializeField]
    private float min;

    [SerializeField]
    private float max;

    [SerializeField]
    private float colorSpeed;

    [SerializeField, Range(0, 5)]
    private float lowHealthBoost;

    private Color myColor;
    private int direction = -1;
    public float contextFactor = 0;

    void Start()
    {
        myColor = new Color(sprite.color.r, sprite.color.g, sprite.color.b);
    }

    void FixedUpdate()
    {   
        contextFactor = lowHealthBoost - (health.health * lowHealthBoost) + 1;

        myColor.g += colorSpeed * contextFactor * direction / 255;
        myColor.b += colorSpeed * contextFactor *  direction / 255;

        if (myColor.g*255 < min || myColor.g*255 > max) 
        {
            direction *= -1;
        }

        sprite.color = myColor;
    }
}
