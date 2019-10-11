using System;

namespace TechSupportWeb.Domain
{
    public class EntityBase
    {
        public EntityBase()
        {
            ID = DateTime.Now.Ticks;
        }

        public long ID { get; }
    }
}