using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GiderSkill : Projectile
{
    public float LifeTime;
    public GameObject GiderFrame;
    public GameObject GiderProjectile;
    public float Speed;
    public float GiderProjectileDelay;

    SpriteRenderer _giderFrameSr;

    void Start()
    {
        transform.position = Holder.transform.position;
        Vector3 direction = Target.transform.position - Holder.transform.position;
        RotateToDirection(direction);

        _giderFrameSr = GiderFrame.GetComponent<SpriteRenderer>();
        GiderFrame.transform.DOScaleY(0.15f, 0.2f);
        _giderFrameSr.DOFade(0, 0.5f).SetDelay(0.8f);

        GiderProjectileDelay += Time.time;
        
        GiderProjectile.GetComponent<Projectile>().Holder = Holder;
        Destroy(gameObject, LifeTime);

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > GiderProjectileDelay)
        {
            if (GiderProjectile != null)
            {
                GiderProjectile.SetActive(true);
                GiderProjectile.transform.Translate(Vector3.up * Speed * Time.deltaTime, Space.Self);
            }
        }
    }
    private void OnDestroy()
    {
        GiderFrame.transform.DOKill();
        _giderFrameSr.DOKill();
    }
}
