using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllIn1SpriteShader;

public class AnimationUIElement : MonoBehaviour
{
    Material mat;
    float value;
    public float speed;
    public float _shineCD;
    float _nextShine;

    void Awake()
    {
        mat = GetComponent<Image>().material;
        //mat.EnableKeyword("SHINE_ON");
        value = mat.GetFloat("_ShineLocation");
        _nextShine = _shineCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (value < 1f)
            value = Mathf.MoveTowards(value, 1f, Time.deltaTime * speed);
        else
        {
            if (Time.time >= _nextShine)
            {
                value = 0f;
                _nextShine = Time.time + _shineCD;
            }

        }

        mat.SetFloat("_ShineLocation", value);
    }
}
