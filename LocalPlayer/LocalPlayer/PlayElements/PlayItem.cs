using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPlayer.PlayElements
{
    class PlayItem
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public int Duration { get; set; }
        public PlayItem(string Title,string filepath, int duration)
        {
            Path = filepath;
            Duration = duration;
        }
    }
}
