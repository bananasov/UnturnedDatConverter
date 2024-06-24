using SDG.Unturned;
using UnturnedDatConverter.Language;

namespace UnturnedDatConverter.Assets;

public abstract class Asset
{
    public virtual void PopulateAsset(DatDictionary data, Local localization)
    {
       Id = data.ParseUInt16("ID", 0);
    }
    
    public ushort Id;
}

public enum ItemType
{
    Hat,
    Pants,
    Shirt,
    Mask,
    Backpack,
    Vest,
    Glasses,
    Gun,
    Sight,
    Tactical,
    Grip,
    Barrel,
    Magazine,
    Food,
    Water,
    Medical,
    Melee,
    Fuel,
    Tool,
    Barricade,
    Storage,
    Beacon,
    Farm,
    Trap,
    Structure,
    Supply,
    Throwable,
    Grower,
    Optic,
    Refill,
    Fisher,
    Cloud,
    Map,
    Key,
    Box,
    ArrestStart,
    ArrestEnd,
    Tank,
    Generator,
    Detonator,
    Charge,
    Library,
    Filter,
    Sentry,
    VehicleRepairTool,
    Tire,
    Compass,
    OilPump
}

public enum ItemRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}


/// <summary>
/// Used for item placement in displays / holsters, and whether useable can be placed in primary/secondary slot.
/// </summary>
public enum SlotType
{
    /// <summary>
    /// Cannot be placed in primary nor secondary slots, but can be equipped from bag.
    /// </summary>
    None,
    /// <summary>
    /// Can be placed in primary slot, but cannot be equipped in secondary or bag.
    /// </summary>
    Primary,
    /// <summary>
    /// Can be placed in primary or secondary slot, but cannot be equipped from bag.
    /// </summary>
    Secondary,
    /// <summary>
    /// Only used by NPCs.
    /// </summary>
    Tertiary,
    /// <summary>
    /// Can be placed in primary, secondary, or equipped while in bag.
    /// </summary>
    Any
}