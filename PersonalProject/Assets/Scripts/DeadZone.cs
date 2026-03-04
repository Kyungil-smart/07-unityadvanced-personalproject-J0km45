using System;
using UnityEngine;

public class DeadZone : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _respawnPoint;

    public void OnInteract(PlayerController player)
    {
        player.transform.position = _respawnPoint.position;
    }
}
