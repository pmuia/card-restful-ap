﻿
namespace Core.Management.Interfaces
{
    public interface IConfigurationRepository
    {
        string TimeZone { get; set; }
        string Helpline { get; set; }
        string DateTimeFormat { get; set; }
        int TokenLifetimeInMins { get; set; }
        int CodeLifetimeInMins { get; set; }
    }
}
