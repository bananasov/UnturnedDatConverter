namespace UnturnedDatConverter.Assets;

public class Action(
    ushort source,
    ActionType type,
    ActionBlueprint[] blueprints,
    string text,
    string tooltip,
    string key)
{
    public ushort Source { get; set;  } = source;

    public ActionType Type { get; set;  } = type;

    public ActionBlueprint[] Blueprints { get; set;  } = blueprints;

    public string Text { get; set;  } = text;

    public string Tooltip { get; set;  } = tooltip;

    public string Key { get; set;  } = key;
}

public enum ActionType
{
    Blueprint
}

public class ActionBlueprint(byte id, bool link)
{
    public byte Id { get; } = id;

    public bool IsLink { get; } = link;
}