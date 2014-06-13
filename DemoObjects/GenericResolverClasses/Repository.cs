using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithGenerics.DemoObjects.GenericResolverClasses
{
    public class Repository : IRepository
    {
        ICustomDbContext context;

        public Repository(ICustomDbContext dbContext)
        { context = dbContext; }

        public void Save(string item)
        {
            Debug.Print("Saving " + item + " to " + context.ToString());
        }
    }
}