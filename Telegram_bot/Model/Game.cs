using System;
using System.Collections.Generic;

namespace Telegram_bot.Model;

public partial class Game
{
    public int IdGame { get; set; }

    public string NameGame { get; set; } = null!;

    public string DescriptionGame { get; set; } = null!;

    public string System { get; set; } = null!;

    public string Setting { get; set; } = null!;

    public string Vibes { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public string FromWho { get; set; } = null!;

    public virtual ICollection<GameImage> GameImages { get; set; } = new List<GameImage>();
}
