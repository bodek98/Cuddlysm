using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _temporaryCamera;
    [SerializeField] private List<LayerMask> _playerLayers;

    private List<PlayerInput> _players = new List<PlayerInput>();
    private PlayerInputManager _playerInputManager;

    private void Awake()
    {
        _playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        _playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        _playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        _players.Add(player);

        Transform playerParent = player.transform.parent;
        // Todo: spawn position

        int layerToAdd = _playerLayers[_players.Count - 1].value;
        int layerIndexToAdd = (int)Mathf.Log(layerToAdd, 2); 

        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerIndexToAdd;
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerIndexToAdd;

        // Todo: cinemachine input handler

        _temporaryCamera.SetActive(false);
    }
}
