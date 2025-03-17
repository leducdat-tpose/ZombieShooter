using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStateZombie : BaseState<Monster>
{
    private Coroutine _coroutine;
    public DeathStateZombie(Monster obj, StateManager<Monster> objectStateManager) : base(obj, objectStateManager)
    {
    }

    public override void EnterState()
    {
        owner.Rigid.velocity = Vector2.zero;
        MonoBehaviour mb = owner as MonoBehaviour;
        _coroutine = mb.StartCoroutine(RemainAfterDeath());
    }

    public override void ExitState()
    {
        MonoBehaviour mb = owner as MonoBehaviour;
        if(_coroutine != null)
        {
            mb.StopCoroutine(_coroutine);
        }
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
    }
    private IEnumerator RemainAfterDeath()
    {
        yield return new WaitForSeconds(2.0f);
        owner.gameObject.SetActive(false);
    }
}
