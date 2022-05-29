using UnityEngine;

public class UiManager : MonoBehaviour
{
    public GameObject canvas;

    private void Awake()
    {
        canvas.SetActive(false);
    }
    public void OnButtenPresed()
    {
        canvas.SetActive (true);
    }
}
