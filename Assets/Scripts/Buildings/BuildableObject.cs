using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableObject : MonoBehaviour
{
    [SerializeField] private BuildingTool _builder;
    [SerializeField] private GameObject _valid;
    [SerializeField] private GameObject _invalid;

    [SerializeField]  private bool _isGround = false;
    [SerializeField]  private bool _isCollidingWithObstacle = false;

    private void Update()
    {
        if (_isGround && !_isCollidingWithObstacle)
        {
            setToValid();
        }
        else
        {
            setToInvalid();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        
        if (other.gameObject.layer == 10)
        {
            _isGround = true;
        }
        else
        {
            _isCollidingWithObstacle = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            _isGround = false;
        } 
        else
        {
            _isCollidingWithObstacle = false;
        }
    }

    private void setToValid()
    {
        _invalid.SetActive(false);
        _valid.SetActive(true);
        if (_builder != null) _builder.readyToBuild = true;
    }

    private void setToInvalid()
    {
        _valid.SetActive(false);
        _invalid.SetActive(true);
        if (_builder != null) _builder.readyToBuild = false;
    }
}
