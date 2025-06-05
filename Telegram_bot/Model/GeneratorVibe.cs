using System;
using System.Collections.Generic;

namespace Telegram_bot;

public partial class GeneratorVibe
{
    public int IdVibes { get; set; }

    public string NameVibes { get; set; } = null!;

    public string? TextVibes { get; set; }

    public byte[]? ImageVibes { get; set; }

    public string FromWho { get; set; } = null!;
}
