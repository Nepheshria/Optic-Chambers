using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float RayDistance = 10;
    [SerializeField] private float ResonatorBoostPower = 10;
    private LayerMask laserLayer;
    private int NumberOfLaser = 0;

    private List<LaserObject> Lasers;
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    private Material laserMaterial;
    private Transform m_transform;
    private List<GameObject> lineRendererToDestroy;

    private void Awake()
    {
        m_lineRenderer.SetPosition(0, laserFirePoint.position);
        laserMaterial = m_lineRenderer.material;
        m_transform = GetComponent<Transform>();
        laserLayer |= (1 << LayerMask.NameToLayer("Default"));
        lineRendererToDestroy = new List<GameObject>();
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
                    case "SplitEntrace":
                        //Debug.Log("Split");
                        ComputeSplit(laserNumber, laserDirectorVector, hit, rayPower-hit.distance);
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
        // Recover Exit point
        Vector2 exitPoint1 = hit.transform.GetChild(0).transform.position;
        // print(hit.transform.GetChild(0).transform.position);
        Vector2 exitPoint2 = hit.transform.GetChild(1).transform.position;
        // print(hit.transform.GetChild(1).transform.position);
        
        
        // Craft exit Director Vector
        Vector2 directorVectorExit1 = (Vector2.up + Vector2.right).normalized;
        Vector2 directorVectorExit2 = (Vector2.down + Vector2.right).normalized;

        // Laser Exit 1
        GameObject childLineRenderer = new GameObject();
        lineRendererToDestroy.Add(childLineRenderer);
        childLineRenderer.AddComponent<LineRenderer>();
        childLineRenderer.transform.parent = hit.transform.GetChild(0).transform;
        childLineRenderer.transform.position = childLineRenderer.transform.parent.position;
        copyLineRendererSetting(childLineRenderer.GetComponent<LineRenderer>(), m_lineRenderer);
        Lasers.Add(new LaserObject(childLineRenderer.GetComponent<LineRenderer>(),laserMaterial));
        ShootLaser(NumberOfLaser, rayPower/2, directorVectorExit1, exitPoint1);
        NumberOfLaser++;

        // Laser Exit 2
        GameObject childLineRenderer2 = new GameObject();
        lineRendererToDestroy.Add(childLineRenderer2);
        childLineRenderer2.AddComponent<LineRenderer>();
        childLineRenderer2.transform.parent = hit.transform.GetChild(1).transform;
        childLineRenderer2.transform.position = childLineRenderer2.transform.parent.position;
        copyLineRendererSetting(childLineRenderer2.GetComponent<LineRenderer>(), m_lineRenderer);
        Lasers.Add(new LaserObject(childLineRenderer2.GetComponent<LineRenderer>(), laserMaterial));
        ShootLaser(NumberOfLaser, rayPower/2, directorVectorExit2, exitPoint2);
        NumberOfLaser++;

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
        // Recover Exit point
        Vector2 exitPointStrong = hit.transform.GetChild(0).transform.position;
        // print(hit.transform.GetChild(0).transform.position);
        // Debug.Log(hit.transform.GetChild(0).name);
        Vector2 exitPointWeak = hit.transform.GetChild(1).transform.position;
        // Debug.Log(hit.transform.GetChild(1).name);
        
        // Craft exit Director Vector
        Vector2 directorVector = Vector2.right;
        
        // Animation
        Transform Sprite = hit.transform.GetChild(2);
        Animator boostAnimator = Sprite.GetComponent<Animator>();
        boostAnimator.SetBool("Activated", true);
        Sprite.GetComponent<AnimationTriggerManagment>().Hit();
        
        // Laser Strong
        GameObject childLineRenderer = new GameObject();
        lineRendererToDestroy.Add(childLineRenderer);
        childLineRenderer.AddComponent<LineRenderer>();
        childLineRenderer.transform.parent = hit.transform.GetChild(0).transform;
        childLineRenderer.transform.position = childLineRenderer.transform.parent.position;
        copyLineRendererSetting(childLineRenderer.GetComponent<LineRenderer>(), m_lineRenderer);
        Lasers.Add(new LaserObject(childLineRenderer.GetComponent<LineRenderer>(),laserMaterial));
        ShootLaser(NumberOfLaser, rayPower, directorVector, exitPointStrong);
        NumberOfLaser++;
        
        //Laser Weak
        GameObject childLineRendererWeak = new GameObject();
        lineRendererToDestroy.Add(childLineRendererWeak);
        childLineRendererWeak.AddComponent<LineRenderer>();
        childLineRendererWeak.transform.parent = hit.transform.GetChild(1).transform;
        childLineRendererWeak.transform.position = childLineRendererWeak.transform.parent.position;
        copyLineRendererSetting(childLineRendererWeak.GetComponent<LineRenderer>(), m_lineRenderer);
        
        // Shot Laser for test need to be replaced with twin system
        Lasers.Add(new LaserObject(childLineRendererWeak.GetComponent<LineRenderer>(),laserMaterial));
        ShootLaser(NumberOfLaser, rayPower, directorVector, exitPointWeak);
        Lasers[NumberOfLaser].setColor(Color.red * 1.5f);
        NumberOfLaser++;
        
        // // Calculate symmetry axis & point
        // Vector2 strongWeakVector2 = exitPointWeak - exitPointStrong;
        // Vector2 symmetryAxisPoint = exitPointStrong + strongWeakVector2 / 2;
        //
        // // Set Twin
        // Lasers[NumberOfLaser-1].setWeekTwin(strongWeakVector2.Perpendicular1(), symmetryAxisPoint, childLineRendererWeak.GetComponent<LineRenderer>());

    }

    void copyLineRendererSetting(LineRenderer lr1, LineRenderer lr2)
    {
        lr1.widthCurve = lr2.widthCurve;
        lr1.startWidth = lr2.startWidth;
        lr1.endWidth = lr2.endWidth;
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
        // Debug.Log("=======================");
        Lasers = new List<LaserObject>();
        destroyLineRenderer();
        Lasers.Add(new LaserObject(m_lineRenderer, laserMaterial));
        Lasers[0].addStep(laserFirePoint.position);
        NumberOfLaser = 1;
        ShootLaser(0, RayDistance, laserFirePoint.transform.right, laserFirePoint.position);
        //Debug.Log("Laser Number " + NumberOfLaser);
        //Debug.Log("Laser In List " + Lasers.Count);
        Draw2DRay();

    }
    
    void Draw2DRay()
    {
        int i = 0;
        foreach (LaserObject laser in Lasers)
        {
            // Debug.Log("Laser " + i);
            laser.Draw();
            i++;
        }
    }

    void destroyLineRenderer()
    {
        if (lineRendererToDestroy.Count!=0)
        {
            foreach (GameObject lineRend in lineRendererToDestroy)
            {
                Destroy(lineRend);
            }
        }
        
    }
}
