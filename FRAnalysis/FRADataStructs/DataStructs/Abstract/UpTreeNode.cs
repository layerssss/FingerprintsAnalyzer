using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Abstract
{

    public class UpTreeNode<ObjectType>
    {
        public UpTreeNode<ObjectType> Parent;
        public ObjectType Obj;
        public UpTreeNode(UpTreeNode<ObjectType> parent, ObjectType obj)
        {
            this.Parent = parent;
            this.Obj = obj;
        }
    }
}
