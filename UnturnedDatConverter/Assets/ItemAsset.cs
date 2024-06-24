using SDG.Unturned;
using UnturnedDatConverter.Language;

namespace UnturnedDatConverter.Assets;

public class ItemAsset : Asset
{
    public override void PopulateAsset(DatDictionary data, Local localization)
    {
        base.PopulateAsset(data, localization);

        ItemName = localization.Format("Name");
        ItemDescription = localization.Format("Description");

        Type = (ItemType)Enum.Parse(typeof(ItemType), data.GetString("Type"), true);

        if (data.ContainsKey("Rarity"))
        {
            Rarity = (ItemRarity)Enum.Parse(typeof(ItemRarity), data.GetString("Rarity"), true);
        }
        else
        {
            Rarity = ItemRarity.Common;
        }

        SizeX = data.ParseUInt8("Size_X");
        SizeY = data.ParseUInt8("Size_Y");

        Amount = data.ParseUInt8("Amount");

        CountMin = data.ParseUInt8("Count_Min");
        CountMax = data.ParseUInt8("Count_Max");

        QualityMin = data.ParseUInt8("Quality_Min");
        QualityMax = data.ParseUInt8("Quality_Max");

        Slot = data.ParseEnum("Slot", SlotType.None);

        var blueprintAmount = data.ParseUInt8("Blueprints");
        Blueprints = new List<Blueprint>
        {
            Capacity = blueprintAmount
        };

        #region Blueprint parsing

        for (byte i = 0; i < blueprintAmount; i += 1)
        {
            var blueprintType =
                (BlueprintType)Enum.Parse(typeof(BlueprintType), data.GetString($"Blueprint_{i}_Type"), true);

            var supplyAmount = data.ParseUInt8($"Blueprint_{i}_Supplies");
            if (supplyAmount < 1) supplyAmount = 1;

            var blueprintSupplies = new BlueprintSupply[supplyAmount];

            var supplyIndex = 0;
            while (supplyIndex < blueprintSupplies.Length)
            {
                var supplyId = data.ParseUInt16($"Blueprint_{i}_Supply_{supplyIndex}_ID");
                var supplyCritical = data.ContainsKey($"Blueprint_{i}_Supply_{supplyIndex}_Critical");
                var supplyItemAmount = data.ParseUInt8($"Blueprint_{i}_Supply_{supplyIndex}_Amount");
                if (supplyItemAmount < 1) supplyItemAmount = 1;

                blueprintSupplies[supplyIndex] = new BlueprintSupply(supplyId, supplyCritical, supplyItemAmount);

                supplyIndex += 1;
            }

            var blueprintOutputAmount = data.ParseUInt8($"Blueprint_{i}_Outputs");
            BlueprintOutput[] outputs;

            if (blueprintOutputAmount > 0)
            {
                outputs = new BlueprintOutput[blueprintOutputAmount];

                var outputIndex = 0;
                while (outputIndex < outputs.Length)
                {
                    var outputId = data.ParseUInt16($"Blueprint_{i}_Output_{outputIndex}_ID");
                    var outputAmount = data.ParseUInt8($"Blueprint_{i}_Output_{outputIndex}_Amount");
                    if (outputAmount < 1) outputAmount = 1;

                    var outputOrigin = data.ParseEnum($"Blueprint_{i}_Output_{outputIndex}_Origin", ItemOrigin.Craft);

                    outputs[outputIndex] = new BlueprintOutput(outputId, outputAmount, outputOrigin);

                    outputIndex += 1;
                }
            }
            else
            {
                outputs = new BlueprintOutput[1];
                var blueprintProduct = data.ParseUInt16($"Blueprint_{i}_Product");
                if (blueprintProduct == 0) blueprintProduct = Id;

                var blueprintProducts = data.ParseUInt8($"Blueprint_{i}_Products");
                if (blueprintProducts < 1) blueprintProducts = 1;

                var itemOrigin = data.ParseEnum($"Blueprint_{i}_Origin", ItemOrigin.Craft);
                outputs[0] = new BlueprintOutput(blueprintProduct, blueprintProducts, itemOrigin);
            }

            var blueprintTool = data.ParseUInt16($"Blueprint_{i}_Tool");
            var blueprintToolCritical = data.ContainsKey($"Blueprint_{i}_Tool_Critical");

            var blueprintSkillLevel = data.ParseUInt8($"Blueprint_{i}_Level");
            var blueprintSkill = BlueprintSkill.None;
            if (blueprintSkillLevel > 0)
            {
                blueprintSkill = (BlueprintSkill)Enum.Parse(typeof(BlueprintSkill), data.GetString(
                    $"Blueprint_{i}_Skill"), true);
            }

            var blueprint = new Blueprint(Id, blueprintType, blueprintSupplies, outputs, blueprintTool,
                blueprintToolCritical, blueprintSkill);
            Blueprints.Add(blueprint);
        }

        #endregion

        var actionAmount = data.ParseUInt8("Actions");
        Actions = new List<Action>
        {
            Capacity = actionAmount
        };

        #region Action parsing

        for (byte i = 0; i < actionAmount; i += 1)
        {
            var actionType = (ActionType)Enum.Parse(typeof(ActionType), data.GetString($"Action_{i}_Type"), true);
            
            var actionBlueprintAmount = data.ParseUInt8($"Action_{i}_Blueprints", 0);
            if (actionBlueprintAmount < 1) actionBlueprintAmount = 1;
            
            var actionBlueprints = new ActionBlueprint[actionBlueprintAmount];
            
            var blueprintIndex = 0;
            while (blueprintIndex < actionBlueprints.Length)
            {
                var actionBlueprintIndex = data.ParseUInt8($"Action_{i}_Blueprint_{blueprintIndex}_Index");
                var blueprintLink = data.ContainsKey($"Action_{i}_Blueprint_{blueprintIndex}_Link");
                
                actionBlueprints[blueprintIndex] = new ActionBlueprint(actionBlueprintIndex, blueprintLink);
                
                blueprintIndex += 1;
            }
            
            var actionKey = data.GetString($"Action_{i}_Key");
            string actionText;
            string actionTooltip;

            if (string.IsNullOrEmpty(actionKey))
            {
                var text = $"Action_{i}_Text";
                actionText = localization.Has(text) ? localization.Format(text) : data.GetString(text);
                
                var text3 = $"Action_{i}_Tooltip";
                actionTooltip = localization.Has(text3) ? localization.Format(text3) : data.GetString(text3);
            }
            else
            {
                actionText = string.Empty;
                actionTooltip = string.Empty;
            }
            
            var actionSource = data.ParseUInt16($"Action_{i}_Source");
            if (actionSource == 0) actionSource = Id;

            var action = new Action(actionSource, actionType, actionBlueprints, actionText, actionTooltip, actionKey);
            Actions.Add(action);
        }

        #endregion
    }

    public string ItemName { get; private set; }
    public string ItemDescription { get; private set; }

    public ItemType Type { get; private set; }
    public ItemRarity Rarity { get; private set; }

    public byte SizeX { get; private set; }
    public byte SizeY { get; private set; }

    public byte Amount { get; private set; }

    public byte CountMin { get; private set; }
    public byte CountMax { get; private set; }

    public byte QualityMin { get; private set; }
    public byte QualityMax { get; private set; }

    public SlotType Slot { get; private set; }

    public List<Blueprint> Blueprints { get; private set; } = [];
    public List<Action> Actions { get; private set; } = [];
}