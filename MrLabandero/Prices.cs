using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrLabandero
{
    public static class Prices
    {
            // Full Service (per load)
            public const decimal FS_Clothes = 180;
            public const decimal FS_Towels = 190;
            public const decimal FS_Beddings = 210;

            // Regular Wash (per kilo)
            public const decimal W_Clothes = 35;
            public const decimal W_Towels = 45;
            public const decimal W_Beddings = 65;

            // Dry and Fold (per basket)
            public const decimal DF_Clothes = 100;
            public const decimal DF_Towels = 110;
            public const decimal DF_Beddings = 130;

            // Add-ons
            public const decimal AddSpin = 30;
            public const decimal AddWash = 30;
            public const decimal AddRinse = 30;
            public const decimal ExtraDetergent = 25;
            public const decimal ExtraFabcon = 15;
    }
}
