﻿
namespace Core.Management.Infrastructure.Seedwork
{
    public interface ISeed
    {
        Task SeedDefaults();
        void UpdateHiLoSequences();
    }
}
