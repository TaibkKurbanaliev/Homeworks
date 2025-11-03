using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework2
{
    public interface IStorage
    {
        void Save(object save);
        T Load<T>();
    }
}
