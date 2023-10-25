using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SkudExplosion : Projectile
{
    public GameObject SkudVFX;

    SpriteRenderer _sr;
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        transform.DOScale(1f, 1f).SetEase(Ease.Linear);
        transform.DOLocalRotate(new Vector3(0, 0, 160f), 1f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
        _sr.DOFade(1f, 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            SkudVFX.SetActive(true);
            _sr.DOFade(0, 0.2f).SetEase(Ease.Linear);
            GameObject[] champions = GameObject.FindGameObjectsWithTag(TargetTag);
            foreach (GameObject champion in champions)
            {
                if (champion.activeSelf && Vector2.Distance(champion.transform.position, transform.position) < 2.5f)
                {
                    // ToDO
                }
            }
        });

        Destroy(gameObject, 2.1f);
    }

    private void OnDestroy()
    {
        _sr.DOKill();
    }
}
