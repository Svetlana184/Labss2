using System;
using System.Collections.Generic;

namespace API_DndBot.Model;

public partial class GeneratorLocation
{
    public int IdLocation { get; set; }

    public string NameLocation { get; set; } = null!;

    public string DescriptionLocation { get; set; } = null!;

    public string? Setting { get; set; }

    public string FromWho { get; set; } = null!;

    public virtual ICollection<LocationImage> LocationImages { get; set; } = new List<LocationImage>();
}
