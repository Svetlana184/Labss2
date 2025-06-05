using System;
using System.Collections.Generic;

namespace API_DndBot.Model;

public partial class LocationImage
{
    public int IdLocationImage { get; set; }

    public string NameImage { get; set; } = null!;

    public int IdLocation { get; set; }

    public byte[] Source { get; set; } = null!;

    public virtual GeneratorLocation IdLocationNavigation { get; set; } = null!;
}
