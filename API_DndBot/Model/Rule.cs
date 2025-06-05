using System;
using System.Collections.Generic;

namespace API_DndBot.Model;

public partial class Rule
{
    public int IdRule { get; set; }

    public string NameRule { get; set; } = null!;

    public string TypeOfRule { get; set; } = null!;

    public string? Link { get; set; }

    public byte[]? Source { get; set; }

    public string? DescriptionRule { get; set; }
}
