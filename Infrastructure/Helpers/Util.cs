using System;
using System.Text;
using crypto = System.Security.Cryptography;

namespace EnergyHeatMap.Infrastructure.Helpers
{
    internal static class Util
    {
        private static readonly crypto.SHA256 Sha256 = crypto.SHA256.Create();

        internal static string ComputeHash(string value, string secret)
        {
            var bytes = Encoding.UTF8.GetBytes($"{secret}.{value}");
            var hash = Sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
