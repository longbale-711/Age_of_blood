using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonPresent : MonoBehaviour
{
    [Header("UI Component")]
    [SerializeField] private Slider _healthSlider;

    [Header("References")]
    [SerializeField] private Skeleton _skeleton;

    private SkeletonController _skeletonController;


    #region MonoBehavior_Methods
    // Start is called before the first frame update
    void Start()
    {
        _skeletonController = new SkeletonController(this, _skeleton);

        _healthSlider.maxValue = _skeleton.Health;
        _healthSlider.value = _healthSlider.maxValue;
        _skeleton.OnHealthChange += UpdateHealthValue;
    }

    private void OnDestroy()
    {
        _skeleton.OnHealthChange -= UpdateHealthValue;
    }
    #endregion

    private void UpdateHealthValue(int health)
    {
        _healthSlider.value = health;
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        _skeletonController.Attack();
    }

    [ContextMenu("Take Damaged")]
    public void TakeDamaged()
    {
        _skeletonController.TakeDamaged(1);
    }
}
