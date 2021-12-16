
public enum EventID
{
    None = 0,
    OnDetectPlayer,
    OnUseHeal,
    OnUseStrength,
    OnUseShield,
    OnUseWeapon,
    OnFinishGame
}

public enum ItemKind
{
    Weapon,
    Strength,
    Shield,
    Heal
}

public enum TrapKind
{
    None = 0,
    Spike,
    Saw,
    Rocket,
    StormDome,
    BobTrap,
    SkullBall,
    PowerPole
}

public enum Kind_Character
{
    Player,
    Bot,
    Bot1,
    Bot2,
    Bot3, 
    Bot4, 
    Bot5, 
    Bot6
}

public enum GUIName
{
    None = 0,
    PauseDialog,
    VictoryDialog,
    DefeatDialog,
    YouDieDialog,
}