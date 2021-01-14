using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectile : MonoBehaviour
{
    public MeshRenderer rend;
    public LineRenderer line;
    public float fadeTime = 2f;
    public Color lineColor = new Color(1f, 1f, 1f, 1f);
    public float origWidth = 0.02f;

    private bool destroyOnFadeEnd = false;


    // Update is called once per frame
    void Update()
    {
        if (!line || !line.enabled)
            return;

        Color currentColor = line.startColor;

        currentColor.a -= Time.deltaTime / fadeTime;

        SetColor(currentColor);
        SetWidth(origWidth * currentColor.a);

        if (currentColor.a <= 0.01f)
        {
            line.enabled = false;
            if (destroyOnFadeEnd)
                Destroy(gameObject);
        }
    }

    public void DestroyOnFadeEnd()
    {
        destroyOnFadeEnd = true;
        rend.enabled = false;
    }

    public void StartFadeLine(Vector3 fromPos)
    {
        line.enabled = true;
        SetColor(lineColor);

        line.SetPosition(0, fromPos);
        line.SetPosition(1, transform.position);
    }

    private void SetColor(Color color)
    {
        line.startColor = color;
        line.endColor = color;
        line.material.SetColor("_Color", color);
    }

    private void SetWidth(float width)
    {
        line.startWidth = width;
        line.endWidth = width;
    }
}
