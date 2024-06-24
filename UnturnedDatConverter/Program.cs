using SDG.Unturned;
using UnturnedDatConverter.Assets;
using UnturnedDatConverter.Language;

DatParser parser = new();

using var contentReader =
    new StreamReader(
        @"C:\Program Files (x86)\Steam\steamapps\common\U3DS\Servers\Escalation\Workshop\Steam\content\304930\3251926587\Escalation\Bundles\Items\Weapons\Guns\ECS-25\ECS-25.dat");
var content = contentReader.ReadToEnd();

using var languageContentReader =
    new StreamReader(
        @"C:\Program Files (x86)\Steam\steamapps\common\U3DS\Servers\Escalation\Workshop\Steam\content\304930\3251926587\Escalation\Bundles\Items\Weapons\Guns\ECS-25\English.dat");
var languageContent = languageContentReader.ReadToEnd();

var data = parser.Parse(content);
var languageData = parser.Parse(languageContent);

var localization = new Local(languageData);

var asset = new ItemAsset();
asset.PopulateAsset(data, localization);

foreach (var action in asset.Actions)
{
    Console.WriteLine($"Action: {action.Key} - {action.Text}");
}