using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum ControlScheme { WASD, Arrows }

    [Header("Controles")]
    public ControlScheme controls = ControlScheme.WASD;

    [Header("Movimiento")]
    public float speed = 5f;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        switch (controls)
        {
            case ControlScheme.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(0, 1f * speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(0, -1f * speed * Time.deltaTime, 0);
                }
                break;

            case ControlScheme.Arrows:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(0, 1f * speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(0, -1f * speed * Time.deltaTime, 0);
                }
                break;

            default:
                Debug.LogWarning("Esquema de control no asignado. No hay movimiento.");
                break;
        }
    }
}