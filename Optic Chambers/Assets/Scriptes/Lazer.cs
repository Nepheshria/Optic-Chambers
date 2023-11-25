using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float RayDistance = 10;
    [SerializeField] private float ResonatorBoostPower = 10;
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    private Transform m_transform;


    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    void ShootLaser(float rayPower, Vector2 laserDirectorVector, Vector2 startPoint)
    {
        RaycastHit2D _hit = Physics2D.Raycast(startPoint, laserDirectorVector, rayPower);
        if (_hit)
        {
                switch (_hit.transform.tag)
                {
                    case "Mirror":
                        Debug.Log("mirror");
                        Vector2 newLaserDirection = ComputeMirrotOutput(laserDirectorVector, _hit);
                        ShootLaser(rayPower-_hit.distance, newLaserDirection, _hit.point);
                        break;
                    case "ResonatorBoost":
                        Debug.Log("resonatorBoost");
                        ShootLaser(rayPower-_hit.distance+ResonatorBoostPower, laserDirectorVector, _hit.point);
                        break;
                }
                Draw2DRay(startPoint, _hit.point);
        }
        else
        {
            Draw2DRay(startPoint, (startPoint + (laserDirectorVector * (rayPower))));
        }
        
    }

    void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        m_lineRenderer.SetPosition(0, startPoint);
        m_lineRenderer.SetPosition(1, endPoint);
    }

    Vector2 ComputeMirrotOutput(Vector2 inputLaser, RaycastHit2D _hit)
    {
        return new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser(RayDistance, laserFirePoint.transform.right, laserFirePoint.position);
    }
}
