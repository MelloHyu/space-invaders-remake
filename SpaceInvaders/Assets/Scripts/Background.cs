using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float loopSpeed;

    [SerializeField] private Renderer bgRenderer;

    private void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(0f, loopSpeed * Time.deltaTime);
    }
}
