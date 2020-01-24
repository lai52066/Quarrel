﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarrel.ViewModels.Services.Voice.Audio
{
    public interface IAudioService
    {
        void CreateGraph(string deviceId = null);
        string DeviceId { get; }
        void Dispose();
    }
}
