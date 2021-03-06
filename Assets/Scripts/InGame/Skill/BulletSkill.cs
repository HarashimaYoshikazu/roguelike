using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletSkill : ISkill
{
    public SkillDef SkillId => SkillDef.Bullet;

    float _timer = 0f;
    float _interval = 2f;
    int _bulletValue = 1;

    public void SetUp()
    {
        
    }

    public void LevelUp()
    {
        _bulletValue++;
        _interval -= 0.2f;
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer> _interval)
        {
            var enemyList = GameManager.Instance.EnemyManager.EnemyPool.ObjectList;
            EnemyController[] targets = new EnemyController[_bulletValue];

            float prevMinDis = -1;
            foreach (var i in enemyList)
            {
                if(!i.gameObject.activeSelf)
                {
                    continue;
                }
                Vector3 vec = i.transform.position - GameManager.Instance.Player.transform.position;
                if (prevMinDis == -1 || vec.sqrMagnitude < prevMinDis)
                {
                    for (int k = 1; k < _bulletValue; k++)
                    {
                        targets[k] = targets[k - 1];
                    }
                    targets[0] = i;
                    prevMinDis = vec.sqrMagnitude;
                }
            }

            for (int i = 0;i<_bulletValue;i++)
            {
                if (targets[i])
                {
                    var bullet = GameManager.Instance.Player.BulletPool.Rent();
                    bullet.Shoot(targets[i].gameObject);
                }

            }

            _timer = 0;
        }
    }

    public void Reset()
    {
        _bulletValue = 1;
    }
}
