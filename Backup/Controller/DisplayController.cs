using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LocalPlayer.PlayElements;

namespace LocalPlayer.Controller
{
    class DisplayController
    {
        List<Schedule> schedules;
        int nextSchedule;
        public string CurrentPlayItem { get; set; }
        public DisplayController(List<Schedule> sches)
        {
            nextSchedule = 0;
            schedules = sches;
            CurrentPlayItem = "";
        }
        public DisplayController()
        {
            nextSchedule=0;
            schedules = new List<Schedule>();
            CurrentPlayItem = "";
        }
        public void ReceiveSchedule()
        { 
        
        }
        public void LoadSchedule()
        { 
            
        }
        public PlayItem ReadSchedule()
        {
            if (schedules != null)
            {
                if (nextSchedule >= schedules.Count)
                    nextSchedule = 0;
                Schedule sche = schedules[nextSchedule];
                PlayItem item = sche.CurrentItem;
                sche.CurrentItemIndex++;
                //  if schedule ends and replay item 0, play next schedule               
                if (sche.CurrentItemIndex == 0)
                    nextSchedule++;
                return item;
            }
            else
            {
                nextSchedule++;
                return null; 
            }
        }
        /*
        public void DoWork()
        {
            while (true)
            {
                Thread.Sleep(100);
                PlayItem item = readSchedule();
                if (item != null)
                {
                    displayForm.Play(item);
                }
            }
        }
         */ 
    }
}
