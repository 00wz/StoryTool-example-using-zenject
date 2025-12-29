using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] 
    private Transform barRoot;
    private float _defaultSize;

    private void Awake()
    {
        _defaultSize = barRoot.localScale.x;
    }

    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);
        Vector3 localScale = barRoot.localScale;
        localScale.x = progress * _defaultSize;
        barRoot.localScale = localScale;
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
    }
}
