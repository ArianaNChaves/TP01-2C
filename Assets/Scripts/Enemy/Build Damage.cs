using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private LayerMask buildLayer;
    private PlayerHealth _playerHealthScript;

    private float _timer;

    private void Start()
    {
        _playerHealthScript = GetComponent<PlayerHealth>();
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other) //todo Agregar un temblor de camara cuando el dron es daniado
    {
        if (!Utilities.CompareLayerAndMask(buildLayer, other.gameObject.layer)) return;
        if (!(_timer >= 0.5f)) return;
        _timer = 0;
        _playerHealthScript.TakeDamage(damage);

    }
}
