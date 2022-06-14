using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField,Tooltip("初期化HP変数")]
    int _initHP = 40;
    public int InitHP => _initHP;

    [SerializeField, Tooltip("初期化トランスフォーム")]
    Transform _initTransform;

    List<ISkill> _skillList = new List<ISkill>();

    [SerializeField,Tooltip("弾のプレハブ")]
    BulletController _bulletController = null;
    [SerializeField, Tooltip("弾の親オブジェクト")]
    Transform _bulletParent = null;

    GenericObjectPool<BulletController> _bulletPool;
    public GenericObjectPool<BulletController> BulletPool => _bulletPool;

    private void Awake()
    {
        AddSkill(2);
        _bulletPool = new GenericObjectPool<BulletController>(_bulletController,_bulletParent);
        GameManager.Instance.SetPlayer(this);
    }


    private void Update()
    {
        if(!GameManager.Instance.IsPauseFlag)
        {
            foreach (var skill in _skillList)
            {
                skill.Update();
            }
        }
    }

    public void AddSkill(int skillId)
    {
        var alraedyHaving = _skillList.Where(s => s.SkillId == (SkillDef)skillId); //引数で取ったSkillIDと合致するスキルを抽出
        if (alraedyHaving.Count() > 0)
        {
            alraedyHaving.Single().LevelUp(); //抽出したスキルは一つしかありえないためSingle関数を使用しレベルアップ
        }
        else
        {
            ISkill newSkill = null; 

            switch ((SkillDef)skillId)　//初めてのスキルはnewしてリストに追加
            {
                case SkillDef.NetAttack:
                    newSkill = new NetSkill();
                    break;
                case SkillDef.Bullet:
                    newSkill = new BulletSkill();
                    break;
            }

            if (newSkill != null)
            {
                newSkill.SetUp();
                _skillList.Add(newSkill);
            }
        }
    }

    public void InitPos()
    {
        this.transform.position = _initTransform.position;
    }
}
