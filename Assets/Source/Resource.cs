using UnityEngine;

namespace Lomztein.BruteForceAttackSequel
{
    public class Resource
    {
        protected string Path { get; private set; }
        protected Object Object { get; set; }

        public Object Get()
        {
            if (Object == null)
                Object = Resources.Load(Path);
            return Object;
        }

        public Resource () { }

        public Resource(string _path)
        {
            Path = _path;
        }

        public override string ToString()
        {
            return Path;
        }
    }

    public class Resource<T> : Resource where T : Object
    {
        protected new T Object { get { return base.Object as T; } set { base.Object = value as T; } }

        public new T Get()
        {
            if (Object == null)
                Object = Resources.Load<T>(Path);
            return Object;
        }

        public Resource(): base () { } 

        public Resource(string _path) : base(_path) { }
    }
}