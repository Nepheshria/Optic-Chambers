

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserObject
{
    // Object variables
    private List<Vector2> stepList = new List<Vector2>();
    
    // Laser Jumeling
    private LaserObject jumeledLaserObject = null;
    private Vector2 symetryAxis;
    private Vector2 symetryAxisPoint;
    
    // Line Look
    private LineRenderer lineRenderer;
    private Color laserColor = Color.red;

    public LaserObject(LineRenderer lineRenderer, Material laserMaterial)
    {
        this.lineRenderer = lineRenderer;
        lineRenderer.material = laserMaterial;
        addStep(lineRenderer.transform.position);
    }

    public void setColor(Color color)
    {
        laserColor = color;
    }
    
    public void addStep(Vector2 stepPosition)
    {
        stepList.Add(stepPosition);
    }

    public void setWeekTwin(Vector2 symetryAxis, Vector2 symetryAxisPoint, LineRenderer twinLineRenderer)
    {
        jumeledLaserObject = new LaserObject(twinLineRenderer, lineRenderer.material);
        this.symetryAxis = symetryAxis;
        this.symetryAxisPoint = symetryAxisPoint;
        
        // Set point for Twin
        Vector2 normalSymetryAxis = this.symetryAxis.Perpendicular1();
        
        foreach (Vector2 point in stepList)
        {
            Vector2 symetricPoint;

            symetricPoint = point + 2 * normalSymetryAxis;
            
            jumeledLaserObject.addStep(symetricPoint);
            Debug.DrawLine(point, symetricPoint, Color.red);
        }
        
    }
    

    public void Draw(bool isTwin = false)
    {
        DefineColor(isTwin);
        
        lineRenderer.positionCount = stepList.Count;
        
        int iterator = 0;
        foreach (Vector2 point in stepList)
        {
            //Debug.Log(point);
            lineRenderer.SetPosition(iterator, point);
            iterator++;
        }

        if (jumeledLaserObject != null)
        {
            jumeledLaserObject.Draw(true);
        }
    }

    void DefineColor(bool isTwin)
    {
        if (isTwin)
        {
            laserColor *= 1.5f;
        }
        
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(laserColor, 0.0f), new GradientColorKey(laserColor, 1.0f) },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(alpha, 0.0f),
                new GradientAlphaKey(0.8f, 0.8f),
                new GradientAlphaKey(0f, 1.0f)
            }
        );
        lineRenderer.colorGradient = gradient;
        
    }
}
