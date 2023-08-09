using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public CircleSpawner circleSpawner;
    public LineDrawer lineDrawer;

    public void OnRestartButtonClicked()
    {
        circleSpawner.SpawnCircles();
        lineDrawer.ClearIntersectedCircles();
    }
}
