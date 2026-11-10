using System.Text;

namespace CommandShell.Core;

public class Parser
{
    public CommandContext Parse(string input)
    {
        string rawInput = input ?? string.Empty;
        List<string> tokens = SplitIntoTokens(rawInput);

        string commandName = string.Empty;
        var arguments = new List<string>();
        var switches = new List<string>();

        for (int i = 0; i < tokens.Count; i++)
        {
            string token = tokens[i];

            if (i == 0)
            {
                commandName = token.ToLower();
            }
            else if (token.StartsWith("/"))
            {
                switches.Add(token);
            }
            else
            {
                arguments.Add(token);
            }
        }

        return new CommandContext(commandName, arguments, switches);
    }

    private List<string> SplitIntoTokens(string input)
    {
        var tokens = new List<string>();
        var current = new StringBuilder();
        bool insideQuotes = false;

        foreach (char c in input)
        {
            if (c == '"')
            {
                insideQuotes = !insideQuotes;
            }
            else if (c == ' ' && !insideQuotes)
            {
                AddToken(tokens, current);
            }
            else
            {
                current.Append(c);
            }
        }

        AddToken(tokens, current);
        return tokens;
    }

    private void AddToken(List<string> tokens, StringBuilder current)
    {
        if (current.Length > 0)
        {
            tokens.Add(current.ToString());
            current.Clear();
        }
    }
}
