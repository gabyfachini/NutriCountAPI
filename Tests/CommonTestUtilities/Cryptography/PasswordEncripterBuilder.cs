﻿using NutriCount.Application.Services.Cryptography;

namespace CommonTestUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncripter Build() => new("abc1234");
    }
}
