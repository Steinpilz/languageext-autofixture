using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageExt.AutoFixture
{

    public static class Exts
    {
        public static void LanguageExt(this IFixture fixture)
        {
            fixture.Customize(new LanguageExtCustomization());
        }
    }
}
