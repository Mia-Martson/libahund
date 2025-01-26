using UnityEngine;

public class LaserIndicatorAnimationScript : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        animator.SetTrigger("PlayAnimation");
    }
}