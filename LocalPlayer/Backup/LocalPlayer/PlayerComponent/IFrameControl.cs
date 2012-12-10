using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPlayer.PlayerComponent
{

    // A delegate type for hooking up change notifications.
    public delegate void PlayFinishedEventHandler(object sender, EventArgs e);
    interface IFrameControl
    {
        void PlayFile(String path);
        void Replay();
        void Stop();
        void SetDuration(int secs);
        void ReleaseResources();
        event PlayFinishedEventHandler PlayFinishedEvent;
    }
}
