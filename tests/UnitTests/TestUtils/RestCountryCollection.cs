using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.TestUtils
{
    [CollectionDefinition("RestCountryCollection")]
    public class RestCountryCollection : ICollectionFixture<RestCountryFixture>
    {
        // This class has no code, and is never created or instantied.
        // Its purpose is simply to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces. Or
        // Its purpose is simply to define the test collection and the associated fixture.
    }
}
