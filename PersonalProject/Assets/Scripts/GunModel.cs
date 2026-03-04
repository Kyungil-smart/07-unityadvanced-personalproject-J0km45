using System;

public class GunModel
{
    public int MaxMagazine { get; private set; } // 최대 탄창 수
    public int CurrentMagazine { get; private set; } // 현재 탄창 수
    public bool IsAiming { get; private set; }
    public bool IsMagazineFull() => CurrentMagazine == MaxMagazine;

    public event Action<int, int> OnMagazineChanged;

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
        OnMagazineChanged?.Invoke(CurrentMagazine, MaxMagazine);
        return true;
    }

    public void SetAiming(bool isAiming)
    {
        IsAiming = isAiming;
    }

    public void Reload()
    {
        CurrentMagazine = MaxMagazine;
        OnMagazineChanged?.Invoke(CurrentMagazine, MaxMagazine);
    }
}
