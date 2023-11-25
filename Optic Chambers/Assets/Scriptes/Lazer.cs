using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float RayDistance = 10;
    [SerializeField] private float ResonatorBoostPower = 10;
    private int stepNumber = 1;
    private List<Vector2> stepList = new List<Vector2>();
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    private Transform m_transform;


    private void Awake()
    {
        m_lineRenderer.SetPosition(0, laserFirePoint.position);
        m_transform = GetComponent<Transform>();
    }

    void ShootLaser(float rayPower, Vector2 laserDirectorVector, Vector2 startPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, laserDirectorVector, rayPower);
        if (hit)
        {
                switch (hit.transform.tag)
                {
                    case "Mirror":
                        Debug.Log("mirror");
                        ComputeMirror(laserDirectorVector, hit, rayPower-hit.distance);
                        break;
                    case "ResonatorBoost":
                        Debug.Log("resonatorBoost");
                        ComputeResonatorBoost(laserDirectorVector, hit, rayPower-hit.distance);
                        break;
                    default:
                        stepNumber++;
                        stepList.Add(hit.point);
                        break;
                }
        }
        else
        {
            stepNumber++;
            stepList.Add((startPoint + (laserDirectorVector * (rayPower))));
        }
        
    }

    

    void ComputeMirror(Vector2 inputLaser, RaycastHit2D hit, float rayPower)
    {
        // Compute new laser angle
        Vector2 laserDirectorVector = Vector2.Reflect(inputLaser, hit.normal);
        
        // Compute new laser starting Position

        Vector2 newStartingPoint = hit.point + hit.normal*0.1f;
        
        // Add step to list
        stepNumber++;
        stepList.Add(newStartingPoint);
        
        // Shoot new laser
        Debug.Log("mirror - "+laserDirectorVector);
        ShootLaser(rayPower, laserDirectorVector, newStartingPoint);
    }

    void ComputeResonatorBoost(Vector2 entry, RaycastHit2D hit, float rayPower)
    {
        // Compute i angle
        Vector2 iVector = ((Vector2)hit.transform.position - entry).normalized;
        Vector2 normalizedEntry = entry.normalized;
        float i = Vector2.Angle(iVector, normalizedEntry);
        
        // Compute new laser angle
        Vector2 laserDirectorVector =
        
        // Compute new laser starting Position
        
        Vector2 newStartingPoint = new Vector2();
        
        // Add step to list
        stepNumber++;
        stepList.Add(newStartingPoint);
        
        // Shoot new laser
        ShootLaser(rayPower+ResonatorBoostPower, laserDirectorVector, newStartingPoint);
    }

    /**
     * n0 origin environment
     * n1 exit environment
     * i origin angle
     */
    float ComputeRefractation(float n0, float n1, float i)
    {
        return Mathf.Asin((n0 / n1) * Mathf.Sin(i));
    }

    // Update is called once per frame
    void Update()
    {
        stepList = new List<Vector2>();
        stepNumber = 1;
        ShootLaser(RayDistance, laserFirePoint.transform.right, laserFirePoint.position);
        Draw2DRay();
    }
    
    // void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    // {
    //     m_lineRenderer.SetPosition(stepNumber, startPoint);
    //     m_lineRenderer.SetPosition(stepNumber+1, endPoint);
    //     stepNumber++;
    // }
    
    void Draw2DRay()
    {
        m_lineRenderer.positionCount = stepList.Count+1;
        int iterator = 1;
        foreach (Vector2 point in stepList)
        {
            m_lineRenderer.SetPosition(iterator, point);
            iterator++;
        }
    }
}
