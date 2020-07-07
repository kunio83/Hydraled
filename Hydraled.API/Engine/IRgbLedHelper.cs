using Hydraled.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hydraled.API.Engine
{
    interface IRgbLedHelper
    {
        void SetColor(RgbLedSetting rgbLedSetting);
    }
}
