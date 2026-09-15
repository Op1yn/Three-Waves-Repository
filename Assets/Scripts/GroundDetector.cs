using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _groundMask;

    public bool isOnGround()
    {
        bool isOnGround = Physics.CheckSphere(_groundCheckPoint.position, _groundCheckRadius, _groundMask); ;

        Debug.Log(isOnGround);

        return isOnGround;
    }
}