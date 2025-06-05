using System;
using System.Collections.Generic;

namespace Telegram_bot.Model;

public partial class Fact
{
    public int IdFact { get; set; }

    public string NameFact { get; set; } = null!;

    public string DescriptionFact { get; set; } = null!;

    public byte[]? ImageFact { get; set; }
}
