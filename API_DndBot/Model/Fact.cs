using System;
using System.Collections.Generic;

namespace API_DndBot.Model;

public partial class Fact
{
    public int IdFact { get; set; }

    public string NameFact { get; set; } = null!;

    public string DescriptionFact { get; set; } = null!;

    public byte[]? ImageFact { get; set; }
}
