using UnityEngine;

public class CollectedCubeFrontCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BarrierCube"))
        {
            transform.parent.parent = null;
            MovementController.movementController.CubeLost();
            AudioController.audioController.PlayCubeLoseSFX();
            Destroy(transform.parent.gameObject, 2f);
        }
        if (other.CompareTag("FinishFloorUpTrigger"))
        {
            MovementController.movementController.TriggerFinishFloor();
            transform.parent.parent = null;
            AudioController.audioController.PlayCubeLoseSFX();
            Destroy(transform.parent.gameObject, 2f);
        }
    }
}
