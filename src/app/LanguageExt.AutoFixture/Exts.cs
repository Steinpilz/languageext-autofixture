using AutoFixture;

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
