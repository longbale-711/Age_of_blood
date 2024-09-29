using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SkeletonAnimationController))]
public class SkeletonPresent : MonoBehaviour
{
    [Header("UI Component")]
    [SerializeField] private Slider _healthSlider;

    [Header("References")]
    [SerializeField] private Skeleton _skeleton;
    [SerializeField] private List<Transform> _waypoints;

    private EnemyManager _enemyManager;
    private SkeletonController _skeletonController;
    private SkeletonAnimationController _skeletonAnimationController;
    private int waypointIndex = 0;

    #region MonoBehavior_Methods
    // Start is called before the first frame update
    void Start()
    {
        // Init references
        _enemyManager = EnemyManager.Instance;
        _skeletonController = new SkeletonController(this, _skeleton);
        _skeletonAnimationController = GetComponent<SkeletonAnimationController>();
        _waypoints = new List<Transform>(_enemyManager.EnemyPathNode);

        // Subscribe event listener
        _skeleton.OnHealthChange += UpdateHealthValue;
        _skeletonController.OnDying += Die;
    }
    private void OnEnable()
    {
        // Set default values
        _healthSlider.maxValue = _skeleton.Health;
        _healthSlider.value = _healthSlider.maxValue;
    }

    private void Update()
    {
        if (_skeletonController.IsDie()) return;
        Move();
    }

    private void OnDestroy()
    {
        // Unsubscribe event listener
        _skeleton.OnHealthChange -= UpdateHealthValue;
        _skeletonController.OnDying -= Die;
    }
    #endregion

    private void UpdateHealthValue(int health)
    {
        _healthSlider.value = health;
    }

    void Move()
    {
        if (waypointIndex >= _waypoints.Count)
        {
            // End of the path 
            Attack();
            // Reset to enemy pool
            _enemyManager.EnemySpawner.DespawnEnemy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[waypointIndex].position, _skeleton.Speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, _waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
        }
    }

    [ContextMenu("Attack")]
    public void Attack()
    {
        _skeletonController.Attack();
    }

    public void TakeDamaged(int dmgTaken)
    {
        _skeletonController.TakeDamaged(dmgTaken);
    }

    private void Die()
    {
        // Return to enemy pool after play die anim completed
        _skeletonAnimationController.PlayDyingAnim(()=> { 
            _enemyManager.EnemySpawner.DespawnEnemy(gameObject);
        });
    }
}
