using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class LineDrawer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private List<GameObject> intersectedCircles = new List<GameObject>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRenderer.positionCount = 0;
            intersectedCircles.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            CheckIntersections();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, mousePosition);
        }
    }

    private void CheckIntersections()
    {
        RaycastHit2D[] hits = Physics2D.LinecastAll(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1));

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Circle"))
            {
                GameObject circle = hit.collider.gameObject;
                if (!intersectedCircles.Contains(circle))
                {
                    circle.transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => Destroy(circle));
                    intersectedCircles.Add(circle);
                }
            }
        }
    }
}
