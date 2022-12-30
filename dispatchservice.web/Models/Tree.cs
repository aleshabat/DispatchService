using System;
using System.Collections.Generic;

namespace dispatchservice.web.Models
{
    public class Tree
    {
        public Tree()
        {
            Childs = new HashSet<Tree>();
        }
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ParentId { get; set; }

        public ICollection<Tree> Childs { get; set; }

        public bool Selected { get; set; }

        public bool Deleted { get; set; }

        public int Level { get; set; }
        
        public bool IsLeaf
        {
            get
            {
                return Childs != null && Childs.Count == 0;
            }
        }
    }

}