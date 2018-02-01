using UnityEngine;
using System;

public class CameraController : MonoBehaviour {

    public float cameraMaxSpeed;
    public float cameraDistDeaccelerate;

    private GameObject player = null;

    void Awake() {
        this.player = GameObject.FindGameObjectWithTag("Player");

        if(!player){
            Debug.Log("Player not found");
            throw new Exception("Player not found");
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
