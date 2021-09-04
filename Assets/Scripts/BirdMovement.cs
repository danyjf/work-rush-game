using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour {
    public float speed;

    private Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() {
        if(Vector2.Distance(player.position, transform.position) < 2.75f)
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if(transform.position.x < player.transform.position.x && Vector2.Distance(player.position, transform.position) > 2.0f)
            Destroy(this.gameObject);
    }
}
