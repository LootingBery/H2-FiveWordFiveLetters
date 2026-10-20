using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionTester
{
    public class Class1<T> : BaseCollection<T>
    {
        public override int Count()
        {
            throw new NotImplementedException();
        }

        public override T FirstObject()
        {
            throw new NotImplementedException();
        }

        public override T LastObject()
        {
            throw new NotImplementedException();
        }

        protected override void FillCollectionInternal(string[] input, Func<string, T> func)
        {
            throw new NotImplementedException();
        }

        protected override void PrintCollectionInternal(TextWriter writer)
        {
            throw new NotImplementedException();
        }

        protected override void SortCollectionInternal(Func<T, T> comparer)
        {
            throw new NotImplementedException();
        }
    }
}
