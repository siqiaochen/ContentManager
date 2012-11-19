using ContentManagerMVC.Models;

namespace ContentManagerMVC.Models
{
    public class PlayerSchedule
    {
        public int ID { get; set; }
        virtual public Schedule schedule { get; set; }
    }
}