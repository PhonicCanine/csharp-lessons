using System;
namespace CookieThur
{
    public class Upgrade
    {
        private DateTime upgraded;
        private int _Level;
        public int Level { get
            {
                return _Level;
            }
            set
            {
                upgraded = DateTime.Now;
                _Level = value;
            }
        }
        public int Rate { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int cost { get
            {
                return (Level + 1) * (Level + 1) * Price;
            } }
    }
}
