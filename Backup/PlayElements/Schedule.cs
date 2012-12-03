﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalPlayer.PlayElements
{
    class Schedule
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Boolean Continuous { get; set; }
        public Boolean PlayOnePerTime { get; set; }
        public List<PlayItem> PlayItems { get; set; }
        public int index;
        public Int32 CurrentItemIndex
        {
            get
            {
                if (PlayItems.Count <= index) index = 0;
                return index;
            }
            set
            {
                if (PlayItems.Count > value)
                    index = value;
                else
                    index = 0;
            }
        }
        public PlayItem CurrentItem
        {
            get
            {
                if (PlayItems.Count > CurrentItemIndex)
                    return PlayItems[CurrentItemIndex];
                else return null;
            }
        }
        public Schedule()
        {
            PlayItems = new List<PlayItem>();
            CurrentItemIndex = 0;
        }
    }
}
