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
    public float rotationStep = 10f;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerMovement();
        Rotation();
        NewColor();
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
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(-1f * speed * Time.deltaTime, 0, 0);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(0, -1f * speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(1f * speed * Time.deltaTime, 0, 0);
                }
                break;

            case ControlScheme.Arrows:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    transform.Translate(0, 1f * speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.Translate(-1f * speed * Time.deltaTime, 0, 0);
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    transform.Translate(0, -1f * speed * Time.deltaTime, 0);
                }
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    transform.Translate(1f * speed * Time.deltaTime, 0, 0);
                }
                break;

            default:
                Debug.LogWarning("Esquema de control no asignado. No hay movimiento.");
                break;
        }
    }

    void Rotation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(0, 0, rotationStep);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, 0, -rotationStep);
        }
    }

    void NewColor()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Color color = Color.white;
            color.r = Random.Range(0, 1f);
            color.g = Random.Range(0, 1f);
            color.b = Random.Range(0, 1f);
            sr.color = color;
        }
    }
}