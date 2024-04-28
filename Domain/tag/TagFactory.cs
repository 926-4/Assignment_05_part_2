using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Team_42.Domain.Tag
{
    internal class TagFactory
    {
        public Tag Instance = new ();

        public TagFactory NewTag()
        {
            Instance = new ();
            return this;
        }

        public TagFactory SetName(string name)
        {
            Instance.Name = name;
            return this;
        }

        public Tag Get()
        {
            Tag returnValue = Instance;
            Instance = new ();
            return returnValue;
        }
    }
}
