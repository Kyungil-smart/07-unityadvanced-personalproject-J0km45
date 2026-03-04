using System;

public class GunModel
{
    public int MaxMagazine { get; private set; } // 최대 탄창 수
    public int CurrentMagazine { get; private set; } // 현재 탄창 수
    public bool IsAiming { get; private set; }
    public bool IsMagazineFull() => CurrentMagazine == MaxMagazine;

    public GunModel(int maxMagazine)
    {
        MaxMagazine = maxMagazine;
        CurrentMagazine = maxMagazine;
        IsAiming = false;
    }

    public bool TryFire()
    {
        if (CurrentMagazine <= 0) return false;

        CurrentMagazine--;
        return true;
    }

    public void SetAiming(bool isAiming)
    {
        IsAiming = isAiming;
    }

    public void Reload()
    {
        CurrentMagazine = MaxMagazine;
    }
}
