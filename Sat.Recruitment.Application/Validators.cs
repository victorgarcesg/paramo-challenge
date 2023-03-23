﻿using FluentValidation;

namespace Sat.Recruitment.Application
{
    public static class Validators
    {
        public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().EmailAddress();
        public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().MinimumLength(3);
        public static IRuleBuilderOptions<T, string> Address<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty();
        public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> builder) => builder.NotEmpty().WithMessage("Phone number is required.").Matches(@"^\d{10}$");
    }
}
