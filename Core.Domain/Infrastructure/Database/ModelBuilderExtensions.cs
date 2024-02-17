using Core.Domain.Entities;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Infrastructure.Database
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //Seed the time to a constant to avoid fresh migrations in every migration event
            DateTime seedTime = new DateTime(2021, 4, 23, 18, 40, 0, 000, DateTimeKind.Unspecified);

            #region Settings
            modelBuilder.Entity<Setting>().HasData(
                    new Setting { SettingId = 1, Key = "InstanceName", Value = "Nito POS", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 2, Key = "CurrencyCode", Value = Constants.Currency, CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 3, Key = "CurrencyConversionUnit", Value = Constants.ConversionUnit.ToString(), CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 4, Key = "MiniStatementSize", Value = "5", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 5, Key = "MinimumMonthsInFullStatement", Value = "1", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 6, Key = "MaximumMonthsInFullStatement", Value = "12", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 7, Key = "Helpline", Value = "0720720720", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 8, Key = "DateTimeFormat", Value = "d/M/yyyy h:mm:ss tt", CreatedAt = seedTime, ModifiedAt = seedTime },// MMMM dd, yyyy h:mm tt
                    new Setting { SettingId = 9, Key = "TimeZone", Value = "E. Africa Standard Time", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 10, Key = "TokenLifetimeInMins", Value = "60", CreatedAt = seedTime, ModifiedAt = seedTime },
                    new Setting { SettingId = 11, Key = "CodeLifetimeInMins", Value = "10", CreatedAt = seedTime, ModifiedAt = seedTime }
                    );
            #endregion

            #region Languages
            modelBuilder.Entity<Language>().HasData(
                    new Language { LanguageId = (byte)Languages.English, Name = nameof(Languages.English), IsoCode = "en", Default = true, CreatedAt = seedTime, ModifiedAt = seedTime, CreatedBy = "seed" },
                    new Language { LanguageId = (byte)Languages.Swahili, Name = nameof(Languages.Swahili), IsoCode = "sw", Default = false, CreatedAt = seedTime, ModifiedAt = seedTime, CreatedBy = "seed" });
            #endregion

        }
    }
}
