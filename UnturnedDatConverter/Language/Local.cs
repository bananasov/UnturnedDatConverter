using SDG.Unturned;

namespace UnturnedDatConverter.Language;

public class Local(DatDictionary? data, DatDictionary? fallbackData = null)
{
    public string? Read(string key)
    {
        return data?.GetString(key);
    }

    public string Format(string key)
    {
        return TryReadString(key, out var text) ? text : key;
    }

    public string Format(string key, object arg0)
    {
        if (!this.TryReadString(key, out var text)) return key;
        try
        {
            return string.Format(text, arg0);
        }
        catch
        {
            return key;
        }
    }

    internal static string FormatText(string text, object arg0)
    {
        string text2;
        try
        {
            text2 = string.Format(text, arg0);
        }
        catch
        {
            text2 = text;
        }

        return text2;
    }

    public string Format(string key, object arg0, object arg1)
    {
        if (!TryReadString(key, out var text)) return key;

        try
        {
            return string.Format(text, arg0, arg1);
        }
        catch
        {
            return key;
        }
    }

    internal static string FormatText(string text, object arg0, object arg1)
    {
        string text2;
        try
        {
            text2 = string.Format(text, arg0, arg1);
        }
        catch
        {
            text2 = text;
        }

        return text2;
    }

    public string Format(string key, object arg0, object arg1, object arg2)
    {
        if (!this.TryReadString(key, out var text)) return key;

        try
        {
            return string.Format(text, arg0, arg1, arg2);
        }
        catch
        {
            return key;
        }
    }

    public static string FormatText(string text, object arg0, object arg1, object arg2)
    {
        string text2;
        try
        {
            text2 = string.Format(text, arg0, arg1, arg2);
        }
        catch
        {
            text2 = text;
        }

        return text2;
    }

    public string Format(string key, params object[] args)
    {
        if (!this.TryReadString(key, out var text)) return key;
        try
        {
            return string.Format(text, args);
        }
        catch
        {
            var text2 = string.Empty;
            for (var i = 0; i < args.Length; i++)
            {
                if (text2.Length > 0)
                {
                    text2 += " ";
                }

                text2 += $"arg{i}: \"{args[i]}\"";
            }

            return key;
        }
    }

    public bool Has(string key)
    {
        return data != null && data.ContainsKey(key);
    }
    
    private bool TryReadString(string key, out string text)
    {
        text = null;

        return (data != null && data.TryGetString(key, out text) && !string.IsNullOrEmpty(text)) ||
               (fallbackData != null && fallbackData.TryGetString(key, out text) &&
                !string.IsNullOrEmpty(text));
    }
}