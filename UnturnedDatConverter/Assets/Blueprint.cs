namespace UnturnedDatConverter.Assets;

public class Blueprint(
    ushort id,
    BlueprintType type,
    BlueprintSupply[] supplies,
    BlueprintOutput[] outputs,
    ushort tool,
    bool toolCritical,
    BlueprintSkill skill)
{
    public ushort Id { get; set;  } = id;

    public BlueprintType Type { get; set; } = type;

    public BlueprintSupply[] Supplies { get; set;  } = supplies;

    public BlueprintOutput[] Outputs { get; set;  } = outputs;

    public ushort Tool { get; set;  } = tool;

    public bool ToolCritical { get; set;  } = toolCritical;

    public BlueprintSkill Skill { get; set;  } = skill;

    public bool HasSupplies = false;
    public bool HasTool = false;
    public bool HasItem = false;
    public bool HasSkills = false;

    public ushort Tools = 0;
    public ushort Products = 0;
    public ushort Items = 0;
}

public class BlueprintOutput(ushort newId, byte newAmount, ItemOrigin newOrigin)
{
    public ushort Id => newId;
    public ushort Amount = newAmount;
    public ItemOrigin Origin = newOrigin;
}

public class BlueprintSupply(ushort id, bool critical, byte amount)
{
    public ushort Id => id;
    public bool IsCritical => critical;
    public ushort Amount = amount;
    public ushort HasAmount = 0;
}

public enum BlueprintType
{
    Tool,
    Apparel,
    Supply,
    Gear,
    Ammo,
    Barricade,
    Structure,
    Utilities,
    Furniture,
    Repair
}

public enum ItemOrigin
{
    World,
    Admin,
    Craft,
    Nature
}

public enum BlueprintSkill
{
    None,
    Craft,
    Cook,
    Repair
}