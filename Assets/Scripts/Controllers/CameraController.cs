using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraMaxSpeed;
    public float cameraDistDeaccelerate;

    private GameObject player = null;

    void Awake() {
        PlayerController pc = FindObjectOfType(typeof(PlayerController))
                                                        as PlayerController;

        if (pc) {
            player = pc.gameObject;
        } else {
            Debug.Log("Não tem jogador nessa porra.");
        }

        // TAG: MainCamera
        gameObject.name = "MainCamera";
        gameObject.layer = 8;
    }

    void FixedUpdate() {
        Vector3 move = player.transform.position - transform.position;
        move.z = 0;
        float c = Mathf.Clamp01(move.magnitude / cameraDistDeaccelerate);
        move *= c*(1.15f-c)*cameraMaxSpeed;
        transform.Translate(move);
    }
}
