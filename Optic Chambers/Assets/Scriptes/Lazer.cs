using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private float RayDistance = 100;
    public LineRenderer m_lineRenderer;
    public Transform laserFirePoint;
    private Transform m_transform;


    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, m_transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * RayDistance);
        }
    }

    void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        m_lineRenderer.SetPosition(0, startPoint);
        m_lineRenderer.SetPosition(1, endPoint);
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }
}
