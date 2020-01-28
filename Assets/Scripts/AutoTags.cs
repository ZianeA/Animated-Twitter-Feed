using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AutoTags : MonoBehaviour
{
    private static readonly string startTag = "<color=#1DA1F2>";
    private static readonly string endTag = "</color>";
    private static readonly char[] invalidCharacters = "!,$%^&*+.;:".ToCharArray();

    private TextMeshProUGUI textLabel;

    private void Awake()
    {
        //Add tags before type-on effect.
        textLabel = GetComponent<TextMeshProUGUI>();
        textLabel.text = AddTags(textLabel.text);
    }

    private string AddTags(string text)
    {
        text = text.Trim();
        var output = text;
        var insert = false;
        var j = 0;

        for (int i = 0; i < text.Length; i++)
        {
            var c = text[i];

            if (c == '#' || c == '@')
            {
                var nextChar = i == text.Length - 1 ? ' ' : text[i + 1];

                if (!invalidCharacters.Any(ch => nextChar == ch) && !char.IsWhiteSpace(nextChar))
                {
                    output = output.Insert(j, startTag);
                    j += startTag.Length;
                    insert = true;
                }
            }

            if (insert)
            {
                if (invalidCharacters.Any(ch => c == ch) || char.IsWhiteSpace(c))
                {
                    output = output.Insert(j, endTag);
                    j += endTag.Length;
                    insert = false;
                }

                else if (i == text.Length - 1)
                {
                    output = output.Insert(j + 1, endTag);
                    insert = false;
                }
            }

            j++;
        }

        return output;
    }
}
