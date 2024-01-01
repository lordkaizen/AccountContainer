using System;
using System.Collections.Generic;

namespace AccountConsole;

public partial class Account
{
    public int Id { get; set; }

    public string Platform { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? RecoveryCode { get; set; }
}
