using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float RayDistance = 10;
    [SerializeField] private float ResonatorBoostPower = 10;
    private LayerMask laserLayer;
    private int stepNumber = 1;
    private int NumberOfLaser = 0;

    private List<LaserObject> Lasers;
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    private Transform m_transform;


    private void Awake()
    {
        m_lineRenderer.SetPosition(0, laserFirePoint.position);
        m_transform = GetComponent<Transform>();
        laserLayer |= (1 << LayerMask.NameToLayer("Default"));
    }

    void ShootLaser(int laserNumber, float rayPower, Vector2 laserDirectorVector, Vector2 startPoint)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, laserDirectorVector, rayPower, laserLayer);
        if (hit)
        {
                switch (hit.transform.tag)
                {
                    case "Mirror":
                        //Debug.Log("mirror");
                        ComputeMirror(laserNumber, laserDirectorVector, hit, rayPower-hit.distance);
                        break;
                    case "ResonatorBoost":
                        //Debug.Log("resonatorBoost");
                        ComputeResonatorBoost(laserNumber, laserDirectorVector, hit, rayPower-hit.distance);
                        break;
                    case "WinTarget":
                        Debug.Log("Win");
                        Lasers[laserNumber].addStep(hit.point);
                        break;
                    case "ResonatorJumeling":
                        //Debug.Log("Jumeling");
                        ComputeResonatorJumelor(laserNumber, laserDirectorVector, hit, rayPower-hit.distance);
                        Lasers[laserNumber].addStep(hit.point);
                        break;
                    default:
                        Lasers[laserNumber].addStep(hit.point);
                        break;
                }
        }
        else
        {
            Lasers[laserNumber].addStep(startPoint + laserDirectorVector * rayPower);
        }
        
    }
    
    void ComputeMirror(int laserNumber, Vector2 inputLaser, RaycastHit2D hit, float rayPower)
    {
        // Compute new laser angle
        Vector2 laserDirectorVector = Vector2.Reflect(inputLaser, hit.normal);
        
        // Compute new laser starting Position

        Vector2 newStartingPoint = hit.point + laserDirectorVector.normalized*0.1f;
        
        // Add step to list
        Lasers[laserNumber].addStep(hit.point);
        
        // Shoot new laser
        //Debug.Log("mirror - "+laserDirectorVector);
        ShootLaser(laserNumber, rayPower, laserDirectorVector, newStartingPoint);
    }
    
    void ComputeSplit(int laserNumber, Vector2 entry, RaycastHit2D hit, float rayPower)
    {
        
        
        
    }

    void ComputeResonatorBoost(int laserNumber, Vector2 entry, RaycastHit2D hit, float rayPower)
    {
        Vector2 newStartingPoint = hit.point + entry.normalized;
        
        // Add step to list
        Lasers[laserNumber].addStep(hit.point);
        
        // Animation
        Animator boostAnimator = hit.transform.GetComponent<Animator>();
        boostAnimator.SetBool("Activated", true);
        hit.transform.GetComponent<AnimationTriggerManagment>().Hit();
        
        // Shoot new laser
        ShootLaser(laserNumber, rayPower+ResonatorBoostPower, entry, newStartingPoint);
    }

    void ComputeResonatorJumelor(int laserNumber, Vector2 entry, RaycastHit2D hit, float rayPower)
    {
        
        
        
    }
    
    
    
    
    /**
     * n0 origin environment
     * n1 exit environment
     * i origin angle
     */
    float ComputeRefractationAngle(float n0, float n1, float i)
    {
        return Mathf.Asin((n0 / n1) * Mathf.Sin(i));
    }
    
    public static Vector2 rotate(Vector2 v, float delta) {
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }


    // Update is called once per frame
    void Update()
    {
        Lasers = new List<LaserObject>();
        Lasers.Add(new LaserObject(m_lineRenderer));
        Lasers[0].addStep(laserFirePoint.position);
        NumberOfLaser = 1;
        ShootLaser(0, RayDistance, laserFirePoint.transform.right, laserFirePoint.position);
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
        foreach (LaserObject laser in Lasers)
        {
            laser.Draw();
        }
    }
}
