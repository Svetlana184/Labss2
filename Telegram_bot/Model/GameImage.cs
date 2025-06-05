using System;
using System.Collections.Generic;
using Telegram_bot.Model;

namespace Telegram_bot;

public partial class GameImage
{
    public int IdGameImage { get; set; }

    public string NameImage { get; set; } = null!;

    public int IdGame { get; set; }

    public byte[] Sourse { get; set; } = null!;

    public virtual Game IdGameNavigation { get; set; } = null!;
}
