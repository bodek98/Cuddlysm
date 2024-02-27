using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ReloadableWeapon : Weapon
{
    [SerializeField] protected int _magazineAmmo;
    [SerializeField] protected int _magazineCapacity;
    [SerializeField] protected int _storageAmmo;
    [SerializeField] protected int _storageAmmoCapacity;
    [SerializeField] private float _reloadDuration = 2.5f;

    protected WeaponGUIUpdater _weaponGUIUpdater;

    protected bool _isReloading = false;
    private IEnumerator _reloadingCoroutine;

    public void Awake()
    {
        _weaponGUIUpdater = GetComponentInParent<WeaponGUIUpdater>();
    }
    
    private void OnDisable()
    {
        if (_reloadingCoroutine != null)
        {
            StopCoroutine(_reloadingCoroutine);
        }
    }
    
    public override void StartReloading(bool forceReload = false)
    {
        if (!forceReload && _isReloading && _storageAmmo <= 0) return;
        
        StopAttack();
        _reloadingCoroutine = Reload();
        StartCoroutine(_reloadingCoroutine);
    }

    protected IEnumerator Reload()
    {
        _isReloading = true;

        yield return StartCoroutine(WaitForReloadAndUpdateGUI(_reloadDuration));

        if (transform.GameObject().activeSelf)
        {
            int missingAmmo = _magazineCapacity - _magazineAmmo;

            if (missingAmmo < _storageAmmo)
            {
                _magazineAmmo += missingAmmo;
                _storageAmmo -= missingAmmo;
            }
            else
            {
                _magazineAmmo += _storageAmmo;
                _storageAmmo = 0;
            }
        }

        _isReloading = false;
        _weaponGUIUpdater?.UpdateAmmoGUI(_magazineAmmo, _storageAmmo);
    }
    
    protected void CheckAutoReload()
    {
        if (_magazineAmmo == 0 && _storageAmmo > 0)
        {
            StartReloading();
        }
    }

    protected void HandleGUIUpdate()
    {
        _weaponGUIUpdater?.UpdateAmmoGUI(_magazineAmmo, _storageAmmo);
        _weaponGUIUpdater?.FillAmmoBar(_magazineAmmo > 0 ? (float)_magazineAmmo / (float)_magazineCapacity : 0);
    }
    
    protected void RefreshWeapon()
    {
        HandleGUIUpdate();

        _weaponGUIUpdater?.SetWeaponSprite(sprite);

        if (_isReloading)
        {
            StartReloading(true);
        }
        else
        {
            CheckAutoReload();
        }
    }

    private IEnumerator WaitForReloadAndUpdateGUI(float duration)
    {
        float timePassed = 0f;
        float animationFrameDelay = 0.01f;

        while (timePassed < duration)
        {
            _weaponGUIUpdater?.FillAmmoBar(timePassed / duration);
            yield return new WaitForSeconds(animationFrameDelay);
            timePassed += animationFrameDelay;
        }
    }
}
